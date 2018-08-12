namespace Labo.DotnetTestResultParser.Tests
{
    using Labo.DotnetTestResultParser.Model;
    using Labo.DotnetTestResultParser.Parsers;
    using Labo.DotnetTestResultParser.Parsers.Factory;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class UnitTestResultParserFixture
    {
        [Test]
        public void ParseXml()
        {
            // Arrange
            string xmlPath = "/test/testresult.xml";
            TestRun expectedTestRun = new TestRun();
            UnitTestResultXmlFormat unitTestResultXmlFormat = UnitTestResultXmlFormat.NUnit;

            ITestResultsParser testResultsParser = CreateTestResultsParser(xmlPath, expectedTestRun);
            ITestResultsParserFactory testResultsParserFactory = CreateTestResultsParserFactory(unitTestResultXmlFormat, testResultsParser);
            UnitTestResultParser unitTestResultParser = CreateUnitTestResultParser(testResultsParserFactory, unitTestResultXmlFormat);

            // Act
            TestRun testRun = unitTestResultParser.ParseXml(xmlPath);

            // Assert
            Assert.AreEqual(unitTestResultXmlFormat, unitTestResultParser.Format);
            Assert.AreSame(expectedTestRun, testRun);

            testResultsParserFactory.Received(1).CreateParser(unitTestResultXmlFormat);
            testResultsParser.Received(1).ParseXml(xmlPath);
        }

        private static ITestResultsParserFactory CreateTestResultsParserFactory(UnitTestResultXmlFormat unitTestResultXmlFormat, ITestResultsParser testResultsParser)
        {
            ITestResultsParserFactory testResultsParserFactory = Substitute.For<ITestResultsParserFactory>();
            testResultsParserFactory.CreateParser(unitTestResultXmlFormat).Returns(testResultsParser);
            return testResultsParserFactory;
        }

        private static ITestResultsParser CreateTestResultsParser(string xmlPath, TestRun expectedTestRun)
        {
            ITestResultsParser testResultsParser = Substitute.For<ITestResultsParser>();
            testResultsParser.ParseXml(xmlPath).Returns(expectedTestRun);
            return testResultsParser;
        }

        private static UnitTestResultParser CreateUnitTestResultParser(ITestResultsParserFactory testResultsParserFactory, UnitTestResultXmlFormat unitTestResultXmlFormat)
        {
            UnitTestResultParser unitTestResultParser = new UnitTestResultParser(testResultsParserFactory, unitTestResultXmlFormat);
            return unitTestResultParser;
        }
    }
}
