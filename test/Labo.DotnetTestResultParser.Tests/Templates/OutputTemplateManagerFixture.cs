namespace Labo.DotnetTestResultParser.Tests.Templates
{
    using System.Collections.Generic;

    using Labo.DotnetTestResultParser.IO;
    using Labo.DotnetTestResultParser.Model;
    using Labo.DotnetTestResultParser.Parsers;
    using Labo.DotnetTestResultParser.Templates;
    using Labo.DotnetTestResultParser.Templates.Factory;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class OutputTemplateManagerFixture
    {
        private IFileSystemManager _fileSystemManager;
        private ITestRunResultParser _testRunResultParser;

        [SetUp]
        public void SetUp()
        {
            _fileSystemManager = Substitute.For<IFileSystemManager>();
            _testRunResultParser = Substitute.For<ITestRunResultParser>();
        }

        [Test]
        public void CreateOutputTemplateFactory_WhenThereIsMoreThanOneFileInPath_ShouldReturnMultipleTestRunOutputTemplateFactory()
        {
            // Arrange
            string xmlPath = "/test/*.xml";
            _fileSystemManager.EnumerateFiles(xmlPath).Returns(new List<string>{ "1.xml", "2.xml" });
            var outputTemplateManager = CreateOutputTemplateManager(xmlPath);
            _testRunResultParser.ParseXml(xmlPath).Returns(new TestRun());

            // Act
            var outputTemplateFactory = outputTemplateManager.CreateOutputTemplateFactory();

            // Assert
            Assert.AreEqual(typeof(MultipleTestRunOutputTemplateFactory), outputTemplateFactory.GetType());
        }

        [Test]
        public void CreateOutputTemplateFactory_WhenThereIsOneFileInPath_ShouldReturnMultipleTestRunOutputTemplateFactory()
        {
            // Arrange
            string xmlPath = "/test/test.xml";
            _fileSystemManager.EnumerateFiles(xmlPath).Returns(new List<string> { xmlPath });
            _testRunResultParser.ParseXml(xmlPath).Returns(new TestRun());
            OutputTemplateManager outputTemplateManager = CreateOutputTemplateManager(xmlPath);

            // Act
            var outputTemplateFactory = outputTemplateManager.CreateOutputTemplateFactory();

            // Assert
            Assert.AreEqual(outputTemplateFactory.GetType(), typeof(TestRunOutputTemplateFactory));
        }

        [Test]
        public void CreateTestRuns_WhenThereAreTwoFilePaths_ShouldTwoTestRun()
        {
            // Arrange
            OutputTemplateManager outputTemplateManager = CreateOutputTemplateManager();
            var filePaths = new List<string> { "a", "b" };

            // Act
            var testRuns = outputTemplateManager.CreateTestRuns(filePaths);

            // Arrange
            Assert.AreEqual(2, testRuns.Count);
        }

        [Test]
        public void CreateTestRuns_WhenThereIsOneFilePath_ShouldTestRunResultParserReceivedOnce()
        {
            // Arrange
            OutputTemplateManager outputTemplateManager = CreateOutputTemplateManager();
            var filePath = "a";
            var filePaths = new List<string> { filePath };

            // Act
            var testRuns = outputTemplateManager.CreateTestRuns(filePaths);

            // Arrange
            _testRunResultParser.Received(1).ParseXml(filePath);
        }

        [Test]
        public void CreateOutputTemplateFactory_WhenThereAreMultipleFileInDirectory_ShouldReturnMultipleTestRunOutputTemplateFactory()
        {
            // Arrange
            var path =  XmlPathUtility.GetTestXmlFolderPath();
            _fileSystemManager = new DefaultFileSystemManager();
            OutputTemplateManager outputTemplateManager = CreateOutputTemplateManager(path);

            // Act
            IOutputTemplateFactory factory = outputTemplateManager.CreateOutputTemplateFactory();
            
            // Assert
            Assert.AreEqual(typeof(MultipleTestRunOutputTemplateFactory), factory.GetType());
        }
        
        private OutputTemplateManager CreateOutputTemplateManager(string xmlPath = "a")
        {
            OutputTemplateManager outputTemplateManager =
                new OutputTemplateManager(xmlPath, _testRunResultParser, _fileSystemManager);
            return outputTemplateManager;
        }
    }
}
