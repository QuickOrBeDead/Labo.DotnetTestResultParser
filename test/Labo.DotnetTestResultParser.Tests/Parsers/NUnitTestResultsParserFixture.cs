namespace Labo.DotnetTestResultParser.Tests.Parsers
{
    using System;
    using System.Xml.Linq;

    using Labo.DotnetTestResultParser.Exceptions;
    using Labo.DotnetTestResultParser.Model;
    using Labo.DotnetTestResultParser.Parsers;

    using NUnit.Framework;

    [TestFixture]
    public class NUnitTestResultsParserFixture
    {
        [Test]
        public void Parse_ShouldThrowArgumentNull_WhenXDocumentIsNull()
        {
            // Arrange - Act
            ArgumentNullException argumentNullException = Assert.Throws<ArgumentNullException>(() => NUnitTestResultsParser.Parse(null));

            // Assert
            Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: xmlDocument", argumentNullException.Message);
        }

        [Test]
        public void Parse()
        {
            // Arrange
            string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><test-run id=\"2\" duration=\"2.9130068999999832\" testcasecount=\"150\" total=\"6\" passed=\"3\" failed=\"2\" skipped=\"1\" result=\"Failed\" start-time=\"2018-08-10T 13:16:57Z\" end-time=\"2018-08-10T 13:17:02Z\"></test-run>";

            // Act
            TestRun testRun = NUnitTestResultsParser.Parse(XDocument.Parse(xml));

            // Assert
            AssertTestRun(testRun, 6, 3, 2, 1, "Failed", false, "2");
        }

        [Test]
        public void ParseXml()
        {
            // Arrange
            string path = XmlPathUtility.GetTestXmlPath("Organon.ExceptionHandling.AspNetCore.Tests.unittest.xml");
            NUnitTestResultsParser testResultsParser = new NUnitTestResultsParser();

            // Act
            TestRun testRun = testResultsParser.ParseXml(path);

            // Assert
            AssertTestRun(testRun, 9, 8, 1, 0, "Failed", false, "2");
        }

        [Test]
        public void GetAttributeValue_ShouldThrowException_WhenAttributeNotFound()
        {
            // Arrange
            string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><test-run total=\"6\" passed=\"3\" failed=\"2\" skipped=\"1\" result=\"Failed\"></test-run>";
            XDocument xDocument = XDocument.Parse(xml);
            
            // Act
            TestResultParserException testResultParserException = Assert.Throws<TestResultParserException>(() => NUnitTestResultsParser.GetAttributeValue(xDocument.Root, "notfound"));

            // Assert
            Assert.AreEqual("Attribute 'notfound' could not be found for the xml element 'test-run'.", testResultParserException.Message);
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
