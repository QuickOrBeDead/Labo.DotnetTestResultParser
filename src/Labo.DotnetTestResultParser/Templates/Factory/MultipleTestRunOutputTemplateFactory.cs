using System;
using System.Collections.Generic;
using System.Linq;
using Labo.DotnetTestResultParser.Model;

namespace Labo.DotnetTestResultParser.Templates.Factory
{
    /// <summary>
    /// The output template multiple factory interface
    /// </summary>
    /// <seealso cref="Labo.DotnetTestResultParser.Templates.Factory.IOutputTemplateFactory" />
    public sealed class MultipleTestRunOutputTemplateFactory : IOutputTemplateFactory
    {
        /// <summary>
        /// Gets or sets the test runs.
        /// </summary>
        /// <value>
        /// The test runs.
        /// </value>
        private IEnumerable<TestRun> _testRuns;

        /// <inheritdoc />
        public bool IsSuccess => IsTestRunsSucess();

        /// <summary>
        /// Initializes a new instance of the <see cref="MultipleTestRunOutputTemplateFactory"/> class.
        /// </summary>
        /// <param name="testRuns">The test runs.</param>
        public MultipleTestRunOutputTemplateFactory(IEnumerable<TestRun> testRuns)
        {
            _testRuns = testRuns;
        }

        /// <inheritdoc />
        public IOutputTemplate Create(OutputTemplateType outputTemplateType)
        {
            switch (outputTemplateType)
            {
                case OutputTemplateType.Summary:
                    return new TestRunSummaryOutputTemplate(_testRuns.ToArray());
                case OutputTemplateType.TestResult:
                    return new TestRunResultOutputTemplate(CreateTestRunForResult());
                default:
                    throw new ArgumentOutOfRangeException(nameof(outputTemplateType), outputTemplateType, null);
            }
        }

        private bool IsTestRunsSucess()
        {
            return _testRuns.All(testRun => testRun.IsSuccess != false);
        }

        internal TestRun CreateTestRunForResult() => new TestRun() { Result = IsTestRunsSucess() ? "Passed" : "Failed" };
    }
}