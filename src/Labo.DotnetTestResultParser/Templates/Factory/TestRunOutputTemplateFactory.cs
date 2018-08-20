namespace Labo.DotnetTestResultParser.Templates.Factory
{
    using System;

    using Labo.DotnetTestResultParser.Model;

    /// <summary>
    /// The test run output template factory class.
    /// </summary>
    /// <seealso cref="IOutputTemplateFactory" />
    public sealed class TestRunOutputTemplateFactory : IOutputTemplateFactory
    {
        private readonly TestRun _testRun;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestRunOutputTemplateFactory"/> class.
        /// </summary>
        /// <param name="testRun">The test run.</param>
        public TestRunOutputTemplateFactory(TestRun testRun)
        {
            _testRun = testRun ?? throw new ArgumentNullException(nameof(testRun));
        }

        /// <inheritdoc />
        public bool IsSuccess => _testRun.IsSuccess;

        /// <inheritdoc />
        public IOutputTemplate CreateSummaryOutputTemplate()
        {
            return new TestRunSummaryOutputTemplate(_testRun);
        }

        /// <inheritdoc />
        public IOutputTemplate CreateTestResultOutputTemplate()
        {
            return new TestRunResultOutputTemplate(_testRun);
        }
    }
}