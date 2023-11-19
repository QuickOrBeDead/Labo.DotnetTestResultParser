namespace Labo.DotnetTestResultParser.Tests.Parsers
{
    using System;
    using System.Xml.Linq;

    using Labo.DotnetTestResultParser.Model;
    using Labo.DotnetTestResultParser.Parsers;

    using NUnit.Framework;

    [TestFixture]
    public class XUnit2TestResultsParserFixture
    {
        [Test]
        public void Parse_ShouldThrowArgumentNull_WhenXDocumentIsNull()
        {
            // Arrange - Act
            ArgumentNullException argumentNullException = Assert.Throws<ArgumentNullException>(() => XUnit2TestResultsParser.Parse(null));

            // Assert
            Assert.AreEqual("Value cannot be null. (Parameter 'xmlDocument')", argumentNullException.Message);
        }

        [Test]
        public void Parse()
        {
            // Arrange
            string xml = XmlPathUtility.GetTestXmlContent("XUnit2.xml");

            // Act
            TestRun testRun = XUnit2TestResultsParser.Parse(XDocument.Parse(xml));

            // Assert
            AssertTestRun(testRun, 12, 4, 6, 2, "Failed", false, "XUnit 09/23/2018 19:57:53");
        }

        [Test]
        public void ParseXml()
        {
            // Arrange
            string path = XmlPathUtility.GetTestXmlPath("XUnit2.xml");
            XUnit2TestResultsParser testResultsParser = new XUnit2TestResultsParser();

            // Act
            TestRun testRun = testResultsParser.ParseXml(path);

            // Assert
            AssertTestRun(testRun, 12, 4, 6, 2, "Failed", false, "XUnit 09/23/2018 19:57:53");
        }

        private static void AssertTestRun(TestRun testRun, int total, int passed, int failed, int skipped, string result, bool isSuccess, string name)
        {
            Assert.AreEqual(result, testRun.Result);
            Assert.AreEqual(total, testRun.Total);
            Assert.AreEqual(passed, testRun.Passed);
            Assert.AreEqual(failed, testRun.Failed);
            Assert.AreEqual(skipped, testRun.Skipped);
            Assert.AreEqual(isSuccess, testRun.IsSuccess);
            Assert.AreEqual(name, testRun.Name);
        }
    }
}
