$Modules = @{
    "CommonDeploymentUtilities" = "$($PSScriptRoot)\CommonDeploymentUtilities.psm1";
}

Write-Host "Using CommonDeploymentUtilities"
# Import all modules
foreach($Module in $Modules.Keys)
{
	if (Get-Module -Name "$($Module)") {
		Write-Host "Module, $($Module), exists. Removing module"
		Remove-Module -Name "$($Module)" -Force
	}
	Write-Host "Importing module $($Module)"
	Import-Module -Name "$($Modules[$Module])"

}
