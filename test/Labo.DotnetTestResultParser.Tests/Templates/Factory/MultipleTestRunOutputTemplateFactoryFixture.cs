namespace Labo.DotnetTestResultParser.Tests.Templates.Factory
{
    using System.Collections.Generic;

    using Labo.DotnetTestResultParser.Model;
    using Labo.DotnetTestResultParser.Templates.Factory;

    using NUnit.Framework;

    [TestFixture]
    public class MultipleTestRunOutputTemplateFactoryFixture
    {
        [Test]
        public void IsTestRunSucess_WhenThereIsOneFalseTestRun_ShoudIsSucessFalse()
        {
            // Arrange
            var testRuns = CreateTestRunsOneSuccessOneUnsuccess();
            var factory = CreateTemplateFactory(testRuns);

            // Act
            var factoryIsSuccess = factory.IsSuccess;

            // Arrange
            Assert.IsFalse(factoryIsSuccess);
        }
        
        [Test]
        public void IsTestRunSucess_WhenAllTestRunAreSuccess_ShoudIsSuccessTrue()
        {
            // Arrange
            var testRuns = CreateTestRunsAllSuccess();
            var factory = CreateTemplateFactory(testRuns);

            // Act
            var isSuccess = factory.IsSuccess;

            // Assert
            Assert.IsTrue(isSuccess);
        }

        private static List<TestRun> CreateTestRunsAllSuccess()
        {
            var testRuns = new List<TestRun> { new TestRun { IsSuccess = true }, new TestRun { IsSuccess = true } };
            return testRuns;
        }

        private static List<TestRun> CreateTestRunsOneSuccessOneUnsuccess()
        {
            var testRuns = new List<TestRun> { new TestRun { IsSuccess = true }, new TestRun { IsSuccess = false } };
            return testRuns;
        }

        private static MultipleTestRunOutputTemplateFactory CreateTemplateFactory(IList<TestRun> testRuns)
        {
            return new MultipleTestRunOutputTemplateFactory(testRuns);
        }
    }
}
