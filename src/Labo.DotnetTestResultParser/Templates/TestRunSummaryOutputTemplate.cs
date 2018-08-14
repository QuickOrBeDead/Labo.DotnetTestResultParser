namespace Labo.DotnetTestResultParser.Templates
{
    using System;

    using Labo.DotnetTestResultParser.Model;
    using Labo.DotnetTestResultParser.Writers;

    /// <summary>
    /// The summary output template class.
    /// </summary>
    public sealed class TestRunSummaryOutputTemplate : IOutputTemplate
    {
        private readonly TestRun _testRun;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestRunSummaryOutputTemplate"/> class.
        /// </summary>
        /// <param name="testRun">The test run.</param>
        /// <exception cref="ArgumentNullException">testRun</exception>
        public TestRunSummaryOutputTemplate(TestRun testRun)
        {
            _testRun = testRun ?? throw new ArgumentNullException(nameof(testRun));
        }

        /// <inheritdoc />
        public void Write(ITestResultsOutputWriter outputWriter)
        {
            if (outputWriter == null)
            {
                throw new ArgumentNullException(nameof(outputWriter));
            }

            outputWriter.WriteLine("Total tests: {0}. Passed: {1}. Failed: {2}. Skipped: {3}.", _testRun.Total, _testRun.Passed, _testRun.Failed, _testRun.Skipped);
            outputWriter.WriteLine("Test Run {0}.", _testRun.Result);
        }
    }
}