namespace Labo.DotnetTestResultParser.Tests
{
    using System.IO;
    using System.Reflection;

    public static class XmlPathUtility
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