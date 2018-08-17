namespace Labo.DotnetTestResultParser.Templates
{
    using System;

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
        /// Initializes a new instance of the <see cref="TestRunResultOutputTemplate"/> class.
        /// </summary>
        /// <param name="testRun">The test run.</param>
        public TestRunResultOutputTemplate(params TestRun[] testRun)
        {
            _testRuns = testRun ?? throw new ArgumentNullException(nameof(testRun));
        }

        /// <inheritdoc />
        public void Write(ITestResultsOutputWriter outputWriter)
        {
            if (outputWriter == null)
            {
                throw new ArgumentNullException(nameof(outputWriter));
            }

            foreach (var testRun in _testRuns)
            {
                outputWriter.WriteLine(testRun.Result);
            }
        }
    }
}