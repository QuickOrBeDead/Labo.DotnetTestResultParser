namespace Labo.DotnetTestResultParser.IO
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// The default file system manager class.
    /// </summary>
    /// <seealso cref="IFileSystemManager" />
    internal sealed class DefaultFileSystemManager : IFileSystemManager
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

        /// <inheritdoc />
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