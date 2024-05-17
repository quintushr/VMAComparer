using System.IO;

namespace VMAComparer.Utility;

public static class StreamExtension
{
    public static byte[] ReadBytesFrom(this Stream stream, int from, int length)
    {
        var buffer = new byte[length];
        stream.Seek(from, SeekOrigin.Current);
        _ = stream.Read(buffer, 0, buffer.Length);
        return buffer;
    }
}