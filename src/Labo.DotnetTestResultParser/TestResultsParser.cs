namespace Labo.DotnetTestResultParser
{
    using System;
    using System.Globalization;
    using System.Xml.Linq;

    /// <summary>
    /// The test results parser class.
    /// </summary>
    public sealed class TestResultsParser : ITestResultsParser
    {
        /// <summary>
        /// Parses the text result XML.
        /// </summary>
        /// <param name="xmlPath">The XML path.</param>
        public TestRun ParseXml(string xmlPath)
        {
            XDocument xmlDocument = XDocument.Load(xmlPath);
            return Parse(xmlDocument);
        }

        internal static TestRun Parse(XDocument xmlDocument)
        {
            if (xmlDocument == null)
            {
                throw new ArgumentNullException(nameof(xmlDocument));
            }

            XElement xmlDocumentRoot = xmlDocument.Root;
            string result = GetAttributeValue(xmlDocumentRoot, "result");
            int total = Convert.ToInt32(GetAttributeValue(xmlDocumentRoot, "total"), CultureInfo.InvariantCulture);
            int passed = Convert.ToInt32(GetAttributeValue(xmlDocumentRoot, "passed"), CultureInfo.InvariantCulture);
            int failed = Convert.ToInt32(GetAttributeValue(xmlDocumentRoot, "failed"), CultureInfo.InvariantCulture);
            int skipped = Convert.ToInt32(GetAttributeValue(xmlDocumentRoot, "skipped"), CultureInfo.InvariantCulture);

            return new TestRun
                       {
                           Result = result,
                           Total = total,
                           Skipped = skipped,
                           Passed = passed,
                           Failed = failed
                       };
        }

        private static string GetAttributeValue(XElement xElement, string name)
        {
            XAttribute attribute = xElement.Attribute(name);
            if (attribute == null)
            {
                throw new NullReferenceException();
            }

            return attribute.Value;
        }
    }
}
