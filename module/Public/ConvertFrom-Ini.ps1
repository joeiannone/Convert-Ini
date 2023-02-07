<#
/*
 * @Author: Joseph Iannone 
 * @Date: 2023-02-06 12:35:13 
 * @Last Modified by: Joseph Iannone
 * @Last Modified time: 2023-02-07 16:14:38
 */
#>

Function ConvertFrom-Ini {
    <#
    .SYNOPSIS
        Convert INI text to PSObject
    
    .DESCRIPTION
        Convert INI text to PSObject
        
    .PARAMETER InputObject
        A INI string to convert to PSObject
    
    .EXAMPLE
    
        $ini = "
        Language=PowerShell and C#
        Name=Joe
        [Address]
        ZIP=19147
        Street=123 South Street
        City=Philadelphia
        State=Pennsylvania
        "
        
        $ht = $ini | ConvertFrom-Ini
        
    #>
    [CmdletBinding()]
    [OutputType([PSCustomObject])]
    Param(
        [Parameter(Mandatory=$false, ValueFromPipeline=$true)][string]$InputObject
    )
    
    Begin {
        
        [System.Collections.ArrayList]$inputBuffer = [System.Collections.ArrayList]::new()

    }
    
    Process {
        
        [void]$inputBuffer.Add([string]$InputObject)
        
    }
    
    End {

        [string]$inputStr = $inputBuffer -join [Environment]::NewLine
        
        [PSCustomObject]$result = [Convert_Ini.IniParser]::Parse($inputStr) 

        $result
        
    }
}