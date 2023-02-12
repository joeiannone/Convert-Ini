<#/*
 * @Author: Joseph Iannone 
 * @Date: 2023-02-08 20:36:45 
 * @Last Modified by:   Joseph Iannone 
 * @Last Modified time: 2023-02-08 20:36:45 
 */#>

# Pester v5 required


BeforeAll {

    # Get module root
    $script:PSModuleRoot = (Get-Item $PSScriptRoot).parent.fullname
    
    # Import module
    Import-Module "$($script:PSModuleRoot)\IniConverter.psd1"

}


Describe 'ConvertFrom-Ini' {

    It 'Converts ini text string to PSobject' {
        
        # Get contents of ini test file to convert
        $iniContents = Get-Content "$($script:PSModuleRoot)\tests\test_input_001.ini"


        # Expected output object
        $expectedObj = [PSCustomObject]@{
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
            Test5 = ""
            Test6 = $null
        }

        
        # Test
        $obj = $iniContents | ConvertFrom-Ini

        # Assert
        $obj.Test | Should -BeExactly $expectedObj.Test
        $obj.Test2 | Should -BeExactly $expectedObj.Test2
        $obj.Test3 | Should -BeExactly $expectedObj.Test3
        $obj.Test4 | Should -BeExactly $expectedObj.Test4
        $obj.Test5 | Should -BeExactly $expectedObj.Test5
        $obj.Profile.Name | Should -BeExactly $expectedObj.Profile.Name
        $obj.Profile.Occupation | Should -BeExactly $expectedObj.Profile.Occupation
        $obj.Address.Street | Should -BeExactly $expectedObj.Address.Street
        $obj.Address.City | Should -BeExactly $expectedObj.Address.City
        $obj.Address.State | Should -BeExactly $expectedObj.Address.State
        $obj.Address.ZipCode | Should -BeExactly $expectedObj.Address.ZipCode

    }
}