using System;
using CommandLine;
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
        // Constant
        const int columnLength = 36;
        const string title = "             || Source                             | Target                              ";

        // Source
        using var sourceStream = VmaFileProvider.Open(opts.Source);
        var sourceInformation = new VmaFileInformation(sourceStream);

        // Target
        using var targetStream = VmaFileProvider.Open(opts.Target);
        var targetInformation = new VmaFileInformation(targetStream);

        // Display comparison information
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

        //for (var i = 0; i < sourceBlocks.Length; i++)
        //{
        //    var sourceBlock = sourceBlocks[i];
        //    var targetBlock = targetBlocks[i];

        //    Console.WriteLine();
        //    Console.WriteLine($"Block {i}");
        //    Console.WriteLine(new string('=', title.Length));
        //    Console.WriteLine(title);
        //    Console.WriteLine($"ClusterNum ||{sourceBlock.ClusterNum,-columnLength}|{targetBlock.ClusterNum,-columnLength}");
        //    Console.WriteLine($"DevId      ||{sourceBlock.DevId,-columnLength}|{targetBlock.DevId,-columnLength}");
        //    Console.WriteLine($"Mask       ||{sourceBlock.Mask,-columnLength}|{targetBlock.Mask,-columnLength}");
        //    Console.WriteLine($"Reserved   ||{sourceBlock.Reserved,-columnLength}|{targetBlock.Reserved,-columnLength}");
        //    Console.WriteLine(new string('=', title.Length));
        //    if (Equals(sourceBlock, targetBlock))
        //        identicalBlocks++;
        //}

        //Console.WriteLine($"\n {identicalBlocks} identical blocks on {sourceBlocks.Length} blocks");
        return 1;
    }
}