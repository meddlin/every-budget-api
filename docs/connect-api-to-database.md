# Connect API to Database

.NET API to Postgres DB

### Install package `Npgsql`

Ref: [https://www.nuget.org/packages/Npgsql/](https://www.nuget.org/packages/Npgsql/)

```
dotnet add package Npgsql --version 8.0.1
```

### Store connection string in `appsettings.json`

*NOTE: A static IP is used, as `localhost` stubbornly didn't want to connect on Windows 11. Your mileage may vary on other systems.*

```json
"ConnectionStrings": {
  "LocalDatabase": "Host=192.168.1.12;Database=every-budget;Username=testuser;Password=password1"
},
```

### Configure in `Program.cs`

Make sure to notice setting the connection string via `_configuration.GetConnectionSTring()`,
and setting up the context, via `Services.AddDbContext<EveryBudgetDbContext>`.

```csharp
var configurationBuilder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile(path: "appsettings.json", optional: true, reloadOnChange: true);
    IConfiguration _configuration = configurationBuilder.Build();
    var rdsDbString = _configuration.GetConnectionString("LocalDatabase");

// Add services to the container.
builder.Services.AddDbContext<EveryBudgetDbContext>(opt =>
                   opt.UseNpgsql(rdsDbString));
```

### Create the DbContext class

In order for the `Program.cs` configuration above to work, we need to create our 
`DbContext` class.

Setup the `EveryBudgetDbContext` like so. Notice connecting the `DbSet<>` classes
below with each one of our core models. Each of these "core models" being the
ones referencing our table schemas.

```csharp
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;

namespace EveryBudgetApi.Models;

public class EveryBudgetDbContext : DbContext
{
    protected readonly IConfiguration Configuration;
    public EveryBudgetDbContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(Configuration.GetConnectionString("LocalDatabase"));
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<BudgetItem> BudgetItems { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
}
```

### Setup model classes

NOTE: This is only one class. Repeat this pattern as needed.

```csharp
using System.ComponentModel.DataAnnotations.Schema;
using Npgsql;

namespace EveryBudgetApi.Models;

[Table("categories")]
public class Category
{
    /* meta-fields, for DB metrics */
    [Column("id")] public Guid Id { get; set; }
    [Column("date_created")] public DateTime DateCreated { get; set; }
    [Column("date_updated")] public DateTime DateUpdated { get; set; }

    /* data fields */
    [Column("name")] public string Name { get; set; }
}
```


### Connect to data

Finally, we can make a query via EntityFramework (or other ORM if you set one up),
and see data returning from the database.

Notice the following:
- Add the context and configuration
- Add a custom constructor, pulling in the custom `DbContext` and `IConfiguration` from the built-in dependency injection framework
- *Make a query in a route.* Now we can use `_context` and have access to those models setup previously.

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using EveryBudgetApi.Models;

namespace EveryBudgetApi.Controllers
{
    [EnableCors("DevelopmentPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BudgetsController : ControllerBase
    {
        private readonly EveryBudgetDbContext _context;
        private readonly IConfiguration _configuration;

        public BudgetsController(EveryBudgetDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        public string Test()
        {
            var test = _context.Categories.ToList();
            return $@"# of Categories: {test.Count()}";
        }
    }
}
```