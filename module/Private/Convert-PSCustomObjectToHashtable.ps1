<#
/*
 * @Author: Joseph Iannone 
 * @Date: 2023-02-07 00:07:33 
 * @Last Modified by: Joseph Iannone
 * @Last Modified time: 2023-02-07 00:32:19
 */
#>
Function Convert-PSCustomObjectToHashtable {
    
    Param(
        [Parameter(Mandatory=$true, ValueFromPipeline=$true)]
        [PSCustomObject]$InputObject
    )
    
    [Hashtable]$ht = @{}
    
    $InputObject.PSObject.properties | Foreach-Object {
        
        If ($_.Value.GetType().Name -eq "PSCustomObject") {
            
            $ht[$_.Name] = $_.Value | Convert-PSCustomObjectToHashtable
        
        }
        
        Else {
            
            $ht[$_.Name] = $_.Value
        
        }
    
    }
    
    $ht
    
}