namespace Labo.DotnetTestResultParser.Tests.Exceptions
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;

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
            if (exceptionType == null)
            {
                throw new ArgumentNullException(nameof(exceptionType));
            }

            // Arrange - Act - Assert
            if (HasPublicConstructor(exceptionType))
            {
                Exception exception = (Exception)Activator.CreateInstance(exceptionType);
                Assert.NotNull(exception);
                Assert.IsNull(exception.InnerException);
                Assert.AreEqual($"Exception of type '{exceptionType.FullName}' was thrown.", exception.Message);

                AssertGetObjectDataDoesNotThrowException(exception);

                BinarySerializeDeserializeException(exception);
            }

            if (HasPublicConstructor(exceptionType, typeof(Exception)))
            {
                Exception innerException = new Exception();
                Exception exception = (Exception)Activator.CreateInstance(exceptionType, innerException);

                Assert.NotNull(exception);
                Assert.AreSame(innerException, exception.InnerException);
                Assert.AreEqual($"Exception of type '{exceptionType.FullName}' was thrown.", exception.Message);

                AssertGetObjectDataDoesNotThrowException(exception);

                BinarySerializeDeserializeException(exception);
            }

            if (HasPublicConstructor(exceptionType, typeof(string)))
            {
                string message = "Test Message";
                Exception exception = (Exception)Activator.CreateInstance(exceptionType, message);

                Assert.NotNull(exception);
                Assert.IsNull(exception.InnerException);
                Assert.AreEqual(message, exception.Message);

                AssertGetObjectDataDoesNotThrowException(exception);

                BinarySerializeDeserializeException(exception);
            }

            if (HasPublicConstructor(exceptionType, typeof(string), typeof(Exception)))
            {
                Exception innerException = new Exception();
                string message = "Test Message";
                Exception exception = (Exception)Activator.CreateInstance(exceptionType, message, innerException);

                Assert.NotNull(exception);
                Assert.AreSame(innerException, exception.InnerException);
                Assert.AreEqual(message, exception.Message);

                AssertGetObjectDataDoesNotThrowException(exception);

                BinarySerializeDeserializeException(exception);
            }
        }

        private static void BinarySerializeDeserializeException(Exception value)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, value);

                memoryStream.Position = 0;
                Exception exception = (Exception)binaryFormatter.Deserialize(memoryStream);

                AssertExceptionsAreEqual(value, exception);

                if (value.InnerException != null)
                {
                    Assert.IsNotNull(exception.InnerException);
                    AssertExceptionsAreEqual(value.InnerException, exception.InnerException);
                }
            }
        }

        private static void AssertExceptionsAreEqual(Exception value, Exception exception)
        {
            Assert.AreEqual(value.Message, exception.Message);
            Assert.AreEqual(value.HResult, exception.HResult);
            Assert.AreEqual(value.HelpLink, exception.HelpLink);
            Assert.AreEqual(value.Source, exception.Source);
            Assert.AreEqual(value.StackTrace, exception.StackTrace);
            Assert.AreEqual(value.TargetSite, exception.TargetSite);
            Assert.AreEqual(value.Data.Count, exception.Data.Count);
        }

        private static void AssertGetObjectDataDoesNotThrowException<TException>(TException exception)
            where TException : Exception
        {
            Assert.DoesNotThrow(() => exception.GetObjectData(new SerializationInfo(exception.GetType(), new FormatterConverter()), new StreamingContext()));
        }

        private static bool HasPublicConstructor(Type exceptionType, params Type[] types)
        {
            return exceptionType.GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, types, null) != null;
        }
    }
}

