namespace Labo.DotnetTestResultParser.Tests.Exceptions
{
    using System;
    using System.Reflection;

    using Labo.DotnetTestResultParser.Exceptions;

    using NUnit.Framework;

    [TestFixture]
    public class TestResultParserExceptionFixture
    {
        [Test]
        public void AssertException()
        {
            // Arrange - Act - Assert
            AssertException(typeof(TestResultParserException));
        }

        private static void AssertException(Type exceptionType)
        {
            ArgumentNullException.ThrowIfNull(exceptionType);

            // Arrange - Act - Assert
            if (HasPublicConstructor(exceptionType))
            {
                Exception exception = (Exception)Activator.CreateInstance(exceptionType);
                Assert.NotNull(exception);
                Assert.IsNull(exception.InnerException);
                Assert.AreEqual($"Exception of type '{exceptionType.FullName}' was thrown.", exception.Message);
            }

            if (HasPublicConstructor(exceptionType, typeof(Exception)))
            {
                Exception innerException = new Exception();
                Exception exception = (Exception)Activator.CreateInstance(exceptionType, innerException);

                Assert.NotNull(exception);
                Assert.AreSame(innerException, exception.InnerException);
                Assert.AreEqual($"Exception of type '{exceptionType.FullName}' was thrown.", exception.Message);
            }

            if (HasPublicConstructor(exceptionType, typeof(string)))
            {
                string message = "Test Message";
                Exception exception = (Exception)Activator.CreateInstance(exceptionType, message);

                Assert.NotNull(exception);
                Assert.IsNull(exception.InnerException);
                Assert.AreEqual(message, exception.Message);
            }

            if (HasPublicConstructor(exceptionType, typeof(string), typeof(Exception)))
            {
                Exception innerException = new Exception();
                string message = "Test Message";
                Exception exception = (Exception)Activator.CreateInstance(exceptionType, message, innerException);

                Assert.NotNull(exception);
                Assert.AreSame(innerException, exception.InnerException);
                Assert.AreEqual(message, exception.Message);
            }
        }

        private static bool HasPublicConstructor(Type exceptionType, params Type[] types)
        {
            return exceptionType.GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, types, null) != null;
        }
    }
}

