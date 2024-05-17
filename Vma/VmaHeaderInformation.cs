using System;
using System.Buffers.Binary;
using System.IO;
using VMAComparer.Utility;

namespace VMAComparer.Vma;

public class VmaHeaderInformation
{
    public long FileSize { get; set; }
    public uint BlobBufferOffset { get; set; }
    public uint BlobBufferSize { get; set; }
    public uint HeaderSize { get; set; }
    public DateTime BackupDate { get; set; }

    public VmaHeaderInformation(Stream stream)
    {
        FileSize = stream.Length;
        stream.Seek(24, SeekOrigin.Begin);
        var cTime = stream.ReadBytesFromAndConvertToLong(8);
        
        BackupDate = DateTime.UnixEpoch.AddSeconds(cTime);

        stream.Seek(16, SeekOrigin.Current);
        
        BlobBufferOffset = stream.ReadBytesFromAndConvertUInt32(4);
        BlobBufferSize = stream.ReadBytesFromAndConvertUInt32(4);
        HeaderSize = stream.ReadBytesFromAndConvertUInt32(4);
    }


    public override string ToString()
    {
        return $"File Size: {FileSize} \n" +
               $"Backup Date: {BackupDate.ToLocalTime()}\n"+
               $"Blob Buffer Offset: {BlobBufferOffset}\n"+
               $"Blob Buffer Size: {BlobBufferSize}\n"+
               $"Header Size: {HeaderSize}\n";
    }
}