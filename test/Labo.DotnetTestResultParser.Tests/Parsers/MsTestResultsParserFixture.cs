namespace Labo.DotnetTestResultParser.Tests.Parsers
{
    using System;
    using System.Xml.Linq;

    using Labo.DotnetTestResultParser.Model;
    using Labo.DotnetTestResultParser.Parsers;

    using NUnit.Framework;

    [TestFixture]
    public class MsTestResultsParserFixture
    {
        [Test]
        public void Parse_ShouldThrowArgumentNull_WhenXDocumentIsNull()
        {
            // Arrange - Act
            ArgumentNullException argumentNullException = Assert.Throws<ArgumentNullException>(() => MsTestResultsParser.Parse(null));

            // Assert
            Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: xmlDocument", argumentNullException.Message);
        }

        [Test]
        public void Parse()
        {
            // Arrange
            string xml = XmlPathUtility.GetTestXmlContent("MSTest.xml");

            // Act
            TestRun testRun = MsTestResultsParser.Parse(XDocument.Parse(xml));

            // Assert
            AssertTestRun(testRun, 12, 4, 6, 2, "Failed", false, "BoraA@DT167 2018-09-23 19:48:07");
        }

        [Test]
        public void ParseXml()
        {
            // Arrange
            string path = XmlPathUtility.GetTestXmlPath("MSTest.xml");
            MsTestResultsParser testResultsParser = new MsTestResultsParser();

            // Act
            TestRun testRun = testResultsParser.ParseXml(path);

            // Assert
            AssertTestRun(testRun, 12, 4, 6, 2, "Failed", false, "BoraA@DT167 2018-09-23 19:48:07");
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
