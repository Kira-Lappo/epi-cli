dotnet publish ./OptimizelyApiClient.Cli  `
  -c Release `
  -o ./publish  `
  /p:AssemblyName=opapi  `
  /p:PublishSingleFile=true  `
  /p:SelfContained=true  `
  /p:PublishReadyToRun=true  `
  /p:PublishTrimmed=true  `
  /p:PublishReadyToRunEmitSymbols=true  `
  /p:PublishReadyToRunComposite=true  `
  /p:RuntimeIdentifier=linux-x64
