<#/*
 * @Author: Joseph Iannone 
 * @Date: 2023-02-10 18:49:37 
 * @Last Modified by:   Joseph Iannone 
 * @Last Modified time: 2023-02-10 18:49:37 
 */#>


 Function Remove-IniProperty {
    <#
    .SYNOPSIS
        Remove Sections or properties from ini files
    
    .DESCRIPTION
        Remove Sections or properties from ini files
        
    .PARAMETER $Path
        Path to ini file

    .PARAMETER $Section
        Section key in ini file to remove or remove from

    .PARAMETER $Property
        Property key in ini file to remove
    
    .EXAMPLE
        PS C:> Remove-IniProperty -Path .\test001.ini -Section "Model" -Property "test"
        

    .EXAMPLE
        PS C:> ".\test001.ini" | Remove-IniProperty -Section "Model" -Property "test"
        PS C:> Remove-IniProperty -Path .\test001.ini -Section "Model" -Property "test"
        PS C:> Remove-IniProperty -Path .\test001.ini -Property "test"

    #>
    [CmdletBinding()]
    Param(
        [Parameter(Mandatory=$true, ValueFromPipeline=$true)][string]$Path,
        [Parameter(Mandatory=$false, ValueFromPipeline=$false)][string]$Property,
        [Parameter(Mandatory=$false, ValueFromPipeline=$false)][string]$Section
    )
    
    Process {

        try {

            # get object
            [PSCustomObject]$obj = Get-Content $Path | ConvertFrom-Ini

            If ($Section) {

                If ($Property) {
                    
                    if ($null -eq $obj.$Section.psobject.properties.$Property) {

                        throw "No key '$($Section).$($Property)' found in $($Path)"

                    }

                    $obj.$Section.psobject.properties.Remove($Property)
                }

                Else {

                    if ($null -eq $obj.$Section) {

                        throw "No key '$($Section)' found in $($Path)"

                    }

                    $obj.psobject.properties.Remove($Section)

                }

            }

            ElseIf ($Property) {

                if ($null -eq $obj.$Property) {

                    throw "No key '$($Property)' found in $($Path)"

                }

                $obj.psobject.properties.Remove($Property)

            }

            # Write changes to specified ini file
            $obj | ConvertTo-Ini > $Path
            
        }

        catch {

            Write-Host "`n$($_)`n"

        }
        
    }
    
}