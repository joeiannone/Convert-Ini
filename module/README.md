# Convert-Ini
![Tests](https://github.com/joeiannone/Convert-Ini/actions/workflows/tests.yml/badge.svg)
### A PowerShell module for parsing, converting, and managing ini file properties

You can find the latest releases and downloads for this module in the PowerShell Gallery: [https://www.powershellgallery.com/packages/IniConverter](https://www.powershellgallery.com/packages/IniConverter)


### Installation
```powershell
PS > Install-Module -Name IniConverter
```
---

#### Exported PowerShell Functions:
- ```ConvertFrom-Ini```
- ```ConvertTo-Ini```
- ```Add-IniProperty```
- ```Remove-IniProperty```

#### Examples:
##### ConvertFrom-Ini
```powershell
PS > $ini = "
>> Language=Powershell
>> Name=Joe
>> [Address]
>> ZIP=19147
>> Street=123 Fitzwater Street
>> State=Pennsylvania
>> "
PS > $obj = $ini | ConvertFrom-Ini
PS > $obj


Language   Name Address
--------   ---- -------
Powershell Joe  @{ZIP=19147; Street=123 Fitzwater Street; City=Philadelphia; State=Pennsylvania}


PS > $obj.Name
Joe
PS > $obj.Address.Street
123 Fitzwater Street
PS >
```
```powershell
PS > $obj1 = Get-Content .\Config.ini | ConvertFrom-Ini
PS > $obj2 = Get-Content -Raw .\Config.ini | ConvertFrom-Ini
PS > $obj3 = ConvertFrom-Ini -InputObject (Get-Content .\Config.ini)
```

##### ConvertTo-Ini
```powershell
PS > $obj = @{
>> Name = 'Joe'
>> Language = 'PowerShell'
>> Address = @{
>>      Street = '123 Fitzwater Street'
>>      City = 'Philadelphia'
>>      State = 'Pennsylvania'
>>      ZIP = 19147
>>   }
>> }
PS > $ini = $obj | ConvertTo-Ini
PS > $ini > Config.ini
PS > type .\Config.ini
Name=Joe
Language=PowerShell

[Address]
ZIP=19147
Street=123 Fitzwater Street
State=Pennsylvania
City=Philadelphia

PS >
```

##### Add-IniProperty
```powershell
PS > type .\test.ini
Test1 = hello
Test2 = world

[TestSection]
test1 = hello
test2 = world

PS > $myobj = @{ TestSection = @{ test1 = "updated"; }; TestSection2 = @{ hello = "world"; } }
PS > .\test.ini | Add-IniProperty -InputObject $myobj
PS > type .\test.ini
Test1 = hello
Test2 = world

[TestSection]
test1 = updated
test2 = world

[TestSection2]
hello = world

PS >
```

##### Remove-IniProperty
```powershell
PS > type .\test.ini
Test1 = hello
Test2 = world

[TestSection]
test1 = updated
test2 = world

[TestSection2]
hello = world

PS > .\test.ini | Remove-IniProperty -Section "TestSection" -Property "test1"
PS > .\test.ini | Remove-IniProperty -Section "TestSection2"
PS > .\test.ini | Remove-IniProperty -Property "Test2"
PS > type .\test.ini
Test1 = hello

[TestSection]
test2 = world

PS >
```