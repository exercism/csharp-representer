<#
.SYNOPSIS
    Create a representation for a solution using the Docker representer image.
.DESCRIPTION
    Solutions on the website are stored in a normalized, representation format
    using the Docker representer image.
    This script allows one to verify that this Docker image correctly
    creates a valid representation of a solution.
.PARAMETER Exercise
    The slug of the exercise for which a representation should be created.
.PARAMETER Directory
    The directory in which the solution can be found.
.EXAMPLE
    The example below will create a representation for the two-fer solution 
    in the "~/exercism/two-fer" directory
    PS C:\> ./represent-in-docker.ps1 two-fer ~/exercism/two-fer
#>

param (
    [Parameter(Position = 0, Mandatory = $true)]
    [string]$Exercise, 
    
    [Parameter(Position = 1, Mandatory = $true)]
    [string]$Directory
)

docker build -t exercism/csharp-representer .
docker run -v ${Directory}:/solution exercism/csharp-representer $Exercise /solution