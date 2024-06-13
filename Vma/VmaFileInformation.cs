using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace VMAComparer.Vma;

public class VmaFileInformation
{
    private const int BlockSize = 4096;
    private const string MagicIdentifier = "VMAE";

    private long FileSize { get; }

    public VmaHeader VmaHeader { get; }

    public IReadOnlyCollection<VMAExtentHeader> VmaExtentHeaders { get; }

    public VmaFileInformation(Stream sourceStream)
    {
        FileSize = sourceStream.Length;
        VmaHeader = new VmaHeader(sourceStream);
        VmaExtentHeaders = new ReadOnlyCollection<VMAExtentHeader>(FillHeaders(sourceStream));
    }

    private List<VMAExtentHeader> FillHeaders(Stream sourceStream)
    {
        sourceStream.Seek(VmaHeader.HeaderSize - sourceStream.Position, SeekOrigin.Current);
        var headers = new List<VMAExtentHeader>();
        while (sourceStream.Position < FileSize)
        {
            var extentHeader = VMAExtentHeader.FromStream(sourceStream);
            if (extentHeader.Magic != MagicIdentifier)
                continue;
            headers.Add(extentHeader);
            var extentHeaderBlockCount = extentHeader.BlockCount * BlockSize;
            sourceStream.Seek(extentHeaderBlockCount, SeekOrigin.Current);
        }
        return headers;
    }

    public override string ToString()
    {
        return $"File Size: {FileSize} \n" +
               $"Backup Date: {VmaHeader.BackupDate.ToLocalTime()}\n" +
               $"Blob Buffer Offset: {VmaHeader.BlobBufferOffset}\n" +
               $"Blob Buffer Size: {VmaHeader.BlobBufferSize}\n" +
               $"Header Size: {VmaHeader.HeaderSize}\n";
    }
}