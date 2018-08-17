using System.IO;
using System.Reflection;

namespace Labo.DotnetTestResultParser.Tests.Parsers
{
    public class XmlPathUtility
    {
        public static string GetTestXmlPath(string xmlPath)
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "_testresultxmls", xmlPath);
        }

        public static string GetTestXmlFolderPath()
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "_testresultmultiplexmls");
        }
    }
}