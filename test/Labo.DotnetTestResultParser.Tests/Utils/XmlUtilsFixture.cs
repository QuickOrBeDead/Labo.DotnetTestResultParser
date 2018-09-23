namespace Labo.DotnetTestResultParser.Tests.Utils
{
    using System.Xml.Linq;

    using Labo.DotnetTestResultParser.Exceptions;
    using Labo.DotnetTestResultParser.Utils;

    using NUnit.Framework;

    [TestFixture]
    public class XmlUtilsFixture
    {
        [Test]
        public void GetAttributeValue_ShouldThrowException_WhenAttributeNotFound()
        {
            // Arrange
            string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><test-run total=\"6\" passed=\"3\" failed=\"2\" skipped=\"1\" result=\"Failed\"></test-run>";
            XDocument xDocument = XDocument.Parse(xml);

            // Act
            TestResultParserException testResultParserException = Assert.Throws<TestResultParserException>(() => XmlUtils.GetAttributeValue(xDocument.Root, "notfound"));

            // Assert
            Assert.AreEqual("Attribute 'notfound' could not be found for the xml element 'test-run'.", testResultParserException.Message);
        }
    }
}
