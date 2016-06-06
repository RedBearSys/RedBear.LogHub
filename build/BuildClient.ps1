$ScriptPath = Split-Path $MyInvocation.MyCommand.Path
& "$ScriptPath\LocalConfig.ps1"

Set-Location $loghub

$folderName="$loghub\Build\Packages"
If (Test-Path $folderName){
	echo "Deleting Packages folder..."
	Remove-Item $folderName -recurse
}

echo "Building release..."
& "$vs\devenv.com" "$loghub\LogHub.sln" /Rebuild "Release|Any Cpu" | Out-Null

$fileversion = (Get-Command "$loghub\LogHub\bin\Release\LogHub.exe").FileVersionInfo.Fileversion

$res = $fileversion -match "(?<msi>[0-9]+\.[0-9]+\.[0-9]+)"
$msiversion = $matches["msi"]
$folder = (Get-ChildItem "C:\Program Files (x86)\Caphyon" | where { $_.psiscontainer } | Select-Object -First 1).Name
$cmd = "C:\Program Files (x86)\Caphyon\" + $folder + "\bin\x86\AdvancedInstaller.com"

echo "Updating the installer..."
&$cmd /edit "$loghub\Setup\LogHub.aip" /SetVersion $msiversion
&$cmd /edit "$loghub\Setup\LogHub.aip" /SetProperty ARMDisplayName="LogHub $fileversion"
&$cmd /rebuild "$loghub\Setup\LogHub.aip"

Write-Host "Press any key to continue ..."
$x = $host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")