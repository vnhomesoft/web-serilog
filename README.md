# Serilog
Getting started: https://github.com/serilog/serilog/wiki/Getting-Started#example-application

## Packages
```shell
dotnet add package Serilog
dotnet add package Serilog.Sinks.Console
dotnet add package Serilog.Sinks.File
```

## Sample request
```shell
# Generate and trust te dev certificate
dotnet cert --clean
dotnet cert --trust

# Rerun project
dotnet run
```

```shell
# -k ignore SSL verification
curl -v -k https://localhost:7004/weatherforecast
```