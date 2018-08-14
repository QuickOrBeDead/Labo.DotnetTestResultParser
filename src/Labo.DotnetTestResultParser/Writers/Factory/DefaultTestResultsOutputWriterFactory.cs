namespace Labo.DotnetTestResultParser.Writers.Factory
{
    using System;
    using System.IO;

    /// <summary>
    /// The default test results output writer factory class.
    /// </summary>
    /// <seealso cref="ITestResultsOutputWriterFactory" />
    public sealed class DefaultTestResultsOutputWriterFactory : ITestResultsOutputWriterFactory
    {
        /// <inheritdoc />
        public ITestResultsOutputWriter Create(string output)
        {
            return output == null ? new TextWriterTestResultsOutputWriter(Console.Out) : new TextWriterTestResultsOutputWriter(File.CreateText(output));
        }
    }
}
