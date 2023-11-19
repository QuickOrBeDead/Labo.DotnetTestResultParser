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
        private readonly TestRun[] _testRun;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestRunSummaryOutputTemplate"/> class.
        /// </summary>
        /// <param name="testRuns">The test run.</param>
        /// <exception cref="ArgumentNullException">testRun</exception>
        public TestRunSummaryOutputTemplate(params TestRun[] testRuns)
        {
            _testRun = testRuns ?? throw new ArgumentNullException(nameof(testRuns));
        }

        /// <inheritdoc />
        public void Write(ITestResultsOutputWriter outputWriter)
        {
            ArgumentNullException.ThrowIfNull(outputWriter);

            for (int i = 0; i < _testRun.Length; i++)
            {
                TestRun testRun = _testRun[i];

                WriteLines(outputWriter, testRun);
            }
        }

        private static void WriteLines(ITestResultsOutputWriter outputWriter, TestRun testRun)
        {
            outputWriter.WriteLine("Test name : {0}", testRun.Name);
            outputWriter.WriteLine("Total tests: {0}. Passed: {1}. Failed: {2}. Skipped: {3}. Errors: {4}.", testRun.Total, testRun.Passed, testRun.Failed, testRun.Skipped, testRun.Errors);
            outputWriter.WriteLine("Test Run {0}.", testRun.Result);
        }
    }
}