namespace Labo.DotnetTestResultParser.Parsers.Factory
{
    using System;

    using Labo.DotnetTestResultParser.Model;

    /// <summary>
    /// The default test results parser factory class.
    /// </summary>
    /// <seealso cref="ITestResultsParserFactory" />
    public sealed class DefaultTestResultsParserFactory : ITestResultsParserFactory
    {
        /// <inheritdoc />
        public ITestResultsParser CreateParser(UnitTestResultXmlFormat unitTestResultXmlFormat)
        {
            switch (unitTestResultXmlFormat)
            {
                case UnitTestResultXmlFormat.NUnit:
                    return new NUnitTestResultsParser();
                case UnitTestResultXmlFormat.XUnit:
                    return new XUnit2TestResultsParser();
                default:
                    throw new ArgumentOutOfRangeException(nameof(unitTestResultXmlFormat), unitTestResultXmlFormat, null);
            }
        }
    }
}