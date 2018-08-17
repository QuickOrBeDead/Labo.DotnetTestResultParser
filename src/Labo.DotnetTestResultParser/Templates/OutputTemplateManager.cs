using System.Collections.Generic;
using System.Linq;

namespace Labo.DotnetTestResultParser.Templates
{
    using System;
    using System.IO;

    using Labo.DotnetTestResultParser.Model;
    using Labo.DotnetTestResultParser.Parsers;
    using Labo.DotnetTestResultParser.Templates.Factory;

    /// <summary>
    /// The output template manager class.
    /// </summary>
    public sealed partial class OutputTemplateManager
    {
        private readonly string _xmlPath;

        private readonly ITestRunResultParser _testRunResultParser;
        private readonly IDirectoryWrapper _directoryWrapper;

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
        public OutputTemplateManager(string xmlPath, ITestRunResultParser runResultParser, IDirectoryWrapper directoryWrapper)
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
        /// Creates the output template factory.
        /// </summary>
        /// <returns></returns>
        public IOutputTemplateFactory CreateOutputTemplateFactory()
        {
            var isDirectory = _directoryWrapper.IsDirectory(_xmlPath);
            
            if (isDirectory)
            {
                var filePaths = _directoryWrapper.EnumerateFiles(_xmlPath);
                return CreateMultipleTestRunOutputTemplateFactory(filePaths);
            }
            else
            {
                var testRun = _testRunResultParser.ParseXml(_xmlPath);
                return new TestRunOutputTemplateFactory(testRun);
            }
        }

        private IOutputTemplateFactory CreateMultipleTestRunOutputTemplateFactory(IEnumerable<string> filePaths)
        {
            var testRuns = CreateTestRuns(filePaths);
            return new MultipleTestRunOutputTemplateFactory(testRuns);
        }

        internal IEnumerable<TestRun> CreateTestRuns(IEnumerable<string> filePaths)
        {
            IList<TestRun> testRuns = new List<TestRun>();

            foreach (var filePath in filePaths)
            {
                var testRun = _testRunResultParser.ParseXml(filePath);
                testRuns.Add(testRun);
            }

            return testRuns;
        }
    }
}
