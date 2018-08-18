namespace Labo.DotnetTestResultParser.Parsers
{
    using Labo.DotnetTestResultParser.Model;

    /// <summary>
    /// The test runner parser
    /// </summary>
    public interface ITestRunResultParser
    {
        /// <summary>
        /// Parses the text result XML.
        /// </summary>
        /// <param name="xmlPath">The XML path.</param>
        TestRun ParseXml(string xmlPath);
    }
}