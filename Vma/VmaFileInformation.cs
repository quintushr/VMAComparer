using System;
using System.Buffers.Binary;
using System.IO;
using VMAComparer.Utility;

namespace VMAComparer.Vma;

public class VmaFileInformation
{
    public long FileSize { get; set; }
    public DateTime BackupDate { get; set; }

    public VmaFileInformation(Stream stream)
    {
        FileSize = stream.Length;

        var cTime = BinaryPrimitives.ReadInt64BigEndian(stream.ReadBytesFrom(24, 8));
        BackupDate = DateTime.UnixEpoch.AddSeconds(cTime);
    }


    public override string ToString()
    {
        return $"File Size: {FileSize} \n" +
               $"Backup Date: {BackupDate.ToLocalTime()}\n";
    }
}