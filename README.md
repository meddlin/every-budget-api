# Every Budget API

> NOTE
> Most recent local run on MacOS required rebuilding local environment and data. Tear
> down all containers, volumes, and check for local postgres running on MacOS. If those
> processes exist, kill them. Then stand up containers, let volumes be rebuilt, and 
> re-run UtilityTester for data seeding.

## Run with `dotnet`

$> `dotnet run --project ./EveryBudgetApi/EveryBudgetApi.csproj`

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

### Docker Setup (tested on MacOS)

1. The `docker-compose.yml` file will stand up a Postgres DB and pgAdmin instance.

- Start -> `docker compose up -d`
- Stop -> `docker compose down`

For any config changes to Postgres, it's best to remove the volume so Postgres can re-run
it's first time initialization. Left over volumes will prevent this from happening (see
docs below).

- Find volumes -> `docker volume ls`
- Remove volume -> `docker volume rm [vol-name]`

2. Setup connection string(s)

Pull username, password, and database name from the `docker-compose.yml` file, 
and create a connection string.

```bash
"Host=localhost;Database=every-budget;Username=user-name;Password=strong-password"
```

This will need to be set in both `EveryBudgetApi` and `UtilityTester` projects.

3. Initial Data Seed - Setup `UtilityTester` and run

```
dotnet run --project ./UtilityTester/UtilityTester.csproj
```

Running this project via debugger is recommended. It should run fine normally, but if you 
run into any issues, it's better to already be in the debugger.

This will ensure the database is reachable via code, and will generate fake data for testing. Somewhere
between the EntityFramework code and Bogus library, this also creates the DB tables, too.

4. Start WebAPI from VSCode debugger

After startup, the WebAPI Swagger doc will be available at either of the following. Check
`EveryBudgetApi/Properties/launchSettings.json` for this.

- `http://localhost:[port]/swagger`
- `https://localhost:[port]/swagger`

Ref: https://stackoverflow.com/questions/65811120/what-is-the-default-swagger-ui-url-in-swashbuckle

(Optional): Setup VSCode debugger (`.vscode/launch.json`)

- Open VSCode debug section (in sidebar)
- Dropdown > Add configuration

**Problem 1**

> This problem occurred on MacOS, not sure if applicable to Linux.

If you previously had Postgres installed, you may get an error about port 5432 already being bound. I solved that by stopping the locally installed Postgres service. 

```bash
brew services stop postgresql
```

Ref: https://stackoverflow.com/questions/34173451/stop-postgresql-service-on-mac-via-terminal

For some reason the postgres service was "sticky" about not letting go of that port 5432 binding. So, I still manually searched for any service holding 5432, and `pkill`'d it.

```bash
$ sudo lsof -i :5432
$ sudo pkill -u postgres
```

Ref: https://stackoverflow.com/questions/54085216/port-5432-is-already-in-use-postgres-mac

For good measure, I restarted after this and everything seemed to continue working as expected. I thought it might be necessary to force (local) Postgres to not startup on boot, but that hasn't been necessary.

- https://dba.stackexchange.com/questions/276416/disable-auto-start-of-postgres-server-on-boot-mac

**Problem 2**

Setting a `POSTGRES_DB` value will create a database upon starting the 
`docker compose up -d`, but an old data volume hanging around (from previous iterations) could prevent this from happening.

Ref: https://stackoverflow.com/questions/56657683/postgres-docker-image-is-not-creating-database-with-custom-name

Explanation -> Ref: https://github.com/docker-library/postgres/issues/453#issuecomment-393939412

Finally, specifying local directories for volumes is a little special:

Ref: https://stackoverflow.com/questions/48091744/error-volumes-dbdata-must-be-a-mapping-or-null

**Problem 3**

> This turned out to be a non-issue. You can access the containers, from the host, via `localhost`.

Get the IP of the container from the host.

```bash
docker inspect \
  -f '{{range.NetworkSettings.Networks}}{{.IPAddress}}{{end}}' 'container-id'
```

Ref: https://stackoverflow.com/questions/17157721/how-to-get-a-docker-containers-ip-address-from-the-host



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