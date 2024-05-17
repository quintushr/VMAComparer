using System;
using CommandLine;
using VMAComparer.Aspect;
using VMAComparer.File;
using VMAComparer.Vma;

namespace VMAComparer.CommandLine;

[Verb("info", HelpText = "Display header information of VMA File.")]
public class InformationCommand
{
    [Option('p', "path", HelpText = "Path to the VMA file to retrieve information from.", Required = true)]
    public string Path { get; set; }

    [ExceptionHandlingAspect]
    public static int Run(InformationCommand opts)
    {
        using var stream = VmaFileProvider.Open(opts.Path);
        var fileInformation = new VmaFileInformation(stream).ToString();
        var format = $"VMA File Information: {System.IO.Path.GetFileName(opts.Path)}";
        Console.WriteLine(new string('=', format.Length));
        Console.WriteLine(format);
        Console.WriteLine(new string('=', format.Length));
        Console.WriteLine(fileInformation);
        return 1;
    }
}
