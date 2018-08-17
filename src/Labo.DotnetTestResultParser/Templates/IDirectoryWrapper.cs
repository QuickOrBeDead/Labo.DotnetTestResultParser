using System.Collections.Generic;

namespace Labo.DotnetTestResultParser.Templates
{
    /// <summary>
    /// .net Directory Wrapper
    /// </summary>
    public interface IDirectoryWrapper
    {
        /// <summary>
        /// Enumerates the files.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        IEnumerable<string> EnumerateFiles(string path);

        /// <summary>
        /// Determines whether the specified path is directory.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>
        ///   <c>true</c> if the specified path is directory; otherwise, <c>false</c>.
        /// </returns>
        bool IsDirectory(string path);
    }
}
