using System;
using System.IO;

namespace VMAComparer.File;

public class VmaFileProvider
{
    public static Stream Open(string path)
    {
        // Check if the file has the .vma extension
        if (!path.EndsWith(".vma", StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException("The file must have a .vma extension");

        // Check if the .vma file exists
        if (!System.IO.File.Exists(path))
            throw new FileNotFoundException("File does not exist");

        // Open the file in read mode
        return System.IO.File.OpenRead(path);
    }
}