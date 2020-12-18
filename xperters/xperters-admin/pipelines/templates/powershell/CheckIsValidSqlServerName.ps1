# Check that the resource group name is not null or empty
Param
(
    [Parameter (Mandatory= $true)] [string] $SqlServerName
)

. "$($PSScriptRoot)\Common.ps1"

CheckSqlServerNameIsValid $SqlServerName