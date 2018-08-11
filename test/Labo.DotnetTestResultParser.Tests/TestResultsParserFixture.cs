namespace Labo.DotnetTestResultParser.Tests
{
    using System.IO;
    using System.Reflection;
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
            AssertTestRun(testRun, 6, 3, 2, 1, "Failed");
        }

        [Test]
        public void ParseXml()
        {
            // Arrange
            string path = GetTestXmlPath("Organon.ExceptionHandling.AspNetCore.Tests.unittest.xml");
            TestResultsParser testResultsParser = new TestResultsParser();

            // Act
            TestRun testRun = testResultsParser.ParseXml(path);

            // Assert
            AssertTestRun(testRun, 9, 8, 1, 0, "Failed");
        }

        private static string GetTestXmlPath(string xmlPath)
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "_testresultxmls", xmlPath);
        }

        private static void AssertTestRun(TestRun testRun, int total, int passed, int failed, int skipped, string result)
        {
            Assert.AreEqual(result, testRun.Result);
            Assert.AreEqual(total, testRun.Total);
            Assert.AreEqual(passed, testRun.Passed);
            Assert.AreEqual(failed, testRun.Failed);
            Assert.AreEqual(skipped, testRun.Skipped);
        }
    }
}
