namespace Labo.DotnetTestResultParser.Parsers
{
    using System;
    using System.Globalization;
    using System.Xml.Linq;

    using Labo.DotnetTestResultParser.Model;

    /// <summary>
    /// The nunit test results parser class.
    /// </summary>
    /// <seealso cref="ITestResultsParser" />
    public sealed class NUnitTestResultsParser : ITestResultsParser
    {
        /// <inheritdoc />
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
            string name = GetAttributeValue(xmlDocumentRoot, "id");

            return new TestRun
                       {
                           Result = result,
                           Total = total,
                           Skipped = skipped,
                           Passed = passed,
                           Failed = failed,
                           IsSuccess = string.Equals("Passed", result, StringComparison.OrdinalIgnoreCase),
                           Name = name
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