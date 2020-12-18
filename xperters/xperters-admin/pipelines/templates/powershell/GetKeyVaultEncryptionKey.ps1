Param(
    [Parameter(Mandatory = $true)] [string]$EncryptionKeyName,
    [Parameter(Mandatory = $true)] [string]$ConnectionString
)

$module = "SqlServer"

$EncryptionKeyName | Out-File -FilePath "d:\debug-value.log"
$ConnectionString | Out-File -FilePath "d:\debug.log"

# remove the column encryption setting from the connection string
$ConnectionString = $ConnectionString.Replace("Column Encryption Setting=Enabled;", "");

If (-not(Get-InstalledModule $module -ErrorAction silentlycontinue)) {
    Install-Module -Name $module -Scope CurrentUser -Force
    Write-Host "Module installed"
}
Else {
    Write-Host "Module already installed"
}

Import-Module $module

$sb = New-Object System.Data.Common.DbConnectionStringBuilder
$sb.set_ConnectionString($ConnectionString)
$dbName = $sb.'initial catalog'

# Set up connection and database SMO objects
$connection = New-Object 'System.Data.SqlClient.SqlConnection' $ConnectionString
$serverConnection = New-Object 'Microsoft.SqlServer.Management.Common.ServerConnection' $connection
$server = New-Object 'Microsoft.SqlServer.Management.Smo.Server' $serverConnection
$database = $server.Databases[$dbName]
Write-Host "Queried sql server for database properties";

#this will only generate the columnencryptionkey if it is doesn't exist
$cek = Get-SqlColumnEncryptionKey -Name $EncryptionKeyName -InputObject $database
$key = $cek.ColumnEncryptionKeyValues[0];

if(![string]::IsNullOrEmpty($key.EncryptedValueAsSqlBinaryString)){
    Write-Host "Found encryption key value for $EncryptionKeyName ";
    Write-Host "##vso[task.setvariable variable=EncryptedKeyValue;]$($key.EncryptedValueAsSqlBinaryString)"
}

