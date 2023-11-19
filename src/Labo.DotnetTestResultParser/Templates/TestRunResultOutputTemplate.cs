namespace Labo.DotnetTestResultParser.Templates
{
    using System;
    using System.Linq;

    using Labo.DotnetTestResultParser.Model;
    using Labo.DotnetTestResultParser.Writers;

    /// <summary>
    /// The test result output template class.
    /// </summary>
    /// <seealso cref="IOutputTemplate" />
    public sealed class TestRunResultOutputTemplate : IOutputTemplate
    {
        private readonly TestRun[] _testRuns;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestRunSummaryOutputTemplate"/> class.
        /// </summary>
        /// <param name="testRuns">The test run.</param>
        /// <exception cref="ArgumentNullException">testRun</exception>
        public TestRunResultOutputTemplate(params TestRun[] testRuns)
        {
            _testRuns = testRuns ?? throw new ArgumentNullException(nameof(testRuns));
        }

        /// <inheritdoc />
        public void Write(ITestResultsOutputWriter outputWriter)
        {
            ArgumentNullException.ThrowIfNull(outputWriter);

            outputWriter.Write(Array.TrueForAll(_testRuns, x => x.IsSuccess) ? TestRunResult.Passed : TestRunResult.Failed);
        }
    }
}