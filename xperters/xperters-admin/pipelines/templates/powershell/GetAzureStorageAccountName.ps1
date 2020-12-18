# Get the first storage account that exists in a resource group name
# This assumes the first storage account will be that one that has been created
# by a release
# Import dependent modules
Param
(
    [Parameter (Mandatory=$true)] [string] $ResourceGroupName
)

. "$($PSScriptRoot)\Common.ps1"

$result = CheckResourseGroupNameIsValid $ResourceGroupName;

$errorMessage = "storage account error. No such resource found in resource group";
try{
    $storageAccount = (Get-AzureRmStorageAccount -ResourceGroupName $ResourceGroupName | Select-Object -First 1);
    $storageAccountName = $storageAccount.StorageAccountName;

    if(![string]::IsNullOrEmpty($storageAccountName)){
        $storageAccountKey = (Get-AzureRmStorageAccountKey $ResourceGroupName -Name $storageAccountName | Select-Object -First 1);
        $storageAccountPrimaryKey = $storageAccountKey.Value;
        $storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=$storageAccountName;AccountKey=$storageAccountPrimaryKey;EndpointSuffix=core.windows.net"
        $storageAccountUri = "$storageAccountName.blob.core.windows.net"
        Write-Host ("##vso[task.setvariable variable=var_StorageAccountUri]$storageAccountUri");
        Write-Host ("##vso[task.setvariable variable=var_StorageConnectionString]$storageConnectionString")

        Write-Host ("Found storage account $storageAccountName");
    }
    else {
        Write-Error $errorMessage;        
    }
}
catch{
    Write-Host $_.Exception.Message
    Write-Error $errorMessage;        
}