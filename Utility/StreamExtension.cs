using System;
using System.Buffers.Binary;
using System.IO;

namespace VMAComparer.Utility;

public static class StreamExtension
{
    public static byte[] ReadBytes(this Stream stream, int length)
    {
        var buffer = new byte[length];
        _ = stream.Read(buffer, 0, buffer.Length);
        return buffer;
    }

    public static byte ReadOnlyByte(this Stream stream)
    {
        var oneByteArray = new byte[1];
        _ = stream.Read(oneByteArray, 0, 1);
        return oneByteArray[0];
    }

    public static long ReadBytesFromAndConvertToLong(this Stream stream, int length)
    {
        return BinaryPrimitives.ReadInt64BigEndian(stream.ReadBytes(length));
    }

    public static uint ReadBytesFromAndConvertUInt32(this Stream stream, int length)
    {
        return BinaryPrimitives.ReadUInt32BigEndian(stream.ReadBytes(length));
    }

    public static ushort ReadBytesFromAndConvertUInt16(this Stream stream, int length)
    {
        var readOnlySpan = stream.ReadBytes(length);
        return BinaryPrimitives.ReadUInt16BigEndian(readOnlySpan);
    }
}