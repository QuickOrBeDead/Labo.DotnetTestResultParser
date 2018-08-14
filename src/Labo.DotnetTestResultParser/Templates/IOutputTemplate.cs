namespace Labo.DotnetTestResultParser.Templates
{
    using Labo.DotnetTestResultParser.Writers;

    /// <summary>
    /// The output template interface.
    /// </summary>
    public interface IOutputTemplate
    {
        /// <summary>
        /// Writes the specified output writer.
        /// </summary>
        /// <param name="outputWriter">The output writer.</param>
        void Write(ITestResultsOutputWriter outputWriter);
    }
}
