Param (
  [Parameter(Mandatory=$true)][string] $Value,
  [Parameter(Mandatory=$true)][string] $VariableName
)

$enc = [System.Text.Encoding]::UTF8
$bytes = $enc.GetBytes($value)
$base64Value = [System.Convert]::ToBase64String($bytes);
echo "##vso[task.setvariable variable=$VariableName]$base64Value";
