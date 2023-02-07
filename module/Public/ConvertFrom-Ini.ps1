<#
/*
 * @Author: Joseph Iannone 
 * @Date: 2023-02-06 12:35:13 
 * @Last Modified by: Joseph Iannone
 * @Last Modified time: 2023-02-07 12:57:34
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

        [Convert_Ini.IniParser]$parser = [Convert_Ini.iniParser]::new()

        [PSCustomObject]$result;

    }
    
    Process {
        
        $inputBuffer.Add([string]$InputObject)
        #$result.Add($parser.ThisIsATest($InputObject))
        #[PSCustomObject]$result = $parser.ThisIsATest($InputObject)

        #$result
    }
    
    End {
        #$result
        [string]$inputStr = $($inputBuffer -join [Environment]::NewLine)
        

        #$inputStr

        #[Convert_Ini.IniParser]$parser = [Convert_Ini.iniParser]::new()
        

        [PSCustomObject]$result = $parser.ThisIsATest($inputStr)
        
        #return $parser.ThisIsATest($inputStr)
        #$result
        
        #[PSCustomObject]$output = New-Object PSCustomObject -Property $result
        
        #$output
        $result
    }
}