$cert = New-SelfSignedCertificate -Subject "CN=CertName" -CertStoreLocation Cert:\CurrentUser\my -KeyExportPolicy Exportable -KeySpec Signature

$tenantid = "dd91a03e-fc6a-4ffb-91bf-c070006cdd51"
$x5t = [System.Convert]::ToBase64String(([System.Text.Encoding]::UTF8).GetBytes($cert.Thumbprint))
$header = @{
    alg = "RS256"
    typ = "JWT"
    x5t = $x5t
} | ConvertTo-Json -Compress

$claimsPayload = @{
    aud = "https://login.microsoftonline.com/$tenantid/oauth2/token"
    exp = [Math]::Floor([decimal](Get-Date((Get-Date).ToUniversalTime().AddSeconds(120)) -UFormat "%s"))
    iss = $ClientID
    jti = (New-Guid).Guid
    nbf = [Math]::Floor([decimal](Get-Date((Get-Date).ToUniversalTime()) -UFormat "%s"))
    sub = $ClientID
} | ConvertTo-Json -Compress

$headerjsonbase64 = [Convert]::ToBase64String([System.Text.Encoding]::UTF8.GetBytes($header)).Split('=')[0].Replace('+', '-').Replace('/', '_')
$claimsPayloadjsonbase64 = [Convert]::ToBase64String([System.Text.Encoding]::UTF8.GetBytes($claimsPayload)).Split('=')[0].Replace('+', '-').Replace('/', '_')

$preJwt = $headerjsonbase64 + "." + $claimsPayloadjsonbase64
$toSign = [System.Text.Encoding]::UTF8.GetBytes($preJwt)

$privateKey = $Cert.PrivateKey
$alg = [Security.Cryptography.HashAlgorithmName]::SHA256
$padding = [Security.Cryptography.RSASignaturePadding]::Pkcs1
$signature = [Convert]::ToBase64String($privateKey.SignData($toSign, $alg, $padding)) -replace '\+', '-' -replace '/', '_' -replace '='

$jwt = $headerjsonbase64 + "." + $claimsPayloadjsonbase64 + "." + $signature

# request token

$Body = @{
    "tenant"                = $tenantid
    "scope"                 = "$ARMResource/.default"
    "client_assertion_type" = "urn:ietf:params:oauth:client-assertion-type:jwt-bearer"
    "client_id"             = $ClientID
    "grant_type"            = "client_credentials"
    "client_assertion"      = $jwt
}

$params = @{
    ContentType = 'application/x-www-form-urlencoded'
    Headers     = @{'accept' = 'application/json' }
    Body        = $Body
    Method      = 'POST'
    URI         = $TokenEndpoint
}

(Invoke-RestMethod @params).Access_Token