using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Labo.DotnetTestResultParser.Model;
using Labo.DotnetTestResultParser.Parsers;
using Labo.DotnetTestResultParser.Templates;
using Labo.DotnetTestResultParser.Templates.Factory;
using Labo.DotnetTestResultParser.Tests.Parsers;
using NSubstitute;
using NUnit.Framework;

namespace Labo.DotnetTestResultParser.Tests
{
    [TestFixture]
    public class OutputTemplateManagerFixture
    {
        private IDirectoryWrapper _directoryWrapper;
        private ITestRunResultParser _testRunResultParser;

        [SetUp]
        public void SetUp()
        {
            _directoryWrapper = Substitute.For<IDirectoryWrapper>();
            _testRunResultParser = Substitute.For<ITestRunResultParser>();
        }

        [Test]
        public void CreateOutputTemplateFactory_WhenThereIsMoreThanOneFileInPath_ShouldReturnMultipleTestRunOutputTemplateFactory()
        {
            //arrange
            string xmlPath = "/test/*.xml";
            _directoryWrapper.IsDirectory(xmlPath).Returns(true);
            var outputTemplateManager = CreateOutputTemplateManager(xmlPath);
            _testRunResultParser.ParseXml(xmlPath).Returns(new TestRun());

            //act
            var outputTemplateFactory = outputTemplateManager.CreateOutputTemplateFactory();

            //assert
            Assert.AreEqual(typeof(MultipleTestRunOutputTemplateFactory), outputTemplateFactory.GetType());
        }

        [Test]
        public void CreateOutputTemplateFactory_WhenThereIsOneFileInPath_ShouldReturnMultipleTestRunOutputTemplateFactory()
        {
            //arrange
            string xmlPath = "/test/test.xml";
            _directoryWrapper.EnumerateFiles(xmlPath).Returns(new List<string>() { xmlPath });
            _testRunResultParser.ParseXml(xmlPath).Returns(new TestRun());
            OutputTemplateManager outputTemplateManager = CreateOutputTemplateManager(xmlPath);

            //act
            var outputTemplateFactory = outputTemplateManager.CreateOutputTemplateFactory();

            //assert
            Assert.AreEqual(outputTemplateFactory.GetType(), typeof(TestRunOutputTemplateFactory));
        }

        [Test]
        public void CreateTestRuns_WhenThereAreTwoFilePaths_ShouldTwoTestRun()
        {
            //arrange
            OutputTemplateManager outputTemplateManager = CreateOutputTemplateManager();
            var filePaths = new List<string>() { "a", "b" };

            //act
            var testRuns = outputTemplateManager.CreateTestRuns(filePaths);

            //arrange
            Assert.AreEqual(2, testRuns.Count());
        }

        [Test]
        public void CreateTestRuns_WhenThereIsOneFilePath_ShouldTestRunResultParserReceivedOnce()
        {
            //arrange
            OutputTemplateManager outputTemplateManager = CreateOutputTemplateManager();
            var filePath = "a";
            var filePaths = new List<string>() { filePath };

            //act
            var testRuns = outputTemplateManager.CreateTestRuns(filePaths);

            //arrange
            _testRunResultParser.Received(1).ParseXml(filePath);
        }

        [Test]
        public void CreateOutputTemplateFactory_WhenThereAreMultipleFileInDirectory_ShouldReturnMultipleTestRunOutputTemplateFactory()
        {
            //arrange
            var path =  XmlPathUtility.GetTestXmlFolderPath();
            _directoryWrapper = new DirectoryWrapper();
            OutputTemplateManager outputTemplateManager = CreateOutputTemplateManager(path);

            //act
            IOutputTemplateFactory factory = outputTemplateManager.CreateOutputTemplateFactory();
            
            //assert
            Assert.AreEqual(typeof(MultipleTestRunOutputTemplateFactory), factory.GetType());
        }
        
        private OutputTemplateManager CreateOutputTemplateManager(string xmlPath = "a")
        {
            OutputTemplateManager outputTemplateManager =
                new OutputTemplateManager(xmlPath, _testRunResultParser, _directoryWrapper);
            return outputTemplateManager;
        }
    }
}
