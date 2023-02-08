<#
/*
 * @Author: Joseph Iannone 
 * @Date: 2023-02-07 00:15:52 
 * @Last Modified by: Joseph Iannone
 * @Last Modified time: 2023-02-07 00:29:07
 */
#>

# Global variable for module root directory
$PSModuleRoot = $PSScriptRoot

# Load ConvertIni assembly
[void]([System.Reflection.Assembly]::LoadFrom("$($PSModuleRoot)\lib\ConvertIni\ConvertIni.dll"))

#Get public and private function definition files.
$Public  = @( Get-ChildItem -Path $PSModuleRoot\Public\*.ps1 -ErrorAction SilentlyContinue )
$Private = @( Get-ChildItem -Path $PSModuleRoot\Private\*.ps1 -ErrorAction SilentlyContinue )

#Dot source the files
Foreach($import in @($Public + $Private))
{
    Try
    {
        . $import.fullname
    }
    Catch
    {
        Write-Error -Message "Failed to import function $($import.fullname): $_"
    }
}


Export-ModuleMember -Function $Public.Basename