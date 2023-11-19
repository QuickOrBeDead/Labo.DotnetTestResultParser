namespace Labo.DotnetTestResultParser.Tests.Templates
{
    using System;

    using Labo.DotnetTestResultParser.Model;
    using Labo.DotnetTestResultParser.Templates;
    using Labo.DotnetTestResultParser.Writers;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class TestRunSummaryOutputTemplateFixture
    {
        [Test]
        public void Constructor_ShouldThrowArgumentNull_WhenTestRunsIsNull()
        {
            // Arrange
            TestRun[] testRuns = null;

            // Act
            ArgumentNullException argumentNullException = Assert.Throws<ArgumentNullException>(() => new TestRunSummaryOutputTemplate(testRuns));

            // Assert
            Assert.AreEqual("Value cannot be null. (Parameter 'testRuns')", argumentNullException.Message);
        }

        [Test]
        public void Write()
        {
            // Arrange
            TestRun testRun = new TestRun { IsSuccess = true, Passed = 10, Failed = 0, Skipped = 2, Errors = 1, Total = 12, Result = "Passed", Name = "Test 1" };
            TestRunSummaryOutputTemplate testRunSummaryOutputTemplate = new TestRunSummaryOutputTemplate(testRun);
            ITestResultsOutputWriter outputWriter = Substitute.For<ITestResultsOutputWriter>();

            // Act
            testRunSummaryOutputTemplate.Write(outputWriter);

            // Assert
            AssertOutputWriterTestRunWrite(outputWriter, testRun);
        }

        [Test]
        public void WriteMultipleTestRuns()
        {
            // Arrange
            TestRun[] testRuns =
                {
                    new TestRun { IsSuccess = true, Passed = 10, Failed = 0, Skipped = 2, Errors = 1, Total = 12, Result = "Passed", Name = "Test 1" },
                    new TestRun { IsSuccess = false, Passed = 8, Failed = 1, Skipped = 1, Errors = 0, Total = 10, Result = "Failed", Name = "Test 2" }
                };
            TestRunSummaryOutputTemplate testRunSummaryOutputTemplate = new TestRunSummaryOutputTemplate(testRuns);
            ITestResultsOutputWriter outputWriter = Substitute.For<ITestResultsOutputWriter>();

            // Act
            testRunSummaryOutputTemplate.Write(outputWriter);

            // Assert
            for (int i = 0; i < testRuns.Length; i++)
            {
                TestRun testRun = testRuns[i];
                AssertOutputWriterTestRunWrite(outputWriter, testRun);
            }
        }

        [Test]
        public void Write_ShouldThrowArgumentNull_WhenOutputWriterIsNull()
        {
            // Arrange
            TestRunSummaryOutputTemplate testRunSummaryOutputTemplate = new TestRunSummaryOutputTemplate(new TestRun());

            // Act
            ArgumentNullException argumentNullException = Assert.Throws<ArgumentNullException>(() => testRunSummaryOutputTemplate.Write(null));

            // Assert
            Assert.AreEqual("Value cannot be null. (Parameter 'outputWriter')", argumentNullException.Message);
        }

        private static void AssertOutputWriterTestRunWrite(ITestResultsOutputWriter outputWriter, TestRun testRun)
        {
            outputWriter.Received(1).WriteLine("Test name : {0}", testRun.Name);
            outputWriter.Received(1).WriteLine("Total tests: {0}. Passed: {1}. Failed: {2}. Skipped: {3}. Errors: {4}.", testRun.Total, testRun.Passed, testRun.Failed, testRun.Skipped, testRun.Errors);
            outputWriter.Received(1).WriteLine("Test Run {0}.", testRun.Result);
        }
    }
}
