namespace Labo.DotnetTestResultParser.Tests.Templates
{
    using System;
    using System.Linq;

    using Labo.DotnetTestResultParser.Model;
    using Labo.DotnetTestResultParser.Templates;
    using Labo.DotnetTestResultParser.Writers;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class TestRunResultOutputTemplateFixture
    {
        [Test]
        public void Write_ShouldWritePassed_WhenTestRunListIsEmpty()
        {
            // Arrange
            TestRunResultOutputTemplate testRunResultOutputTemplate = new TestRunResultOutputTemplate(Array.Empty<TestRun>());
            ITestResultsOutputWriter testResultsOutputWriter = Substitute.For<ITestResultsOutputWriter>();
            
            // Act
            testRunResultOutputTemplate.Write(testResultsOutputWriter);

            // Assert
            testResultsOutputWriter.Received(1).Write(TestRunResult.Passed);
        }

        [Test]
        public void Write_ShoulWritePassed_WhenAllTestRunsAreSuccess()
        {
            // Arrange
            TestRunResultOutputTemplate testRunResultOutputTemplate = new TestRunResultOutputTemplate(Enumerable.Repeat(new TestRun { IsSuccess = true }, 3).ToArray());
            ITestResultsOutputWriter testResultsOutputWriter = Substitute.For<ITestResultsOutputWriter>();

            // Act
            testRunResultOutputTemplate.Write(testResultsOutputWriter);

            // Assert
            testResultsOutputWriter.Received(1).Write(TestRunResult.Passed);
        }

        [Test]
        public void Write_ShoulWritePassed_WhenAllTestRunsUnSuccessful()
        {
            // Arrange
            TestRunResultOutputTemplate testRunResultOutputTemplate = new TestRunResultOutputTemplate(Enumerable.Repeat(new TestRun { IsSuccess = false }, 3).ToArray());
            ITestResultsOutputWriter testResultsOutputWriter = Substitute.For<ITestResultsOutputWriter>();

            // Act
            testRunResultOutputTemplate.Write(testResultsOutputWriter);

            // Assert
            testResultsOutputWriter.Received(1).Write(TestRunResult.Failed);
        }

        [Test]
        public void Write_ShoulWritePassed_WhenOneTestRunsUnSuccessful()
        {
            // Arrange
            TestRunResultOutputTemplate testRunResultOutputTemplate = new TestRunResultOutputTemplate(new TestRun { IsSuccess = true }, new TestRun { IsSuccess = false }, new TestRun { IsSuccess = true });
            ITestResultsOutputWriter testResultsOutputWriter = Substitute.For<ITestResultsOutputWriter>();

            // Act
            testRunResultOutputTemplate.Write(testResultsOutputWriter);

            // Assert
            testResultsOutputWriter.Received(1).Write(TestRunResult.Failed);
        }
    }
}
