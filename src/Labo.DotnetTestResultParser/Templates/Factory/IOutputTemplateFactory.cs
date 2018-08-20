namespace Labo.DotnetTestResultParser.Templates.Factory
{
    /// <summary>
    /// The output template factory interface.
    /// </summary>
    public interface IOutputTemplateFactory
    {
        /// <summary>
        /// Gets a value indicating whether the test result is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the test result is success; otherwise, <c>false</c>.
        /// </value>
        bool IsSuccess { get; }

        /// <summary>
        /// Creates the summary output template.
        /// </summary>
        /// <returns></returns>
        IOutputTemplate CreateSummaryOutputTemplate();

        /// <summary>
        /// Creates the test result output template.
        /// </summary>
        /// <returns></returns>
        IOutputTemplate CreateTestResultOutputTemplate();
    }
}