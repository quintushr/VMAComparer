using System;
using System.IO;
using VMAComparer.Utility;

namespace VMAComparer.Vma;

public class BlockInfo
{
    public ushort Mask { get; private init; }    
    public byte Reserved { get; private init; }
    public byte DevId { get; private init; }
    public uint ClusterNum { get; private init; }

    private bool Equals(BlockInfo other)
    {
        return Mask == other.Mask && Reserved == other.Reserved && DevId == other.DevId && ClusterNum == other.ClusterNum;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((BlockInfo)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Mask, Reserved, DevId, ClusterNum);
    }

    public static BlockInfo FromReader(Stream stream)
    {
        return new BlockInfo
        {
            Mask = stream.ReadBytesFromAndConvertUInt16(2),
            Reserved = stream.ReadOnlyByte(),
            DevId = stream.ReadOnlyByte(),
            ClusterNum = stream.ReadBytesFromAndConvertUInt32(4)
        };
    }
}