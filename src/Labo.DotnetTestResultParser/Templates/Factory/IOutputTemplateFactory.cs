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
        /// Creates the output template class for the specified output template type.
        /// </summary>
        /// <param name="outputTemplateType">Type of the output template.</param>
        /// <returns></returns>
        IOutputTemplate Create(OutputTemplateType outputTemplateType);
    }
}