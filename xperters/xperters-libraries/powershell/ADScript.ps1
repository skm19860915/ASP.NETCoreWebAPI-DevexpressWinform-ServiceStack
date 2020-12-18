$version = "2.0.2.76"

if ((Get-InstalledModule `
    -Name "AzureAD" `
    -RequiredVersion $version `
    -ErrorAction SilentlyContinue) -eq $null) {

    Write-Host "Needs installing"
    Install-Module -Name AzureAD -Scope CurrentUser -Force -RequiredVersion $version
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

$pwd = "Testing@123"
$currentDate = Get-Date
$endDate = $currentDate.AddYears(10) #10 years is nice and long
$thumb = (New-SelfSignedCertificate -DnsName "xperters.com" -CertStoreLocation "Cert:\CurrentUser\My" -KeyExportPolicy Exportable -Provider "Microsoft Enhanced RSA and AES Cryptographic Provider" -NotAfter $endDate).Thumbprint
$pwd = ConvertTo-SecureString -String $pwd -Force -AsPlainText

Export-PfxCertificate -cert "Cert:\CurrentUser\My\$thumb" -FilePath .\xperters.pfx -Password $pwd
$path = (Get-Item -Path ".\").FullName
$cert = New-Object System.Security.Cryptography.X509Certificates.X509Certificate("$path\xperters.pfx", $pwd)
$keyValue = [System.Convert]::ToBase64String($cert.GetRawCertData())

$tenantId = "dd91a03e-fc6a-4ffb-91bf-c070006cdd51"
$appId = "034c6f1f-fb75-4c45-98bc-541dec4fd825"

# Test to login using the app
Connect-AzureAD -TenantId $tenantId -ApplicationId $appId -CertificateThumbprint $thumb

$user = (Get-AzureADUser)[0]