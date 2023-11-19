namespace Labo.DotnetTestResultParser.Writers
{
    using System;
    using System.IO;

    /// <summary>
    /// The TextWriter test results output writer class.
    /// </summary>
    /// <seealso cref="Labo.DotnetTestResultParser.Writers.ITestResultsOutputWriter" />
    public sealed class TextWriterTestResultsOutputWriter : ITestResultsOutputWriter
    {
        private readonly TextWriter _textWriter;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextWriterTestResultsOutputWriter"/> class.
        /// </summary>
        /// <param name="textWriter">The text writer.</param>
        public TextWriterTestResultsOutputWriter(TextWriter textWriter)
        {
            _textWriter = textWriter ?? throw new ArgumentNullException(nameof(textWriter));
        }

        internal TextWriter Writer => _textWriter;

        /// <inheritdoc />
        public void Write(string text, params object[] args)
        {
            _textWriter.Write(text, args);
        }

        /// <inheritdoc />
        public void WriteLine(string text, params object[] args)
        {
            _textWriter.WriteLine(text, args);
        }

        #region IDisposable Support
        private bool _disposed; // To detect redundant calls

        void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects).
                    _textWriter.Dispose();
                }

                _disposed = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        /// <inheritdoc />
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}