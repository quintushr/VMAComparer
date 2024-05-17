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

    public static long ReadBytesFromAndConvertToLong(this Stream stream, int length)
    {
        return BinaryPrimitives.ReadInt64BigEndian(stream.ReadBytes(length));
    }

    public static uint ReadBytesFromAndConvertUInt32(this Stream stream, int length)
    {
        return BinaryPrimitives.ReadUInt32BigEndian(stream.ReadBytes(length));
    }
}