namespace Labo.DotnetTestResultParser.Tests.Factory
{
    using Labo.DotnetTestResultParser.Model;
    using Labo.DotnetTestResultParser.Parsers;
    using Labo.DotnetTestResultParser.Parsers.Factory;

    using NUnit.Framework;

    [TestFixture]
    public class DefaultTestResultsParserFactoryFixture
    {
        [Test]
        public void Create()
        {
            // Arrange
            DefaultTestResultsParserFactory defaultTestResultsParserFactory = new DefaultTestResultsParserFactory();

            // Act
            ITestResultsParser testResultsParser = defaultTestResultsParserFactory.CreateParser(UnitTestResultXmlFormat.NUnit);

            // Assert
            Assert.AreEqual(true, testResultsParser is NUnitTestResultsParser);
        }
    }
}
