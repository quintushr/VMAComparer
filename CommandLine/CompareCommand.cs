using CommandLine;
using System;
using System.Linq;
using VMAComparer.Aspect;
using VMAComparer.File;
using VMAComparer.Vma;

namespace VMAComparer.CommandLine;

[Verb("compare", HelpText = "Compare VMA files block.")]
public class CompareCommand
{
    [Option('s', "source", HelpText = "Path to the source file for comparison.", Required = true)]
    public string Source { get; set; }

    [Option('t', "target", HelpText = "Path to the target file for comparison.", Required = true)]
    public string Target { get; set; }

    [Option('d', "details", Required = false, HelpText = "Display blocks comparison.")]
    public bool Details { get; set; }

    [Option('v', "verbose", Required = false, HelpText = "Enable verbose output.")]
    public bool Verbose { get; set; }

    [ExceptionHandlingAspect]
    public static int Run(CompareCommand opts)
    {
        // Source
        using var sourceStream = VmaFileProvider.Open(opts.Source);
        var sourceInformation = new VmaFileInformation(sourceStream);

        // Target
        using var targetStream = VmaFileProvider.Open(opts.Target);
        var targetInformation = new VmaFileInformation(targetStream);

        // Display header comparison information
        PrintHeaderComparison(opts, sourceInformation, targetInformation);

        // Display details comparison information
        PrintDetailsComparison(opts, sourceInformation, targetInformation);

        return 1;
    }

    private static void PrintDetailsComparison(CompareCommand opts, VmaFileInformation sourceInformation,
        VmaFileInformation targetInformation)
    {
        var maxHeaders = Math.Max(sourceInformation.VmaExtentHeaders.Count, targetInformation.VmaExtentHeaders.Count);
        const string headersComparisonTittle = "             || Identical blocks in header";

        WriteLineDetails(new string('-', headersComparisonTittle.Length), opts);
        WriteLineDetails(headersComparisonTittle, opts);
        WriteLineDetails(new string('-', headersComparisonTittle.Length), opts);
        var allIdenticalBlocks = 0;
        for (var i = 0; i < maxHeaders; i++)
        {
            var identicalBlocks = 0;
            var source = sourceInformation.VmaExtentHeaders.ElementAtOrDefault(i);
            var target = targetInformation.VmaExtentHeaders.ElementAtOrDefault(i);
            if (source is null || target is null)
                continue;

            for (var j = 0; j < 59; j++)
            {
                var sourceBlock = source.BlockInfos[j];
                var targetBlock = target.BlockInfos[j];
                if (sourceBlock.ClusterNum == targetBlock.ClusterNum)
                    identicalBlocks++;
            }

            WriteLineDetails($"{"Header" + i,-14}||{identicalBlocks,-31}", opts);
            allIdenticalBlocks += identicalBlocks;
        }

        WriteLineDetails(new string('-', headersComparisonTittle.Length), opts);
        WriteLineDetails(null, opts);

        Console.WriteLine($"There are {allIdenticalBlocks} identical blocks on {maxHeaders * 59} in {maxHeaders} VMA Extent Headers between these files");
    }

    private static void PrintHeaderComparison(CompareCommand opts, VmaFileInformation sourceInformation, VmaFileInformation targetInformation)
    {
        const int columnLength = 36;
        const string title = "             || Source                             | Target                              ";

        Console.WriteLine("VMA Files Comparison");
        Console.WriteLine($"Source File: {opts.Source}");
        Console.WriteLine($"Target File: {opts.Target}");
        Console.WriteLine();
        Console.WriteLine(new string('-', title.Length));
        Console.WriteLine(title);
        Console.WriteLine(new string('-', title.Length));
        Console.WriteLine($"Magic        ||{sourceInformation.VmaHeader.Magic,-(columnLength + 1)}|{targetInformation.VmaHeader.Magic,-columnLength}");
        Console.WriteLine($"Version      ||{sourceInformation.VmaHeader.Version,-columnLength}|{targetInformation.VmaHeader.Version,-columnLength}");
        Console.WriteLine($"Uuid         ||{sourceInformation.VmaHeader.Uuid,-columnLength}|{targetInformation.VmaHeader.Uuid,-columnLength}");
        Console.WriteLine($"BlobOffset   ||{sourceInformation.VmaHeader.BlobBufferOffset,-columnLength}|{targetInformation.VmaHeader.BlobBufferOffset,-columnLength}");
        Console.WriteLine($"BlobSize     ||{sourceInformation.VmaHeader.BlobBufferSize,-columnLength}|{targetInformation.VmaHeader.BlobBufferSize,-columnLength}");
        Console.WriteLine($"HeaderSize   ||{sourceInformation.VmaHeader.HeaderSize,-columnLength}|{targetInformation.VmaHeader.HeaderSize,-columnLength}");
        Console.WriteLine($"BackupDate   ||{sourceInformation.VmaHeader.BackupDate,-columnLength}|{targetInformation.VmaHeader.BackupDate,-columnLength}");
        Console.WriteLine($"ExtentHeaders||{sourceInformation.VmaExtentHeaders.Count,-columnLength}|{targetInformation.VmaExtentHeaders.Count,-columnLength}");
        Console.WriteLine(new string('-', title.Length));
        Console.WriteLine();
    }

    private static void WriteLineDetails(string? value, CompareCommand opts)
    {
        if (opts.Details)
            Console.WriteLine(value);
    }
}