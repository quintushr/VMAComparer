using System;
using System.IO;

namespace VMAComparer.IO.File;

public class VmaFileReader
{

    public static Stream Open(string path)
    {
        // Check if the .vma file exists
        if (!System.IO.File.Exists(path)) 
            throw new FileNotFoundException("File doest not exist");
        // Open the file in read mode
        var fileStream = System.IO.File.OpenRead(path);
        // Read the file content
        var buffer = new byte[1024];
        if (fileStream.Read(buffer, 0, buffer.Length) > 0)
            return fileStream;
        throw new FileLoadException("Stream is empty");
    }
}