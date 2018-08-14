namespace Labo.DotnetTestResultParser.Writers.Factory
{
    /// <summary>
    /// The test results output writer factory interface.
    /// </summary>
    public interface ITestResultsOutputWriterFactory
    {
        /// <summary>
        /// Creates the specified output writer.
        /// </summary>
        /// <param name="output">The output.</param>
        /// <returns></returns>
        ITestResultsOutputWriter Create(string output);
    }
}