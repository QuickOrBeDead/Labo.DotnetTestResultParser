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

        private Task<int> OnExecuteAsync()
        {
            UnitTestResultParser unitTestResultParser = new UnitTestResultParser(Format);
            TestRun testRun = unitTestResultParser.ParseXml(Path);

            Console.WriteLine("Total tests: {0}. Passed: {1}. Failed: {2}. Skipped: {3}.", testRun.Total, testRun.Passed, testRun.Failed, testRun.Skipped);
            Console.WriteLine("Test Run {0}.", testRun.Result);

            return Task.FromResult(0);
        }

        /// <summary>
        /// The main method.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static Task<int> Main(string[] args) => CommandLineApplication.ExecuteAsync<Program>(args);

        private static string GetVersion() =>
            typeof(Program).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
    }
}
