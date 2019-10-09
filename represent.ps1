<#
.SYNOPSIS
    Create a representation for a solution.
.DESCRIPTION
    Create a representation for a solution.
.PARAMETER Slug
    The slug of the exercise for which a representation should be created.
.PARAMETER Directory
    The directory in which the solution can be found.
.EXAMPLE
    The example below will create a representation for the two-fer solution 
    in the "~/exercism/two-fer" directory
    PS C:\> ./analyze.ps1 two-fer ~/exercism/two-fer
#>

param (
    [Parameter(Position = 0, Mandatory = $true)]
    [string]$Exercise, 
    
    [Parameter(Position = 1, Mandatory = $true)]
    [string]$Directory
)

dotnet run --project ./src/Exercism.Representers.CSharp/ $Exercise $Directory