using CommandLine;

namespace VMAComparer.CommandLine;

[Verb("compare", HelpText = "Compare VMA files block.")]
public class CompareCommand
{
    [Option('s', "source", HelpText = "Path to the source file for comparison.", Required = true)]
    public string Source { get; set; }

    [Option('t', "target", HelpText = "Path to the target file for comparison.", Required = true)]
    public string Target { get; set; }

    [Option('v', "verbose", Required = false, HelpText = "Enable verbose output.")]
    public bool Verbose { get; set; }

    public static int Run(CompareCommand opts)
    {
        return 1;
    }
}