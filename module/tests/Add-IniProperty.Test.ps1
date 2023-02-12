<#/*
 * @Author: Joseph Iannone 
 * @Date: 2023-02-11 11:31:43 
 * @Last Modified by:   Joseph Iannone 
 * @Last Modified time: 2023-02-11 11:31:43 
 */#>


BeforeAll {

    # Get module root
    $script:PSModuleRoot = (Get-Item $PSScriptRoot).parent.fullname
    
    # Import module
    Import-Module "$($script:PSModuleRoot)\IniConverter.psd1"
    

    $script:TestFile = "$($script:PSModuleRoot)\tests\add-inipropertytest.ini"
}


Describe 'Add-IniProperty' {

    It 'Adds or Updates properties from an input object in a specified ini file' {

        # setup test file
        Get-Content "$($script:PSModuleRoot)\tests\test_input_002.ini" > $script:TestFile

        [PSCustomObject]$obj =  Get-Content $script:TestFile | ConvertFrom-Ini

        $testKey1 = "testsectionkey$((Get-Date).ToString("yyyyMMddHHmmss"))"

        # Verify existing keys, and null keyxs
        $obj.Test1.GetType().Name | Should -BeExactly "String"
        $obj.$testKey1 | Should -BeExactly $null
        $obj.$testKey1.test | Should -BeExactly $null
        $obj.$testKey1.test2 | Should -BeExactly $null

        $script:TestFile | Add-IniProperty -InputObject @{$testKey1 = @{test = 123; test2 = "hello"}}

        [PSCustomObject]$obj = Get-Content $script:TestFile | ConvertFrom-Ini

        # Verify properties were added
        $obj.$testKey1.GetType().Name | Should -BeExactly "PSCustomObject"
        $obj.$testKey1.test | Should -BeExactly "123"
        $obj.$testKey1.test2 | Should -BeExactly "hello"

        $script:TestFile | Add-IniProperty -InputObject @{$testKey1 = @{test = 456 }}

        [PSCustomObject]$obj = Get-Content $script:TestFile | ConvertFrom-Ini

        # Verify child property was updated
        $obj.$testKey1.GetType().Name | Should -BeExactly "PSCustomObject"
        $obj.$testKey1.test | Should -BeExactly "456"
        $obj.$testKey1.test2 | Should -BeExactly "hello"


        $script:TestFile | Add-IniProperty -InputObject @{$testKey1 = "testing string override"}

        [PSCustomObject]$obj = Get-Content $script:TestFile | ConvertFrom-Ini

        # Verify property was overwritten
        $obj.$testKey1.GetType().Name | Should -BeExactly "String"
        $obj.$testKey1.test | Should -BeExactly $null
        $obj.$testKey1.test2 | Should -BeExactly $null

        # remove test file
        Remove-Item $script:TestFile

    }

}