namespace Labo.DotnetTestResultParser.Parsers
{
    using Labo.DotnetTestResultParser.Model;

    /// <summary>
    /// The test results parser interface.
    /// </summary>
    public interface ITestResultsParser
    {
        /// <summary>
        /// Parses the text result XML.
        /// </summary>
        /// <param name="xmlPath">The XML path.</param>
        TestRun ParseXml(string xmlPath);
    }
}