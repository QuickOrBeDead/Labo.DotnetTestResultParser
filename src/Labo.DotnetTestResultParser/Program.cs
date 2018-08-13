namespace Labo.DotnetTestResultParser
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;
    using System.Threading.Tasks;

    using Labo.DotnetTestResultParser.Model;

    using McMaster.Extensions.CommandLineUtils;

    /// <summary>
    /// The program class.
    /// </summary>
    [Command(Name = "dotnet-labodotnettestresultsparser", FullName = "dotnet-labodotnettestresultsparser", Description = "Net Core Test Result Parser Global Tool.")]
    [VersionOptionFromMember(MemberName = nameof(GetVersion))]
    [HelpOption]
    public class Program
    {
        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        [Required]
        [Argument(0, "path", Description = "The test result xml path.")]
        public string Path { get; }

        /// <summary>
        /// Gets or sets the format.
        /// </summary>
        /// <value>
        /// The format.
        /// </value>
        [AllowedValues("NUnit", IgnoreCase = true)]
        [Option("-f|--format", Description = "Unit test result xml format. (Default: NUnit)")]
        public UnitTestResultXmlFormat Format { get; } = UnitTestResultXmlFormat.NUnit;

        /// <summary>
        /// Gets a value indicating whether [fail when result is failed].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [fail when result is failed]; otherwise, <c>false</c>.
        /// </value>
        [Option("--fail-when-result-is-failed", Description = "Fails the program when the unit test result is 'Failed'.")]
        public bool FailWhenResultIsFailed { get; } = false;

        private int OnExecute()
        {
            UnitTestResultParser unitTestResultParser = new UnitTestResultParser(Format);
            TestRun testRun = unitTestResultParser.ParseXml(Path);

            Console.WriteLine("Total tests: {0}. Passed: {1}. Failed: {2}. Skipped: {3}.", testRun.Total, testRun.Passed, testRun.Failed, testRun.Skipped);
            Console.WriteLine("Test Run {0}.", testRun.Result);

            if (FailWhenResultIsFailed && !testRun.IsSuccess)
            {
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// The main method.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        private static string GetVersion() =>
            typeof(Program).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
    }
}
