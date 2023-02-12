<#/*
 * @Author: Joseph Iannone 
 * @Date: 2023-02-08 09:32:58 
 * @Last Modified by:   Joseph Iannone 
 * @Last Modified time: 2023-02-08 09:32:58 
 */#>

# Pester v5 required

BeforeAll {

    # Get module root
    $script:PSModuleRoot = (Get-Item $PSScriptRoot).parent.fullname
    
    # Import module
    Import-Module "$($script:PSModuleRoot)\IniConverter.psd1"

}


Describe 'ConvertTo-Ini' {

    It 'Converts PSObject to Ini text string' {

        # Object to convert
        $obj = [PSCustomObject]@{
            Test1 = "hello"
            Test2 = "world"
            Test3 = 123456
            Test4 = 123.456
            Profile = [PSCustomObject]@{
                Name = "Joe"
                Occupation = "Applications Developer"
            }
            EmptySection = @{}
            Address = [PSCustomObject]@{
                Street = "123 Main Street"
                City = "Philadelphia"
                State = "PA"
                ZipCode = 123456
            }
            Test5 = $null
            Test6 = ""
        }

        # Expected output string
        $expected = Get-Content "$($script:PSModuleRoot)\tests\test_input_001.ini" | Out-String

        # Convert
        $ini = $obj | ConvertTo-Ini

        # Assert
        $ini.Trim() | Should -BeExactly $expected.Trim()
    }
}