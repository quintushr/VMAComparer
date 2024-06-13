# VMA Comparer

## Overview
VMA Comparer is a command-line application built on .NET 8 that facilitates the comparison of VMA (Virtual Machine Archive) backup files. Since the 2.3 release, Proxmox Virtual Environment (PVE) has introduced a new backup format called VMA, replacing the old common .tar format. VMA files, like the old .tar files, can be compressed in .lzo or .gz formats. This tool aids in comparing VMA backup files, providing insights into the block-level differences between two VMA files, as well as displaying header information of a single VMA file.

## Features
- **Information Command**: Display header information of a single VMA backup file.
- **Comparison Command**: Compare two VMA backup files block by block to identify differences.
- **Verbose Output**: Enable verbose output for more detailed information during execution.

## Installation
To use VMA Comparer, ensure you have [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) installed on your system.

1. Clone this repository:
   ```bash
   git clone https://github.com/quintushr/VMAComparer.git
   ```

2. Navigate to the project directory:
   ```bash
   cd VMA-Comparer
   ```

3. Build the project:
   ```bash
   dotnet build
   ```

## Usage

### Information Command
Display header information of a single VMA backup file.

```bash
dotnet run info --path <vma-file-path>
```

- `vma-file-path`: Path to the VMA backup file.

#### Example
```bash
dotnet run info --path /path/to/file.vma
```

### Compare Command
Compare two VMA backup files block by block.

```bash
dotnet run compare --source <source-path> --target <target-path> [--details] [--verbose]
```

- `source-path`: Path to the source VMA backup file.
- `target-path`: Path to the target VMA backup file.
- `--details`: (Optional) Display detailed block comparison.
- `--verbose`: (Optional) Enable verbose output.

#### Example
```bash
dotnet run compare --source /path/to/source.vma --target /path/to/target.vma --details --verbose
```

## Examples

### Information Command
To display header information of a VMA file:
```bash
dotnet run info --path /path/to/file.vma
```

### Compare Command
To compare two VMA files and display detailed block comparison with verbose output:
```bash
dotnet run compare --source /path/to/source.vma --target /path/to/target.vma --details --verbose
```

## Contributing
Contributions are welcome! Please feel free to open issues or pull requests for any improvements or features you'd like to see.

## License
This project is licensed under the [GNU General Public License](LICENSE).