namespace Labo.DotnetTestResultParser.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// The test result parser exception class.
    /// </summary>
    /// <seealso cref="Exception" />
    public class TestResultParserException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestResultParserException"/> class.
        /// </summary>
        public TestResultParserException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestResultParserException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public TestResultParserException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestResultParserException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public TestResultParserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestResultParserException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"></see> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"></see> that contains contextual information about the source or destination.</param>
        protected TestResultParserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
