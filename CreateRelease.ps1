
param ( $version = "1.0", $Config = "Release")


Invoke-BatchFile $env:VS120COMNTOOLS\vsvars32.bat;
devenv .\sql-least-privilege.sln /rebuild $Config;
rm ".\SqlPermissions\bin\$Config\*.vshost.exe*"

$file_name = "SqlPermissions.$version.zip";
@(ls .\SqlPermissions\bin\$Config) + @(ls .\Test\*.tdf) | ? { $_.Extension -in @('.exe','.dll','.config','.tdf') } | Write-Zip -OutputPath $file_name -FlattenPaths;

Read-Archive $file_name | ft;