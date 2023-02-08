BeforeAll {

    $PSModuleRoot = (Get-Item $PSScriptRoot).parent.fullname

    [void]([System.Reflection.Assembly]::LoadFrom("$($PSModuleRoot)\lib\ConvertIni\ConvertIni.dll"))
    
    . $PSModuleRoot\Public\ConvertTo-Ini.ps1

}

Describe 'ConvertTo-Ini' {

    It 'Expected convert to output' {

        $testobj = [PSCustomObject]@{
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

        $expectedStringLiteral = "Test1=hello
Test2=world
Test3=123456
Test4=123.456
Test5=
Test6=

[Profile]
Name=Joe
Occupation=Applications Developer

[EmptySection]

[Address]
Street=123 Main Street
City=Philadelphia
State=PA
ZipCode=123456

        "

        
        $ini = $testobj | ConvertTo-Ini

        $ini | Should -Be $expectedStringLiteral
    }
}