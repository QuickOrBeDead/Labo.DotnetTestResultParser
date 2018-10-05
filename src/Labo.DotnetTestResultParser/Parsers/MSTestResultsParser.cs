namespace Labo.DotnetTestResultParser.Parsers
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Xml.Linq;

    using Labo.DotnetTestResultParser.Model;
    using Labo.DotnetTestResultParser.Utils;

    /// <summary>
    /// The ms test results parser class.
    /// </summary>
    /// <seealso cref="Labo.DotnetTestResultParser.Parsers.ITestResultsParser" />
    public sealed class MsTestResultsParser : ITestResultsParser
    {
        private static readonly XNamespace Ns = @"http://microsoft.com/schemas/VisualStudio/TeamTest/2010";

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
            XElement resultSummaryElement = xmlDocument.Element(Ns + "TestRun").Element(Ns + "ResultSummary");
            string outcome = XmlUtils.GetAttributeValue(resultSummaryElement, "outcome");
            bool isSuccess = string.Equals(outcome, "Passed", StringComparison.OrdinalIgnoreCase);

            XElement countersElement = resultSummaryElement.Elements(Ns + "Counters").Single();

            int total = Convert.ToInt32(XmlUtils.GetAttributeValue(countersElement, "total"), CultureInfo.InvariantCulture);
            int passed = Convert.ToInt32(XmlUtils.GetAttributeValue(countersElement, "passed"), CultureInfo.InvariantCulture);
            int failed = Convert.ToInt32(XmlUtils.GetAttributeValue(countersElement, "failed"), CultureInfo.InvariantCulture);
            int skipped = Convert.ToInt32(XmlUtils.GetAttributeValue(countersElement, "notExecuted"), CultureInfo.InvariantCulture);
            int errors = Convert.ToInt32(XmlUtils.GetAttributeValue(countersElement, "error"), CultureInfo.InvariantCulture);

            string name = XmlUtils.GetAttributeValue(xmlDocumentRoot, "name");

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
