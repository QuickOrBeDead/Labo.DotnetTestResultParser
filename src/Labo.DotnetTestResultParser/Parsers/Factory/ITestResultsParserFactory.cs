namespace Labo.DotnetTestResultParser.Parsers.Factory
{
    using Labo.DotnetTestResultParser.Model;

    /// <summary>
    /// The test results parser factory interface.
    /// </summary>
    public interface ITestResultsParserFactory
    {
        /// <summary>
        /// Creates the parser.
        /// </summary>
        /// <param name="unitTestResultXmlFormat">The unit test result XML format.</param>
        /// <returns></returns>
        ITestResultsParser CreateParser(UnitTestResultXmlFormat unitTestResultXmlFormat);
    }
}
