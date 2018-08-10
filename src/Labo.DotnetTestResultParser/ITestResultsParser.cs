namespace Labo.DotnetTestResultParser
{
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