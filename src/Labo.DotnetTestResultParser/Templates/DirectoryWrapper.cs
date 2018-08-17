using System.Collections.Generic;
using System.IO;

namespace Labo.DotnetTestResultParser.Templates
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Labo.DotnetTestResultParser.Templates.IDirectoryWrapper" />
    internal class DirectoryWrapper : IDirectoryWrapper
    {
        /// <inheritdoc />
        public IEnumerable<string> EnumerateFiles(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(path));
            }

            string directoryName = Path.GetDirectoryName(path);
            string fileName = Path.GetFileName(path);

            if (!string.IsNullOrWhiteSpace(fileName) && fileName.Contains("*", StringComparison.InvariantCulture))
            {
                return Directory.EnumerateFiles(directoryName, fileName);
            }

            string extension = Path.GetExtension(fileName);
            if (string.IsNullOrWhiteSpace(extension))
            {
                return Directory.EnumerateFiles(path);
            }
            else
            {
                return Directory.EnumerateFiles(directoryName, fileName);
            }
          
        }

        public bool IsDirectory(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(path));
            }

            return Directory.Exists(path);
        }
    }
}