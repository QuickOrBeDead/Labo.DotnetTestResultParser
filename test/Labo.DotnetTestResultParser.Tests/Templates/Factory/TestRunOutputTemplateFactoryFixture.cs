namespace Labo.DotnetTestResultParser.Tests.Templates.Factory
{
    using System;

    using Labo.DotnetTestResultParser.Model;
    using Labo.DotnetTestResultParser.Templates;
    using Labo.DotnetTestResultParser.Templates.Factory;

    using NUnit.Framework;

    [TestFixture]
    public class TestRunOutputTemplateFactoryFixture
    {
        [Test]
        public void Constructur_ShouldThrowArgumentNull_WhenTestRunIsNull()
        {
            // Arrange
            TestRun testRun = null;

            // Act
            ArgumentNullException argumentNullException = Assert.Throws<ArgumentNullException>(() => new TestRunOutputTemplateFactory(testRun));

            // Assert
            Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: testRun", argumentNullException.Message);
        }

        [Test]
        public void IsSucess_WhenTestRunIsUnSuccess_ShoudIsSucessFalse()
        {
            // Arrange
            var testRuns = CreateTestRunUnsuccess();
            var factory = CreateTemplateFactory(testRuns);

            // Act
            var factoryIsSuccess = factory.IsSuccess;

            // Arrange
            Assert.IsFalse(factoryIsSuccess);
        }
        
        [Test]
        public void IsSucess_WhenTestRunIsSuccess_ShoudIsSuccessTrue()
        {
            // Arrange
            var testRuns = CreateTestRunSuccess();
            var factory = CreateTemplateFactory(testRuns);

            // Act
            var isSuccess = factory.IsSuccess;

            // Assert
            Assert.IsTrue(isSuccess);
        }

        [Test]
        public void CreateSummaryOutputTemplate_ShouldReturnTestRunSummaryOutputTemplate()
        {
            // Arrange
            TestRunOutputTemplateFactory factory = CreateTemplateFactory(new TestRun());

            // Act
            IOutputTemplate outputTemplate = factory.CreateSummaryOutputTemplate();

            // Assert
            Assert.IsInstanceOf<TestRunSummaryOutputTemplate>(outputTemplate);
        }

        [Test]
        public void CreateTestResultOutputTemplate_ShouldReturnTestRunResultOutputTemplate()
        {
            // Arrange
            TestRunOutputTemplateFactory factory = CreateTemplateFactory(new TestRun());

            // Act
            IOutputTemplate outputTemplate = factory.CreateTestResultOutputTemplate();

            // Assert
            Assert.IsInstanceOf<TestRunResultOutputTemplate>(outputTemplate);
        }

        private static TestRun CreateTestRunSuccess()
        {
            return new TestRun { IsSuccess = true };
        }

        private static TestRun CreateTestRunUnsuccess()
        {
            return new TestRun { IsSuccess = false };
        }

        private static TestRunOutputTemplateFactory CreateTemplateFactory(TestRun testRun)
        {
            return new TestRunOutputTemplateFactory(testRun);
        }
    }
}
