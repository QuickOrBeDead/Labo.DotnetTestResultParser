namespace Labo.DotnetTestResultParser.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Labo.DotnetTestResultParser.IO;
    using Labo.DotnetTestResultParser.Model;
    using Labo.DotnetTestResultParser.Parsers;
    using Labo.DotnetTestResultParser.Templates.Factory;

    /// <summary>
    /// The output template manager class.
    /// </summary>
    public sealed class OutputTemplateManager
    {
        private readonly string _xmlPath;

        private readonly ITestRunResultParser _testRunResultParser;
        private readonly IFileSystemManager _directoryWrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="OutputTemplateManager"/> class.
        /// </summary>
        /// <param name="xmlPath">The XML path.</param>
        /// <param name="runResultParser">The run result parser.</param>
        /// <param name="directoryWrapper">The directory wrapper.</param>
        /// <exception cref="ArgumentException">Value cannot be null or whitespace. - xmlPath</exception>
        /// <exception cref="ArgumentNullException">
        /// runResultParser
        /// or
        /// directoryWrapper
        /// </exception>
        public OutputTemplateManager(string xmlPath, ITestRunResultParser runResultParser, IFileSystemManager directoryWrapper)
        {
            if (string.IsNullOrWhiteSpace(xmlPath))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(xmlPath));
            }

            _xmlPath = xmlPath;
            _testRunResultParser = runResultParser ?? throw new ArgumentNullException(nameof(runResultParser));
            _directoryWrapper = directoryWrapper ?? throw new ArgumentNullException(nameof(directoryWrapper));
        }

        /// <summary>
        /// Creates the output template.
        /// </summary>
        /// <param name="outputTemplateFactory">The output template factory.</param>
        /// <param name="outputTemplateType">Type of the output template.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">outputTemplateType - null</exception>
        public static IOutputTemplate CreateOutputTemplate(IOutputTemplateFactory outputTemplateFactory, OutputTemplateType outputTemplateType)
        {
            switch (outputTemplateType)
            {
                case OutputTemplateType.Summary:
                    return outputTemplateFactory.CreateSummaryOutputTemplate();
                case OutputTemplateType.TestResult:
                    return outputTemplateFactory.CreateTestResultOutputTemplate();
                default:
                    throw new ArgumentOutOfRangeException(nameof(outputTemplateType), outputTemplateType, null);
            }
        }

        /// <summary>
        /// Creates the output template factory.
        /// </summary>
        /// <returns></returns>
        public IOutputTemplateFactory CreateOutputTemplateFactory()
        {
            IList<string> filePaths = _directoryWrapper.EnumerateFiles(_xmlPath).ToList();

            if (filePaths.Count > 1)
            {
                return CreateMultipleTestRunOutputTemplateFactory(filePaths);
            }
            else
            {
                TestRun testRun = _testRunResultParser.ParseXml(_xmlPath);
                return new TestRunOutputTemplateFactory(testRun);
            }
        }

        private MultipleTestRunOutputTemplateFactory CreateMultipleTestRunOutputTemplateFactory(IList<string> filePaths)
        {
            var testRuns = CreateTestRuns(filePaths);
            return new MultipleTestRunOutputTemplateFactory(testRuns);
        }

        internal List<TestRun> CreateTestRuns(IList<string> filePaths)
        {
            var testRuns = new List<TestRun>();

            for (int i = 0; i < filePaths.Count; i++)
            {
                string filePath = filePaths[i];
                TestRun testRun = _testRunResultParser.ParseXml(filePath);
                testRuns.Add(testRun);
            }

            return testRuns;
        }
    }
}
