<#
/*
 * @Author: Joseph Iannone 
 * @Date: 2023-02-06 23:57:35 
 * @Last Modified by: Joseph Iannone
 * @Last Modified time: 2023-02-07 16:02:20
 */
#>
Function ConvertTo-Ini {
    <#
    .SYNOPSIS
        Convert PSObjects to INI text
        
    .DESCRIPTION
        Convert PSObjects to INI text
        
    .PARAMETER InputObject
        A PSObject to convert to INI
    
    .EXAMPLE
    
        $myObject = @{
            Name = 'Joe'     
            Language = 'PowerShell and C#'
            Address = @{
                Street = '123 South Street'
                City = 'Philadelphia'
                State = 'Pennsylvania'
                ZIP = 19147
            }
        }
        
        $Ini = $myObject | ConvertTo-Ini
        
        $Ini
        
        Language=PowerShell and C#
        Name=Joe
        [Address]
        ZIP=19147
        Street=123 South Street
        City=Philadelphia
        State=Pennsylvania
    #>
    [CmdletBinding()]
    [OutputType([string])]
    Param(
        [Parameter(Mandatory=$true, ValueFromPipeline=$true)][Object]$InputObject,
        [Parameter(Mandatory=$false, ValueFromPipeline=$false)][switch]$Compress
    )
    
    Begin {
    
    }
    
    Process {

        # normalize / validate input object as json
        $obj = $InputObject | ConvertTo-Json | ConvertFrom-Json
        
        $result = [Convert_Ini.IniWriter]::Write($obj, $Compress)
        
        $result
    
    }
    
    End {

    }

}