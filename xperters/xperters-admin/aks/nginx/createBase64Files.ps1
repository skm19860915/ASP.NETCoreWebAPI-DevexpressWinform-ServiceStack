$folder = Get-Location
$nginxFile = "$folder/nginx.conf"
$certFile = "$folder/../../../certs/xperters.crt"
$certKeyFile = "$folder/../../../certs/xperters.key"
$certPfxFile = "$folder/../../../certs/localhost.pfx"

# if (Test-Path -Path $certPfxFile -PathType Leaf){
#     $certPfxBase64 = [Convert]::ToBase64String([IO.File]::ReadAllBytes($certPfxFile))
#     $certPfxBase64 | out-file -FilePath "base64-localhost.pfx"
# }

if (Test-Path -Path $nginxFile -PathType Leaf){
    $nginxFileBase64 = [Convert]::ToBase64String([IO.File]::ReadAllBytes($nginxFile))
    $nginxFileBase64 | out-file -FilePath "base64-nginx.conf"
}

# if (Test-Path -Path $certFile  -PathType Leaf){
#     $certFileBase64 = [Convert]::ToBase64String([IO.File]::ReadAllBytes($certFile))
#     $certFileBase64 | out-file -FilePath "base64-xperters.crt"
# }

# if (Test-Path -Path $certKeyFile -PathType Leaf){
#     $certKeyFileBase64 = [Convert]::ToBase64String([IO.File]::ReadAllBytes($certKeyFile))
#     $certKeyFileBase64 | out-file -FilePath "base64-xperters.key"
# }

