# Every Budget API


## Launch & Debug

- API routes: https://localhost:{port}/{controller | route}
- Swagger: https://localhost:{port}/swagger/index.html

This `launch.json` allows VSCode to have easy debugging straight from the editor.

```json
{
    // Use IntelliSense to learn about possible attributes.
    // Hover to view descriptions of existing attributes.
    // For more information, visit: https://go.microsoft.com/fwlink/?linkid=830387
    "version": "0.2.0",
    "configurations": [
        {
            "name": ".NET Core Launch (web)",
            "type": "coreclr",
            "request": "launch",
            "preLaunchTask": "build",
            "program": "${workspaceFolder}/EveryBudgetApi/bin/Debug/net8.0/EveryBudgetApi.dll",
            "args": [],
            "cwd": "${workspaceFolder}/EveryBudgetApi",
            "stopAtEntry": false,
            "serverReadyAction": {
                "action": "openExternally",
                "pattern": "\\bNow listening on:\\s+(https?://\\S+)"
            },
            "env": {
                "ASPNETCORE_ENVIRONMENT": "Development"
            },
            "sourceFileMap": {
                "/Views": "${workspaceFolder}/Views"
            }
        },
        {
            "name": ".NET Core Attach",
            "type": "coreclr",
            "request": "attach"
        }
    ]
}
```

## Development Environment Notes

This project's development was started on MacOS + VSCode. Visual Studio for Mac is being deprecated (related in a moment) thus, VSCode with the C# Dev Kit package are bring provided as a "replacement" for doing native development on Mac. You'll also want to install the `dotnet-aspnet-codegenerator` package.

C# Dev Kit package: [https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit)

aspnet-codegenerator: [https://learn.microsoft.com/en-us/aspnet/core/fundamentals/tools/dotnet-aspnet-codegenerator?view=aspnetcore-3.1](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/tools/dotnet-aspnet-codegenerator?view=aspnetcore-3.1)

### Scaffolding/Code Generation

Checkout the arguments for how to start scaffolding code from the CLI

https://learn.microsoft.com/en-us/aspnet/core/fundamentals/tools/dotnet-aspnet-codegenerator?view=aspnetcore-3.1#arguments

**Example**

The following examples generates an API controller (no views), given a name, and with the provided namespace.

```bash
> dotnet aspnet-codegenerator controller -api -name TransactionsController -namespace EveryBudgetApi.Controllers
```