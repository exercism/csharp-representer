<#
.SYNOPSIS
    Bulk create representations for the solutions in a directory.
.DESCRIPTION
    Bulk create representation for the solutions in a directory.
    Each child directory of the specified directory will be assumed to
    contain a solution.
.PARAMETER Exercise
    The slug of the exercise for which a representation should be created.
.PARAMETER Directory
    The directory in which the solutions can be found.
.EXAMPLE
    The example below will create a representation for the two-fer solutions
    in the "~/exercism/two-fer" directory
    PS C:\> ./bulk-represent.ps1 two-fer ~/exercism/two-fer
#>

param (
    [Parameter(Position = 0, Mandatory = $true)]
    [string]$Exercise, 
    
    [Parameter(Position = 1, Mandatory = $true)]
    [string]$Directory
)

dotnet run --project ./src/Exercism.Representers.CSharp.Bulk/ $Exercise $Directory