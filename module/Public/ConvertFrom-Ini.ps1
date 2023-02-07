<#
/*
 * @Author: Joseph Iannone 
 * @Date: 2023-02-06 12:35:13 
 * @Last Modified by: Joseph Iannone
 * @Last Modified time: 2023-02-07 00:14:15
 */
#>

Function ConvertFrom-Ini {
	<#
	.SYNOPSIS
		Convert INI text to Hashtable

	.DESCRIPTION
		Convert INI text to Hashtable

	.PARAMETER InputObject
		A INI string to convert to Hashtable

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
	Param(
		[Parameter(Mandatory=$true, ValueFromPipeline=$true)]
		[string]$InputObect
	)
	
	
	Begin {

	}

	Process {	

		[Convert_Ini.IniParser]$parser = [Convert_Ini.iniParser]::new()
        
		$result = $parser.ThisIsATest($InputObect)

        [PSCustomObject]$output = New-Object PSCustomObject -Property $result
    
        $output

	}

	End {
        
	}
}