# Exercism C# representer

A tool that can create a normalized representation of C# solutions submitted to [Exercism](https://exercism.io).

## Create a representation of a solution

To create a representation of a solution, follow these steps:

1. Open a command prompt in the root directory.
1. Run `./represent.ps1 <exercise> <directory>`. This script will create a representation for the solution in the specified directory.
1. Once the script has completed, the representation will be written to `<directory>/representation.txt`.

## Create a representation for multiple solutions

To create representations for multiple solutions at once, follow these steps:

1. Open a command prompt in the root directory.
1. Run `./bulk-represent.ps1 <exercise> <directory>`. This script will create a representation for the solution in each directory sub-directory of `<directory>`.
1. Once the script has completed, it will:
   1. Output general statistics to the console.
   1. Write detailed analysis results to `<directory>/bulk_represent.json`.

## Create a representation of a solution using Docker

To create a representation of a solution using a Docker container, follow these steps:

1. Open a command prompt in the root directory.
1. Run `./represent-in-docker.ps1 <exercise> <directory>`. This script will:
   1. Build the representer Docker image (if necessary).
   1. Run the representer Docker image (as a container), passing the specified `exercise` and `directory` arguments.
1. Once the script has completed, the representation can be found at `<directory>/representation.txt`.

Note that the Docker image is built using the [.NET IL Linker](https://github.com/dotnet/core/blob/master/samples/linker-instructions.md#using-the-net-il-linker), which is why building can be quite slow.

## Source code formatting

This repository uses the [dotnet-format tool](https://github.com/dotnet/format/) to format the source code. There are no custom rules; we just use the default formatting. You can format the code by running the `./format.ps1` command.

### Scripts

The scripts in this repository are written in PowerShell. As PowerShell is cross-platform nowadays, you can also install it on [Linux](https://docs.microsoft.com/en-us/powershell/scripting/install/installing-powershell-core-on-linux?view=powershell-6) and [macOS](https://docs.microsoft.com/en-us/powershell/scripting/install/installing-powershell-core-on-macos?view=powershell-6).
