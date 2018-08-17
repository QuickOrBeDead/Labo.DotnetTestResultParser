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
            var testRuns = new List<TestRun>() { new TestRun() { IsSuccess = true }, new TestRun() { IsSuccess = false } };
            var factory = CreateTemplateFactory(testRuns);

            //act
            Assert.IsFalse(factory.IsSuccess);
        }
        [Test]
        public void IsTestRunSucess_WhenAllTestRunAreSuccess_ShoudIsSucessTrue()
        {
            //arrange
            var testRuns = new List<TestRun>() { new TestRun() { IsSuccess = true }, new TestRun() { IsSuccess = true } };
            var factory = CreateTemplateFactory(testRuns);

            //act
            Assert.IsTrue(factory.IsSuccess);
        }

        private static MultipleTestRunOutputTemplateFactory CreateTemplateFactory(IList<TestRun> testRuns)
        {
            return new MultipleTestRunOutputTemplateFactory(testRuns);
        }
    }
}
