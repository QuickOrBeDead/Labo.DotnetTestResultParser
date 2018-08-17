using System.Collections.Generic;
using System.IO;

namespace Labo.DotnetTestResultParser.Templates
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Labo.DotnetTestResultParser.Templates.IDirectoryWrapper" />
    internal class DirectoryWrapper : IDirectoryWrapper
    {
        /// <inheritdoc />
        public IEnumerable<string> EnumerateFiles(string path)
        {
            return Directory.EnumerateFiles(path);
        }

        public bool IsDirectory(string path)
        {
            return Directory.Exists(path);
        }
    }
}