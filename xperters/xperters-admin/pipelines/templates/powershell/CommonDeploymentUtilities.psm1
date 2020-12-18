# Check that the resource group name is not null or empty
function CheckResourseGroupNameIsValid([string] $resourceGroupName){

    $result = StringNotIsNullOrEmpty($resourceGroupName);

    if ($result -eq $false){
        write-error "Variable var_Resource_Group has not been set. Run the release again and provide this variable."
    }
    else {
        write-host "Azure resource group name found and is valid: $resourceGroupName"
    }
}

# Check that the key vault name is not null or empty
function CheckKeyVaultNameIsValid([string] $keyVaultName){
    $result = StringNotIsNullOrEmpty($keyVaultName);

    if ($result -eq $false){
        write-error "No key vault name is blank. Please check and try again."
    }
    else {
        write-host "Key vault name found: $keyVaultName"
    }

    return $result;
}

# Check that the sql server name is not null or empty
function CheckSqlServerNameIsValid([string] $sqlServerName){
    $result = StringNotIsNullOrEmpty($sqlServerName);

    if ($result -eq $false){
        write-error "Sql server name is blank. Please check and try again."
    }
    else {
        write-host "Sql server name found and is valid: $sqlServerName"
    }

    return $result;
}

# Check that the sql server name is not null or empty
function CheckWebAppNameIsValid([string]  $webAppSiteName){
    $result = StringNotIsNullOrEmpty( $webAppSiteName);

    if ($result -eq $false){
        write-error "No web app name is blank. Please check and try again."
    }
    else {
        write-host "Web app name is valid: $webAppSiteName"
    }

    return $result;
}

function StringNotIsNullOrEmpty([string] $value){

    return -not ([string]::IsNullOrEmpty($value) -or [string]::IsNullOrWhiteSpace($value));
}