using System.Linq;

using Labo.DotnetTestResultParser.Templates;

using NUnit.Framework;

namespace Labo.DotnetTestResultParser.Tests.Templates
{
    using System.Collections.Generic;
    using System.IO;

    [TestFixture]
    public class DirectoryWrapperFixture
    {
        private DirectoryWrapper _directoryWrapper;

        [SetUp]
        public void SetUp()
        {
            _directoryWrapper = new DirectoryWrapper();
        }

        [Test]
        public void EnumerateFiles_WhenThereAre2FilesInDirectory_ShouldListHas2Items()
        {
            //act
            var path = XmlPathUtility.GetTestXmlFolderPath();
            var fileList = _directoryWrapper.EnumerateFiles(path);

            //assert
            Assert.AreEqual(2, fileList.Count());
        }

        [Test]
        public void IsDirectory_WhenItIsADirectory_ShouldReturnTrue()
        {
            //act
            var path = XmlPathUtility.GetTestXmlFolderPath();
            var result =_directoryWrapper.IsDirectory(path);

            //assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsDirectory_WhenItIsAFile_ShouldReturnFalse()
        {
            //act
            var path = XmlPathUtility.GetTestXmlPath("Organon.ExceptionHandling.AspNetCore.Tests.unittest.xml");
            var result = _directoryWrapper.IsDirectory(path);

            //assert
            Assert.IsFalse(result);
        }

        [Test]
        public void EnumerateFiles_ShouldSearchFilesByWildcardPattern()
        {
            // Arrange
            string testXmlPath = XmlPathUtility.GetTestXmlPath("*.directorytest.xml");

            // Act
            IList<string> files = _directoryWrapper.EnumerateFiles(testXmlPath).ToList();
            
            // Assert
            Assert.AreEqual(2, files.Count);
            Assert.AreEqual(true, files.Contains(XmlPathUtility.GetTestXmlPath("1.directorytest.xml")));
            Assert.AreEqual(true, files.Contains(XmlPathUtility.GetTestXmlPath("2.directorytest.xml")));
        }

        [Test]
        public void EnumerateFiles_ShouldSearchFilesByExactName()
        {
            // Arrange
            string testXmlPath = XmlPathUtility.GetTestXmlPath("1.directorytest.xml");

            // Act
            IList<string> files = _directoryWrapper.EnumerateFiles(testXmlPath).ToList();

            // Assert
            Assert.AreEqual(1, files.Count);
            Assert.AreEqual(true, files.Contains(XmlPathUtility.GetTestXmlPath("1.directorytest.xml")));
        }
    }
}
