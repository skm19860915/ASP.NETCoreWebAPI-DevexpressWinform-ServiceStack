$applicationId = "034c6f1f-fb75-4c45-98bc-541dec4fd825";
$securePassword = "@ih.nqm8=wy7HD[5WHFp5-LfV/D]1XDf" | ConvertTo-SecureString -AsPlainText -Force
$credential = New-Object -TypeName System.Management.Automation.PSCredential -ArgumentList $applicationId, $securePassword
Connect-AzAccount -ServicePrincipal -Credential $credential -TenantId "dd91a03e-fc6a-4ffb-91bf-c070006cdd51"

$context = Get-AzContext
$tenantId = "dd91a03e-fc6a-4ffb-91bf-c070006cdd51"
$token = [Microsoft.Azure.Commands.Common.Authentication.AzureSession]::Instance.AuthenticationFactory.Authenticate($context.Account, $context.Environment, $tenantId, $null, "Never", $null, "74658136-14ec-4630-ad9b-26e160ff0fc6")

$headers = @{
    "Authorization"          = "Bearer $($token.AccessToken)"
    'Content-Type'           = 'application/json'
    'x-ms-client-request-id' = [System.Guid]::NewGuid().ToString()
    'x-ms-client-session-id' = [System.Guid]::NewGuid().ToString()    
}

$objid = "1b823ba2-059e-4b1a-a038-0af488949fea"
$response = Invoke-RestMethod "https://main.iam.ad.ext.azure.com/api/UserDetails/$objid" -Headers $headers -Method GET
Write-Host $response