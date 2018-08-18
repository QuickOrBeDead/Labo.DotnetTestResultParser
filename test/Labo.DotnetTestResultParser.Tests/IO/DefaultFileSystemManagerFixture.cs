namespace Labo.DotnetTestResultParser.Tests.IO
{
    using System.Collections.Generic;
    using System.Linq;

    using Labo.DotnetTestResultParser.IO;

    using NUnit.Framework;

    [TestFixture]
    public class DefaultFileSystemManagerFixture
    {
        private DefaultFileSystemManager _fileSystemManager;

        [SetUp]
        public void SetUp()
        {
            _fileSystemManager = new DefaultFileSystemManager();
        }

        [Test]
        public void EnumerateFiles_WhenThereAre2FilesInDirectory_ShouldListHas2Items()
        {
            // Arrange
            var path = XmlPathUtility.GetTestXmlFolderPath();

            // Act
            var fileList = _fileSystemManager.EnumerateFiles(path);

            // Assert
            Assert.AreEqual(2, fileList.Count());
        }

        [Test]
        public void IsDirectory_WhenItIsADirectory_ShouldReturnTrue()
        {
            // Arrange
            var path = XmlPathUtility.GetTestXmlFolderPath();

            // Act
            var result =_fileSystemManager.IsDirectory(path);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsDirectory_WhenItIsAFile_ShouldReturnFalse()
        {
            // Arrange
            var path = XmlPathUtility.GetTestXmlPath("Organon.ExceptionHandling.AspNetCore.Tests.unittest.xml");

            // Act
            var result = _fileSystemManager.IsDirectory(path);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void EnumerateFiles_ShouldSearchFilesByWildcardPattern()
        {
            // Arrange
            string testXmlPath = XmlPathUtility.GetTestXmlPath("*.directorytest.xml");

            // Act
            IList<string> files = _fileSystemManager.EnumerateFiles(testXmlPath).ToList();
            
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
            IList<string> files = _fileSystemManager.EnumerateFiles(testXmlPath).ToList();

            // Assert
            Assert.AreEqual(1, files.Count);
            Assert.AreEqual(true, files.Contains(XmlPathUtility.GetTestXmlPath("1.directorytest.xml")));
        }
    }
}
