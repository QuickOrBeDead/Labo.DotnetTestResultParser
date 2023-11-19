namespace Labo.DotnetTestResultParser.Parsers
{
    using System;
    using System.Globalization;
    using System.Xml.Linq;

    using Labo.DotnetTestResultParser.Model;
    using Labo.DotnetTestResultParser.Utils;

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
            ArgumentNullException.ThrowIfNull(xmlDocument);

            XElement xmlDocumentRoot = xmlDocument.Root;
            string result = XmlUtils.GetAttributeValue(xmlDocumentRoot, "result");
            int total = Convert.ToInt32(XmlUtils.GetAttributeValue(xmlDocumentRoot, "total"), CultureInfo.InvariantCulture);
            int passed = Convert.ToInt32(XmlUtils.GetAttributeValue(xmlDocumentRoot, "passed"), CultureInfo.InvariantCulture);
            int failed = Convert.ToInt32(XmlUtils.GetAttributeValue(xmlDocumentRoot, "failed"), CultureInfo.InvariantCulture);
            int skipped = Convert.ToInt32(XmlUtils.GetAttributeValue(xmlDocumentRoot, "skipped"), CultureInfo.InvariantCulture);
            string name = XmlUtils.GetAttributeValue(xmlDocumentRoot, "id");

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
    }
}