namespace Labo.DotnetTestResultParser.Tests
{
    using System.Xml.Linq;

    using NUnit.Framework;

    [TestFixture]
    public class TestResultsParserFixture
    {
        [Test]
        public void Parse()
        {
            // Arrange
            string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><test-run id=\"2\" duration=\"2.9130068999999832\" testcasecount=\"150\" total=\"6\" passed=\"3\" failed=\"2\" skipped=\"1\" result=\"Failed\" start-time=\"2018-08-10T 13:16:57Z\" end-time=\"2018-08-10T 13:17:02Z\"></test-run>";

            // Act
            TestRun testRun = TestResultsParser.Parse(XDocument.Parse(xml));

            // Assert
            Assert.AreEqual("Failed", testRun.Result);
            Assert.AreEqual(6, testRun.Total);
            Assert.AreEqual(3, testRun.Passed);
            Assert.AreEqual(2, testRun.Failed);
            Assert.AreEqual(1, testRun.Skipped);
        }
    }
}
