using System;
using System.Buffers.Binary;
using System.IO;

namespace VMAComparer.Vma;

public class VmaFileInformation
{
    public long FileSize { get; set; }
    public DateTime BackupDate { get; set; }

    public VmaFileInformation(Stream stream)
    {
        FileSize = stream.Length;

        var cTime = BinaryPrimitives.ReadInt64BigEndian(GetBytes(stream, 24, 8));
        BackupDate = DateTime.UnixEpoch.AddSeconds(cTime);
    }

    private static byte[] GetBytes(Stream stream, int from, int length)
    {
        var buffer = new byte[length];
        stream.Seek(from, SeekOrigin.Current);
        _ = stream.Read(buffer, 0, buffer.Length);
        return buffer;
    }

    public override string ToString()
    {
        return $"File Size: {FileSize} \n" +
               $"Backup Date: {BackupDate.ToLocalTime()}\n";
    }
}