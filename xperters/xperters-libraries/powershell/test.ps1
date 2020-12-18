$ObjectIdOfApplicationToChange = "ffb4b21e-888f-4df4-a306-aa6ae677c227"
 
$TenantId = "dd91a03e-fc6a-4ffb-91bf-c070006cdd51"
$ApplicationId = "034c6f1f-fb75-4c45-98bc-541dec4fd825"
$ServicePrincipalKey = ConvertTo-SecureString -String "@ih.nqm8=wy7HD[5WHFp5-LfV/D]1XDf" -AsPlainText -Force
 
Write-Information "Login to AzureRM as SP: $ApplicationId"
$AzureADCred = New-Object System.Management.Automation.PSCredential($ApplicationId, $ServicePrincipalKey)
Add-AzureRmAccount -ServicePrincipal -Credential $AzureADCred -TenantId $TenantId
 
# Get application with AzureRM because this will fill the tokencache for AzureAD as well (hidden feature).
Write-Information "Get application with AzureRM: $ObjectIdOfApplicationToChange"
Get-AzureRmADApplication -ObjectId $ObjectIdOfApplicationToChange
 
$ctx = Get-AzureRmContext
$cache = $ctx.TokenCache
$cacheItems = $cache.ReadItems()
 
$token = ($cacheItems | Where-Object { $_.Resource -eq "https://graph.windows.net/" })
 
Write-Information "Login to AzureAD with same SP: $ApplicationId"
Connect-AzureAD -AadAccessToken $token.AccessToken -AccountId $ctx.Account.Id -TenantId $ctx.Tenant.Id
 
Write-Information "Now get same application with AzureAD: $ObjectIdOfApplicationToChange"
$user = Get-AzureADUser -ObjectId  "ea4da60d-4e0b-4b63-9a72-a8f001d0bfa1" | Select-Object TelephoneNumber, Phone

Get-AzureADUser -ObjectId  "ea4da60d-4e0b-4b63-9a72-a8f001d0bfa1" | select otherMails
Get-AzureADUser -ObjectId  "ea4da60d-4e0b-4b63-9a72-a8f001d0bfa1" | select Mobile
Get-AzureADUser -ObjectId  "ea4da60d-4e0b-4b63-9a72-a8f001d0bfa1" | select TelephoneNumber

Get-AzureADUser | select DisplayName,UserPrincipalName,otherMails,Mobile,TelephoneNumber | Format-Table
Write-Host "Done"
 