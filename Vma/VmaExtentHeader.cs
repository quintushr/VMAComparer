using System.IO;
using System.Text;
using System;
using VMAComparer.Utility;

namespace VMAComparer.Vma;

public class VMAExtentHeader
{
    private const int Clusters = 59;
    public string Magic { get; set; }
    public ushort BlockCount { get; set; }
    public Guid Uuid { get; set; }
    public byte[] Md5Sum { get; set; }
    public byte[] Reserved { get; set; }
    public BlockInfo[] BlockInfos { get; set; }

    public static VMAExtentHeader FromStream(Stream stream)
    {

        var extentHeader = new VMAExtentHeader
        {
            Magic = Encoding.UTF8.GetString(stream.ReadBytes(4)),
            Reserved = stream.ReadBytes(2),
            BlockCount = stream.ReadBytesFromAndConvertUInt16(2),
            Uuid = new Guid(stream.ReadBytes(16)),
            Md5Sum = stream.ReadBytes(16),
            BlockInfos = new BlockInfo[Clusters]
        };

        for (var i = 0; i < Clusters; i++)
        {
            extentHeader.BlockInfos[i] = BlockInfo.FromReader(stream);
        }
        return extentHeader;
    }
}