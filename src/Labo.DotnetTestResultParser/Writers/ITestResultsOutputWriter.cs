namespace Labo.DotnetTestResultParser.Writers
{
    using System;

    /// <summary>
    /// The test results output writer interface.
    /// </summary>
    public interface ITestResultsOutputWriter : IDisposable
    {
        /// <summary>
        /// Writes the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="args">The arguments.</param>
        void Write(string text, params object[] args);

        /// <summary>
        /// Writes the line.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="args">The arguments.</param>
        void WriteLine(string text, params object[] args);
    }
}
