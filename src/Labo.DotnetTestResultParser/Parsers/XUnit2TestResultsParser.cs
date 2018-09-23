namespace Labo.DotnetTestResultParser.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Xml.Linq;

    using Labo.DotnetTestResultParser.Model;
    using Labo.DotnetTestResultParser.Utils;

    /// <summary>
    /// The xunit 2 test results parser class.
    /// </summary>
    /// <seealso cref="ITestResultsParser" />
    public sealed class XUnit2TestResultsParser : ITestResultsParser
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

            int total = 0;
            int passed = 0;
            int failed = 0;
            int skipped = 0;
            int errors = 0;

            IEnumerable<XElement> xElements = xmlDocumentRoot.Elements("assembly");
            foreach (XElement xElement in xElements)
            {
                total += Convert.ToInt32(XmlUtils.GetAttributeValue(xElement, "total"), CultureInfo.InvariantCulture);
                passed += Convert.ToInt32(XmlUtils.GetAttributeValue(xElement, "passed"), CultureInfo.InvariantCulture);
                failed += Convert.ToInt32(XmlUtils.GetAttributeValue(xElement, "failed"), CultureInfo.InvariantCulture);
                skipped += Convert.ToInt32(XmlUtils.GetAttributeValue(xElement, "skipped"), CultureInfo.InvariantCulture);
                errors += Convert.ToInt32(XmlUtils.GetAttributeValue(xElement, "errors"), CultureInfo.InvariantCulture);
            }

            string name = $"XUnit {XmlUtils.GetAttributeValue(xmlDocumentRoot, "timestamp")}";

            bool isSuccess = failed != 0 && errors != 0;
            return new TestRun
                       {
                           Result = isSuccess ? "Passed" : "Failed",
                           Total = total,
                           Skipped = skipped,
                           Passed = passed,
                           Failed = failed,
                           Errors = errors,
                           IsSuccess = isSuccess,
                           Name = name
                       };
        }
    }
}