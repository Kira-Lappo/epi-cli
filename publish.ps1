#!/bin/pwsh
param(
	[Parameter()]
	[ValidateSet("linux-x64", "win-x64")]
	$RID = "linux-x64"
)

dotnet publish $PSScriptRoot/src/OptimizelyApiClient.Cli  `
	-c Release  `
	-p:PublishProfile="publish.pubxml"  `
	-p:RuntimeIdentifier="$RID"  `
	-o "$PSScriptRoot/out/$RID"
