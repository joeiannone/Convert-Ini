<#/*
 * @Author: Joseph Iannone 
 * @Date: 2023-02-06 12:35:13 
 * @Last Modified by: Joseph Iannone
 * @Last Modified time: 2023-02-07 16:14:38
 */#>


Function ConvertFrom-Ini {
    <#
    .SYNOPSIS
        Convert INI text to PSCustomObject
    
    .DESCRIPTION
        Convert INI text to PSCustomObject
        
    .PARAMETER InputObject
        A INI string to convert to PSCustomObject
    
    .EXAMPLE
        PS C:> $ini = "
        >> Language=Powershell
        >> Name=Joe
        >> [Address]
        >> ZIP=19147
        >> Street=123 Fitzwater Street
        >> State=Pennsylvania
        >> "
        PS C:> $obj = $ini | ConvertFrom-Ini
        PS C:> $obj


        Language   Name Address
        --------   ---- -------
        Powershell Joe  @{ZIP=19147; Street=123 Fitzwater Street; City=Philadelphia; State=Pennsylvania}


        PS C:> $obj.Name
        Joe
        PS C:> $obj.Address.Street
        123 Fitzwater Street

    .EXAMPLE
        PS C:> $obj1 = Get-Content .\Config.ini | ConvertFrom-Ini
        PS C:> $obj2 = Get-Content -Raw .\Config.ini | ConvertFrom-Ini
        PS C:> $obj3 = ConvertFrom-Ini -InputObject (Get-Content .\Config.ini)

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
        
        [PSCustomObject]$result = [ConvertIni.IniParser]::Parse($inputStr) 

        $result
        
    }
}