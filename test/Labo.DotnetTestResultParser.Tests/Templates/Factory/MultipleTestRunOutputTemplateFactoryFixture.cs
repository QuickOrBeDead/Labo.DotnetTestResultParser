using System;
using System.Collections.Generic;
using System.Text;
using Labo.DotnetTestResultParser.Model;
using Labo.DotnetTestResultParser.Templates.Factory;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Labo.DotnetTestResultParser.Tests.Templates.Factory
{
    [TestFixture]
    public class MultipleTestRunOutputTemplateFactoryFixture
    {
        [Test]
        public void IsTestRunSucess_WhenThereIsOneFalseTestRun_ShoudIsSucessFalse()
        {
            //arrange
            var testRuns = CreateTestRunsOneSuccessOneUnsuccess();
            var factory = CreateTemplateFactory(testRuns);

            //act
            var factoryIsSuccess = factory.IsSuccess;

            //arrange
            Assert.IsFalse(factoryIsSuccess);
        }
        
        [Test]
        public void IsTestRunSucess_WhenAllTestRunAreSuccess_ShoudIsSuccessTrue()
        {
            //arrange
            var testRuns = CreateTestRunsAllSuccess();
            var factory = CreateTemplateFactory(testRuns);

            //act
            var isSuccess = factory.IsSuccess;

            //assert
            Assert.IsTrue(isSuccess);
        }

        [Test]
        public void CreateTestRunForResult_WhenAllTestRunAreSuccess_ShouldTestRunIsSuccessTrue()
        {
            //arrange
            var testRuns = CreateTestRunsAllSuccess();
            var factory = CreateTemplateFactory(testRuns);

            //act
            var testRun = factory.CreateTestRunForResult();

            //assert
            Assert.IsTrue(testRun.IsSuccess);
        }

        [Test]
        public void CreateTestRunForResult_WhenOneSuccessAndOneUnscucess_ShouldTestRunIsSuccessFalse()
        {
            //arrange
            var testRuns = CreateTestRunsOneSuccessOneUnsuccess();
            var factory = CreateTemplateFactory(testRuns);

            //act
            var testRun = factory.CreateTestRunForResult();

            //assert
            Assert.IsFalse(testRun.IsSuccess);
        }

        private static List<TestRun> CreateTestRunsAllSuccess()
        {
            var testRuns = new List<TestRun>() { new TestRun() { IsSuccess = true }, new TestRun() { IsSuccess = true } };
            return testRuns;
        }

        private static List<TestRun> CreateTestRunsOneSuccessOneUnsuccess()
        {
            var testRuns = new List<TestRun>() { new TestRun() { IsSuccess = true }, new TestRun() { IsSuccess = false } };
            return testRuns;
        }

        private static MultipleTestRunOutputTemplateFactory CreateTemplateFactory(IList<TestRun> testRuns)
        {
            return new MultipleTestRunOutputTemplateFactory(testRuns);
        }
    }
}
