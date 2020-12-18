$version = "1.1.183.57"

if ((Get-InstalledModule `
    -Name "MSOnline" `
    -RequiredVersion $version `
    -ErrorAction SilentlyContinue) -eq $null) {

    Write-Host "Needs installing"
    Install-Module -Name MSOnline -Scope CurrentUser -Force -RequiredVersion $version
}
else {
    Write-Host "Does not need installing"

}

if (!(Get-Module -Name "AzureAD")) {
    Write-Host "Needs importing"
    Import-Module -Name "AzureAD"
}
else{
    Write-Host "Does not need importing"
}

$ObjectIdOfApplicationToChange = "ffb4b21e-888f-4df4-a306-aa6ae677c227"
 
$TenantId = "dd91a03e-fc6a-4ffb-91bf-c070006cdd51"
$ApplicationId = "034c6f1f-fb75-4c45-98bc-541dec4fd825"
$ServicePrincipalKey = ConvertTo-SecureString -String "@ih.nqm8=wy7HD[5WHFp5-LfV/D]1XDf" -AsPlainText -Force
 
Write-Information "Login to AzureRM as SP: $ApplicationId"
$AzureADCred = New-Object System.Management.Automation.PSCredential($ApplicationId, $ServicePrincipalKey)
Add-AzureRmAccount -ServicePrincipal -Credential $AzureADCred -TenantId $TenantId
 
Connect-MsolService -Credential $AzureADCred
 
$result = Get-MsolUser -ObjectId  "ea4da60d-4e0b-4b63-9a72-a8f001d0bfa1"  -EnabledFilter EnabledOnly -All | Select UserPrincipalName, DisplayName, MobilePhone, AlternateEmailAddresses, AlternateMobilePhones -ExpandProperty StrongAuthenticationUserDetails 
Write-Host "Done"
 