# Convert-Ini
![Tests](https://github.com/joeiannone/Convert-Ini/actions/workflows/tests.yml/badge.svg)
### A PowerShell Module for converting ini files into objects and objects into ini files.

You can find the latest releases and downloads for this module in the PowerShell Gallery: [https://www.powershellgallery.com/packages/IniConverter](https://www.powershellgallery.com/packages/IniConverter)


### Installation
```
PS C:> Install-Module -Name IniConverter
```
---

#### Exported PowerShell Functions:
- ```ConvertFrom-Ini```
- ```ConvertTo-Ini```

#### Examples:
##### ConvertFrom-Ini
```
PS C:> $ini = "
>> Language=Powershell
>> Name=Joe
>> [Address]
>> ZIP=19147
>> Street=123 Fitzwater Street
>> State=Pennsylvania
>> "
PS C:> $obj = $ini | ConvertFrom-Ini
PS C:> $obj


Language   Name Address
--------   ---- -------
Powershell Joe  @{ZIP=19147; Street=123 Fitzwater Street; City=Philadelphia; State=Pennsylvania}


PS C:> $obj.Name
Joe
PS C:> $obj.Address.Street
123 Fitzwater Street
PS C:>
```
```
PS C:> $obj1 = Get-Content .\Config.ini | ConvertFrom-Ini
PS C:> $obj2 = Get-Content -Raw .\Config.ini | ConvertFrom-Ini
PS C:> $obj3 = ConvertFrom-Ini -InputObject (Get-Content .\Config.ini)
```

##### ConvertTo-Ini
```
PS C:> $obj = @{
>> Name = 'Joe'
>> Language = 'PowerShell'
>> Address = @{
>>      Street = '123 Fitzwater Street'
>>      City = 'Philadelphia'
>>      State = 'Pennsylvania'
>>      ZIP = 19147
>>   }
>> }
PS C:> $ini = $obj | ConvertTo-Ini
PS C:> $ini > Config.ini
PS C:> cat .\Config.ini
Name=Joe
Language=PowerShell

[Address]
ZIP=19147
Street=123 Fitzwater Street
State=Pennsylvania
City=Philadelphia

PS C:>
```