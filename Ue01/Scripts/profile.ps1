# place in $PROFILE.CurrentUserAllHosts

function prompt {
  $p = Split-Path -leaf -path (Get-Location)
  "$p> "
}

function Set-WhiteColorTheme {
  # Set-PSReadlineOption -TokenKind Command -ForegroundColor Green
	Set-PSReadLineOption -Colors @{Command = "Green"}
}

function VsDevShell() {
  $VsInstallPath = "C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise"
  Import-Module "$VsInstallPath\Common7\Tools\Microsoft.VisualStudio.DevShell.dll"
  Enter-VsDevShell -VsInstallPath $VsInstallPath -StartInPath ${pwd}
}

function DotNetCompletion() {
	Register-ArgumentCompleter -Native -CommandName dotnet -ScriptBlock {
			 param($commandName, $wordToComplete, $cursorPosition)
					 dotnet complete --position $cursorPosition "$wordToComplete" | ForEach-Object {
							[System.Management.Automation.CompletionResult]::new($_, $_, 'ParameterValue', $_)
					 }
	}
}

# change console colors
Set-WhiteColorTheme

# docker completion
# Install-Module DockerCompletion -Scope CurrentUser # execute this command once
Import-Module DockerCompletion

# git completion
# Install-Module posh-git -Scope CurrentUser
Import-Module posh-git

# PowerShell parameter completion shim for the dotnet CLI 
DotNetCompletion