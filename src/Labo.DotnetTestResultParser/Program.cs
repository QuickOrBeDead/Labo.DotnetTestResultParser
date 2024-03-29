﻿namespace Labo.DotnetTestResultParser
{
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;

    using Labo.DotnetTestResultParser.IO;
    using Labo.DotnetTestResultParser.Model;
    using Labo.DotnetTestResultParser.Parsers;
    using Labo.DotnetTestResultParser.Templates;
    using Labo.DotnetTestResultParser.Templates.Factory;
    using Labo.DotnetTestResultParser.Writers;
    using Labo.DotnetTestResultParser.Writers.Factory;

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
        // ReSharper disable once UnassignedGetOnlyAutoProperty
        public string Path { get; }

        /// <summary>
        /// Gets or sets the format.
        /// </summary>
        /// <value>
        /// The format.
        /// </value>
        [Option("-f|--format", Description = "Unit test result xml format. Allowed values are: NUnit, XUnit. (Default: NUnit)")]
        public UnitTestResultXmlFormat Format { get; } = UnitTestResultXmlFormat.NUnit;

        /// <summary>
        /// Gets a value indicating whether [fail when result is failed].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [fail when result is failed]; otherwise, <c>false</c>.
        /// </value>
        [Option("--fail-when-result-is-failed", Description = "Fails the program when the unit test result is 'Failed'.")]
        public bool FailWhenResultIsFailed { get; } = false;

        /// <summary>
        /// Gets or sets the output.
        /// </summary>
        /// <value>
        /// The output.
        /// </value>
        [Option("-o|--output", Description = "Output file to write results. (Default output is Console)")]
        // ReSharper disable once UnassignedGetOnlyAutoProperty
        public string Output { get; }

        /// <summary>
        /// Gets the template.
        /// </summary>
        /// <value>
        /// The template.
        /// </value>
        [Option("-t|--template", Description = "The output template. Allowed values are: Summary, TestResult. (Default: Summary)")]
        public OutputTemplateType Template { get; } = OutputTemplateType.Summary;

        // ReSharper disable once UnusedMember.Local
        [SuppressMessage("Major Code Smell", "S1144:Unused private types or members should be removed", Justification = "<Pending>")]
        private int OnExecute()
        {
            var outputWriterFactory = new DefaultTestResultsOutputWriterFactory();
            using (var outputWriter = outputWriterFactory.Create(Output))
            {
                OutputTemplateManager outputTemplateManager = new OutputTemplateManager(Path, new TestRunResultParser(Format), new DefaultFileSystemManager());
                IOutputTemplateFactory outputTemplateFactory = outputTemplateManager.CreateOutputTemplateFactory();
                IOutputTemplate outputTemplate = OutputTemplateManager.CreateOutputTemplate(outputTemplateFactory, Template);
                outputTemplate.Write(outputWriter);

                if (FailWhenResultIsFailed && !outputTemplateFactory.IsSuccess)
                {
                    return -1;
                }
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
