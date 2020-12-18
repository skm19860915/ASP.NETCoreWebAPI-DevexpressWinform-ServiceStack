# Get the first sql server that exists in a resource group name
# This assumes the first sql server will be that one that has been created
# by a release
# Import dependent modules
Param
(
    [Parameter (Mandatory= $true)] [string] $ResourceGroupName
)

. "$($PSScriptRoot)\Common.ps1"

$result = CheckResourseGroupNameIsValid $ResourceGroupName

try{
    Write-Host "Checking for sql server in resource group: $ResourceGroupName";
    $sqlServerName = (Get-AzureRmSqlServer -ResourceGroupName $ResourceGroupName)[0].ServerName
    $result = CheckSqlServerNameIsValid $sqlServerName 

    if($result -eq $true){
        $sqlServerFullName = "$sqlServerName.database.windows.net"
        Write-Host ("##vso[task.setvariable variable=var_sqlserverhostname]$sqlServerFullName");
        Write-Host ("##vso[task.setvariable variable=var_sqlservername]$sqlServerName")

        Write-Host ("Found sql server $sqlServerFullName");
    }
    else {
        Write-Error "Sql server error. Probably no sql server found in resource group";        
    }
}
catch{
    Write-Host $_.Exception.Message
    Write-Error "Sql server error. Probably no sql server found in resource group";        
}