namespace Labo.DotnetTestResultParser
{
    using Labo.DotnetTestResultParser.Model;
    using Labo.DotnetTestResultParser.Parsers;
    using Labo.DotnetTestResultParser.Parsers.Factory;

    /// <summary>
    /// The test results parser class.
    /// </summary>
    public sealed class UnitTestResultParser
    {
        private readonly ITestResultsParser _testResultsParser;

        /// <summary>
        /// Gets the format.
        /// </summary>
        /// <value>
        /// The format.
        /// </value>
        public UnitTestResultXmlFormat Format { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitTestResultParser"/> class.
        /// </summary>
        /// <param name="unitTestResultXmlFormat">The unit test result XML format.</param>
        public UnitTestResultParser(UnitTestResultXmlFormat unitTestResultXmlFormat)
            : this(new DefaultTestResultsParserFactory(), unitTestResultXmlFormat)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitTestResultParser"/> class.
        /// </summary>
        /// <param name="testResultsParserFactory">The test results parser factory.</param>
        /// <param name="unitTestResultXmlFormat">The unit test result XML format.</param>
        public UnitTestResultParser(ITestResultsParserFactory testResultsParserFactory, UnitTestResultXmlFormat unitTestResultXmlFormat)
        {
            Format = unitTestResultXmlFormat;
            _testResultsParser = testResultsParserFactory.CreateParser(unitTestResultXmlFormat);
        }

        /// <summary>
        /// Parses the text result XML.
        /// </summary>
        /// <param name="xmlPath">The XML path.</param>
        public TestRun ParseXml(string xmlPath)
        {
            return _testResultsParser.ParseXml(xmlPath);
        }
    }
}
