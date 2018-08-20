namespace Labo.DotnetTestResultParser.Tests.Writers.Factory
{
    using System;
    using System.IO;
    using System.Reflection;

    using Labo.DotnetTestResultParser.Writers;
    using Labo.DotnetTestResultParser.Writers.Factory;

    using NUnit.Framework;

    [TestFixture]
    public class DefaultTestResultsOutputWriterFactoryFixture
    {
        [Test]
        public void Create_ShouldReturnConsoleOut_WhenOutputIsNull()
        {
            // Arrange
            DefaultTestResultsOutputWriterFactory defaultTestResultsOutputWriterFactory = new DefaultTestResultsOutputWriterFactory();
            string output = null;

            // Act
            using (ITestResultsOutputWriter testResultsOutputWriter = defaultTestResultsOutputWriterFactory.Create(output))
            {
                // Assert
                Assert.AreEqual(true, testResultsOutputWriter is TextWriterTestResultsOutputWriter);
                TextWriter textWriter = ((TextWriterTestResultsOutputWriter)testResultsOutputWriter).Writer;

                Assert.AreSame(Console.Out, textWriter);
            }
        }

        [Test]
        public void Create_ShouldReturnFileStreamWriter_WhenOutputIsNotNull()
        {
            // Arrange
            DefaultTestResultsOutputWriterFactory defaultTestResultsOutputWriterFactory = new DefaultTestResultsOutputWriterFactory();
            string output = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "output.txt"); ;

            // Act
            using (ITestResultsOutputWriter testResultsOutputWriter = defaultTestResultsOutputWriterFactory.Create(output))
            {
                // Assert
                Assert.AreEqual(true, testResultsOutputWriter is TextWriterTestResultsOutputWriter);
                TextWriter textWriter = ((TextWriterTestResultsOutputWriter)testResultsOutputWriter).Writer;

                Assert.AreEqual(true, textWriter is StreamWriter);
                object baseStream = textWriter.GetType().GetProperty("BaseStream").GetValue(textWriter);
                Assert.AreEqual(true, baseStream is FileStream);
                Assert.AreEqual(output, ((FileStream)baseStream).Name);
            }
        }
    }
}
