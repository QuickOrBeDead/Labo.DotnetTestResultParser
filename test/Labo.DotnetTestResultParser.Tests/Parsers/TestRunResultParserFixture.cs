namespace Labo.DotnetTestResultParser.Tests.Parsers
{
    using Labo.DotnetTestResultParser.Model;
    using Labo.DotnetTestResultParser.Parsers;
    using Labo.DotnetTestResultParser.Parsers.Factory;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class TestRunResultParserFixture
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
            TestRunResultParser unitTestResultParser = CreateUnitTestResultParser(testResultsParserFactory, unitTestResultXmlFormat);

            // Act
            TestRun testRun = unitTestResultParser.ParseXml(xmlPath);

            // Assert
            Assert.AreEqual(unitTestResultXmlFormat, unitTestResultParser.Format);
            Assert.AreSame(expectedTestRun, testRun);

            testResultsParserFactory.Received(1).CreateParser(unitTestResultXmlFormat);
            testResultsParser.Received(1).ParseXml(xmlPath);
        }

        [Test]
        public void ConstructorWithUnitTestResultXmlFormat_ShouldSetTestResultsParser()
        {
            // Arrange - Act
            TestRunResultParser testRunResultParser = new TestRunResultParser(UnitTestResultXmlFormat.NUnit);

            // Assert
            Assert.AreEqual(true, testRunResultParser.TestResultsParser is NUnitTestResultsParser);
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

        private static TestRunResultParser CreateUnitTestResultParser(ITestResultsParserFactory testResultsParserFactory, UnitTestResultXmlFormat unitTestResultXmlFormat)
        {
            TestRunResultParser unitTestResultParser = new TestRunResultParser(testResultsParserFactory, unitTestResultXmlFormat);
            return unitTestResultParser;
        }
    }
}
