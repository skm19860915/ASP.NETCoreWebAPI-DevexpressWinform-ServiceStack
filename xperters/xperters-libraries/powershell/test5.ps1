$clientId = "034c6f1f-fb75-4c45-98bc-541dec4fd825";
$clientSecret = "@ih.nqm8=wy7HD[5WHFp5-LfV/D]1XDf"
$tenantId = "xpertersdev.onmicrosoft.com";
$resourceId = "https://graph.windows.net"
$redirectUri = new-object System.Uri("urn:ietf:wg:oauth:2.0:oob")
$login = "https://login.microsoftonline.com"

$authContext = New-Object Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext("{0}/{1}" -f $login,$tenantId);
$clientCredential = New-Object Microsoft.IdentityModel.Clients.ActiveDirectory.ClientCredential($clientId, $clientSecret);
$authenticationResult = $authContext.AcquireTokenAsync($resourceId, $clientCredential).Result;
$token = $authenticationResult.AccessToken

Connect-MsolService -AccessToken $token -Verbose 