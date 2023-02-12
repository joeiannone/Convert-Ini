<#/*
 * @Author: Joseph Iannone 
 * @Date: 2023-02-10 22:52:35 
 * @Last Modified by:   Joseph Iannone 
 * @Last Modified time: 2023-02-10 22:52:35 
 */#>


Function Add-IniProperty {
    <#
    .SYNOPSIS
        Adds or Updates properties from an input object to a sepcified ini file
    
    .DESCRIPTION
        Adds or Updates properties from an input object to a sepcified ini file
        
    .PARAMETER InputObject
        An object containing properties to add or update
    
    .EXAMPLE
        PS > .\test001.ini | Add-IniProperty -InputObject $myObj
        PS > Add-IniProperty -Path .\test001.ini -InputObject $myObj

    #>
    [CmdletBinding()]
    Param(
        [Parameter(Mandatory=$true, ValueFromPipeline=$true)][string]$Path,
        [Parameter(Mandatory=$true, ValueFromPipeline=$false)][Object]$InputObject
    )
    
    Process {

        # Get ini contenet as object from specified path
        [PSCustomObject]$obj = Get-Content $Path | ConvertFrom-Ini

        # normalize input object
        [PSCustomObject]$InputObject = $InputObject | ConvertTo-Ini | ConvertFrom-Ini

        # iterate over each input property
        $InputObject.PSObject.Properties | ForEach-Object {

            # if input property already exists
            If ($obj.PSObject.Properties[$_.Name]) {
                
                $currentObjName = $_.Name
                
                $objItemType = $obj.$currentObjName.GetType()

                # if the input property and file property values are same type but not string
                If ($objItemType -eq $_.Value.GetType() -and $objItemType.Name -ne "String") {

                    # iterate over each property of the child object
                    $_.Value.PSObject.Properties | ForEach-Object  {
                        
                        # update existing property with matching input porperty value
                        $obj.$currentObjName.PSObject.Properties[$_.Name].Value = $_.Value

                    }

                }

                Else {
                    # when the type is different just overwrite
                    # In this case either an object is being replaced with a string or a string with an object
                    $obj.PSObject.Properties[$_.Name].Value = $_.Value

                }
                
            }

            Else {
                # add new property
                $obj | Add-Member -MemberType NoteProperty -Name $_.Name -Value $_.Value

            }

            # Write changes to specified ini file
            $obj | ConvertTo-Ini > $Path
               
        }
        
    }
    
}