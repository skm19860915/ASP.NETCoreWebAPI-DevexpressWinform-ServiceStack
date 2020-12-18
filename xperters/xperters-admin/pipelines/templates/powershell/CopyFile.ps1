[CmdletBinding()]
param (
    [Parameter(Mandatory=$true)][string]$SourceFile,
    [Parameter(Mandatory=$true)][string]$DestinationFile
)
Copy-Item -Path $SourceFile -Destination $DestinationFile -Force
Write-Host "Copied file: $SourceFile to $DestinationFile"