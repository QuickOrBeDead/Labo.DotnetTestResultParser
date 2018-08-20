namespace Labo.DotnetTestResultParser.Tests.Writers
{
    using System;
    using System.IO;
    using System.Text;

    using Labo.DotnetTestResultParser.Writers;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class TextWriterTestResultsOutputWriterFixture
    {
        public sealed class TextWriterStub : TextWriter
        {
            private readonly ITestResultsOutputWriter _writer;

            /// <inheritdoc />
            public override Encoding Encoding => Encoding.UTF8;

            public TextWriterStub(ITestResultsOutputWriter writer)
            {
                _writer = writer ?? throw new ArgumentNullException(nameof(writer));
            }

            /// <inheritdoc />
            public override void WriteLine(string format, params object[] arg)
            {
                _writer.WriteLine(format, arg);
            }

            /// <inheritdoc />
            public override void Write(string format, params object[] arg)
            {
                _writer.Write(format, arg);
            }

            /// <inheritdoc />
            protected override void Dispose(bool disposing)
            {
                _writer.Dispose();
            }
        }

        [Test]
        public void Write()
        {
            // Arrange
            ITestResultsOutputWriter writer = Substitute.For<ITestResultsOutputWriter>();
            string text = "Test Result: {0}. Passed: {1}";
            object[] args = { "Passed", 10 };

            using (TextWriterTestResultsOutputWriter testResultsOutputWriter = new TextWriterTestResultsOutputWriter(new TextWriterStub(writer)))
            {
                // Act
                testResultsOutputWriter.Write(text, args);

                // Assert
                writer.Received(1).Write(text, args);
            }

            // Assert
            writer.Received(1).Dispose();
        }

        [Test]
        public void WriteLine()
        {
            // Arrange
            ITestResultsOutputWriter writer = Substitute.For<ITestResultsOutputWriter>();
            string text = "Test Result: {0}. Passed: {1}";
            object[] args = { "Passed", 10 };

            using (TextWriterTestResultsOutputWriter testResultsOutputWriter = new TextWriterTestResultsOutputWriter(new TextWriterStub(writer)))
            {
                // Act
                testResultsOutputWriter.WriteLine(text, args);

                // Assert
                writer.Received(1).WriteLine(text, args);
            }

            // Assert
            writer.Received(1).Dispose();
        }
    }
}
