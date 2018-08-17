using Labo.DotnetTestResultParser.Model;

namespace Labo.DotnetTestResultParser.Parsers
{
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