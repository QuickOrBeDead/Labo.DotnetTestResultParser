using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Labo.DotnetTestResultParser.Templates;
using Labo.DotnetTestResultParser.Tests.Parsers;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Labo.DotnetTestResultParser.Tests.Templates
{
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
    }
}
