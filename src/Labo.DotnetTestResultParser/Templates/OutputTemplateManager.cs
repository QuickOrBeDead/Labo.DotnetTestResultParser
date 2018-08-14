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
    public sealed class OutputTemplateManager
    {
        private readonly string _xmlPath;

        private readonly UnitTestResultXmlFormat _format;

        /// <summary>
        /// Initializes a new instance of the <see cref="OutputTemplateManager"/> class.
        /// </summary>
        /// <param name="xmlPath">The XML path.</param>
        /// <param name="format">The format.</param>
        public OutputTemplateManager(string xmlPath, UnitTestResultXmlFormat format)
        {
            if (string.IsNullOrWhiteSpace(xmlPath))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(xmlPath));
            }

            _xmlPath = xmlPath;
            _format = format;
        }

        /// <summary>
        /// Creates the output template factory.
        /// </summary>
        /// <returns></returns>
        public IOutputTemplateFactory CreateOutputTemplateFactory()
        {
            // TODO: Add MultipleTestRunOutputTemplateFactory for wildcard path that results more than one file. Directory.EnumerateFiles(_xmlPath)

            TestRunResultParser unitTestRunResultParser = new TestRunResultParser(_format);
            TestRun testRun = unitTestRunResultParser.ParseXml(_xmlPath);

            return new TestRunOutputTemplateFactory(testRun);
        }
    }
}
