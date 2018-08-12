namespace Labo.DotnetTestResultParser.Model
{
    /// <summary>
    /// The test run class.
    /// </summary>
    public sealed class TestRun
    {
        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets the passed.
        /// </summary>
        /// <value>
        /// The passed.
        /// </value>
        public int Passed { get; set; }

        /// <summary>
        /// Gets or sets the failed.
        /// </summary>
        /// <value>
        /// The failed.
        /// </value>
        public int Failed { get; set; }

        /// <summary>
        /// Gets or sets the skipped.
        /// </summary>
        /// <value>
        /// The skipped.
        /// </value>
        public int Skipped { get; set; }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public string Result { get; set; }
    }
}
