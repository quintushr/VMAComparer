using CommandLine;

namespace VMAComparer.CommandLine;

[Verb("info", HelpText = "Display header information of VMA File.")]
public class InformationCommand
{
    [Option('p', "path", HelpText = "Path to the VMA file to retrieve information from.", Required = true)]
    public string Path { get; set; }

    [Option('v', "verbose", Required = false, HelpText = "Enable verbose output.")]
    public bool Verbose { get; set; }


    public static int Run(InformationCommand opts)
    {
        return 1;
    }
}
