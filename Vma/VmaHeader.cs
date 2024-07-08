using System;
using System.IO;
using System.Text;
using VMAComparer.Utility;

namespace VMAComparer.Vma;


public class VmaHeader
{
    public string Magic { get; }
    public uint Version { get; }
    public Guid Uuid { get; }
    public uint BlobBufferOffset { get; }
    public uint BlobBufferSize { get; }
    public uint HeaderSize { get; }
    public DateTime BackupDate { get; }

    public string Md5Checksum { get; }

    public VmaHeader(Stream sourceStream)
    {
        Magic = Encoding.UTF8.GetString(sourceStream.ReadBytes(4));
        Version = sourceStream.ReadBytesFromAndConvertUInt32(4);
        Uuid = new Guid(sourceStream.ReadBytes(16));

        var cTime = sourceStream.ReadBytesFromAndConvertToLong(8);
        BackupDate = DateTime.UnixEpoch.AddSeconds(cTime);
        Md5Checksum = BitConverter.ToString(sourceStream.ReadBytes(16)).Replace("-", "").ToLowerInvariant();

        BlobBufferOffset = sourceStream.ReadBytesFromAndConvertUInt32(4);
        BlobBufferSize = sourceStream.ReadBytesFromAndConvertUInt32(4);
        HeaderSize = sourceStream.ReadBytesFromAndConvertUInt32(4);
    }
}