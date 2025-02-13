<#/*
 * @Author: Joseph Iannone 
 * @Date: 2023-02-11 11:33:16 
 * @Last Modified by:   Joseph Iannone 
 * @Last Modified time: 2023-02-11 11:33:16 
 */#>


BeforeAll {

    # Get module root directory
    $script:PSModuleRoot = (Get-Item $PSScriptRoot).Parent.FullName

    # Import module
    Import-Module (Join-Path $script:PSModuleRoot "IniConverter.psd1")

    # Set test file path
    $script:TestFile = Join-Path $script:PSModuleRoot "tests/remove-inipropertytest.ini"

    # Setup test file
    $sourceFile = Join-Path $script:PSModuleRoot "tests/test_input_002.ini"
    Get-Content $sourceFile | Set-Content $script:TestFile

}
 
 
Describe 'Remove-IniProperty' {

    It 'Removes a specified property or section from an ini file' {
        
        # verify existing properties
        $obj = Get-Content $script:TestFile | ConvertFrom-Ini
        
        $obj.Test1 | Should -BeExactly "hello world"
        $obj.Address.Street.GetType().Name | Should -BeExactly "String"
        $obj.Address.City.GetType().Name | Should -BeExactly "String"


        # verify only requested property removed
        Remove-IniProperty -Path $script:TestFile -Section "Address" -Property "Street"

        $obj = Get-Content $script:TestFile | ConvertFrom-Ini

        $obj.Test1 | Should -BeExactly "hello world"
        $obj.Address.Street | Should -BeExactly $null
        $obj.Address.City.GetType().Name | Should -BeExactly "String"
        $obj.Address.State.GetType().Name | Should -BeExactly "String"
        $obj.Address.ZipCode.GetType().Name | Should -BeExactly "String"

        # verify removing entire section
        $obj.TestSection.GetType().Name | Should -BeExactly "PSCustomObject"

        Remove-IniProperty -Path $script:TestFile -Section "TestSection"

        $obj = Get-Content $script:TestFile | ConvertFrom-Ini

        $obj.TestSection | Should -BeExactly $null

        
        # remove test file
        Remove-Item $script:TestFile

    }

}
