namespace Labo.DotnetTestResultParser.Tests.Factory
{
    using System;

    using Labo.DotnetTestResultParser.Model;
    using Labo.DotnetTestResultParser.Parsers;
    using Labo.DotnetTestResultParser.Parsers.Factory;

    using NUnit.Framework;

    [TestFixture]
    public class DefaultTestResultsParserFactoryFixture
    {
        [Test]
        public void Create_ShouldThrowArgumentOutOfRange_WhenUnitTestResultXmlFormatIsNone()
        {
            // Arrange
            DefaultTestResultsParserFactory defaultTestResultsParserFactory = new DefaultTestResultsParserFactory();

            // Act
            ArgumentOutOfRangeException argumentOutOfRangeException = Assert.Throws<ArgumentOutOfRangeException>(() => defaultTestResultsParserFactory.CreateParser(UnitTestResultXmlFormat.None));

            // Assert
            Assert.AreEqual($"Exception of type 'System.ArgumentOutOfRangeException' was thrown. (Parameter 'unitTestResultXmlFormat'){Environment.NewLine}Actual value was None.", argumentOutOfRangeException.Message);
        }

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
