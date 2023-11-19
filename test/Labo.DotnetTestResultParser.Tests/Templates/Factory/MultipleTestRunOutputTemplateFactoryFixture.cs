namespace Labo.DotnetTestResultParser.Tests.Templates.Factory
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using Labo.DotnetTestResultParser.Model;
    using Labo.DotnetTestResultParser.Templates;
    using Labo.DotnetTestResultParser.Templates.Factory;

    using NUnit.Framework;

    [TestFixture]
    public class MultipleTestRunOutputTemplateFactoryFixture
    {
        [Test]
        public void Constructur_ShouldThrowArgumentNull_WhenTestRunsIsNull()
        {
            // Arrange
            IEnumerable<TestRun> testRuns = null;

            // Act
            ArgumentNullException argumentNullException = Assert.Throws<ArgumentNullException>(() => new MultipleTestRunOutputTemplateFactory(testRuns));

            // Assert
            Assert.AreEqual("Value cannot be null. (Parameter 'testRuns')", argumentNullException.Message);
        }

        private static readonly char[] Separator = { ',' };

        [Test]
        [TestCase("", true)]
        [TestCase("true", true)]
        [TestCase("false", false)]
        [TestCase("true, true", true)]
        [TestCase("true, false", false)]
        [TestCase("false, false", false)]
        public void IsSuccess(string successString, bool expected)
        {
            // Arrange
            IList<TestRun> testRuns = successString.Split(Separator, StringSplitOptions.RemoveEmptyEntries).Select(x => new TestRun{ IsSuccess = Convert.ToBoolean(x, CultureInfo.InvariantCulture)}).ToList();
            MultipleTestRunOutputTemplateFactory factory = CreateTemplateFactory(testRuns);

            // Act
            bool isSuccess = factory.IsSuccess;

            // Assert
            Assert.AreEqual(expected, isSuccess);
        }

        [Test]
        public void IsSuccess_WhenThereIsOneFalseTestRun_ShoudIsSucessFalse()
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
        public void IsSuccess_WhenAllTestRunAreSuccess_ShoudIsSuccessTrue()
        {
            // Arrange
            var testRuns = CreateTestRunsAllSuccess();
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
            MultipleTestRunOutputTemplateFactory factory = CreateTemplateFactory(new List<TestRun>());

            // Act
            IOutputTemplate outputTemplate = factory.CreateSummaryOutputTemplate();

            // Assert
            Assert.IsInstanceOf<TestRunSummaryOutputTemplate>(outputTemplate);
        }

        [Test]
        public void CreateTestResultOutputTemplate_ShouldReturnTestRunResultOutputTemplate()
        {
            // Arrange
            MultipleTestRunOutputTemplateFactory factory = CreateTemplateFactory(new List<TestRun>());

            // Act
            IOutputTemplate outputTemplate = factory.CreateTestResultOutputTemplate();

            // Assert
            Assert.IsInstanceOf<TestRunResultOutputTemplate>(outputTemplate);
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
