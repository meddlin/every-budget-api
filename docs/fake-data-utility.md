# Utility - Fake Data

With this being a highly data-driven application, it's useful to be able to 
generate some "fake" data for easier testing. Ultimately, Bogus is the library we'll use to generate data.

[https://github.com/bchavez/Bogus](https://github.com/bchavez/Bogus)


### Re-arranging the Solution

1. Create a console app
2. Create a class library project
3. Add a reference to the class library, in the console app and web API projects

### Console app: create the `DbContext` class

In the console app, create a `DbContext` class. This is how we'll communicate 
to Postgres.

```csharp
public class EveryBudgetDbContext : DbContext
{
    public DbSet<EveryBudgetCore.Models.Category> Categories { get; set; }
    public DbSet<EveryBudgetCore.Models.BudgetItem> BudgetItems { get; set; }
    public DbSet<EveryBudgetCore.Models.Transaction> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=192.168.1.12;Database=every-budget;Username=testuser;Password=password1");
}
```

### Create generators with Bogus

Here, we're creating sample objects, but not doing anything with them yet.

```csharp
public class FakeBudget
{
    public static List<EveryBudgetCore.Models.Category> TestFunction()
    {
        Randomizer.Seed = new Random(8675309);

        var categories = new Faker<EveryBudgetCore.Models.Category>()
            .RuleFor(c => c.Id, f => f.Random.Guid())
            .RuleFor(c => c.DateCreated, f => f.Date.Past())
            .RuleFor(c => c.DateUpdated, f => f.Date.Recent())
            .RuleFor(c => c.Name, f => f.Lorem.Word());

        List<EveryBudgetCore.Models.Category> test = categories.Generate(1);

        var budgetItemGenerator = new Faker<EveryBudgetCore.Models.BudgetItem>()
            .RuleFor(b => b.Id, f => f.Random.Guid())
            .RuleFor(b => b.DateCreated, f => f.Date.Past())
            .RuleFor(b => b.DateUpdated, f => f.Date.Recent())
            //.RuleFor(b => b.CategoryId, f => f.Random.Guid())
            .RuleFor(b => b.Name, f => f.Lorem.Word())
            .RuleFor(b => b.Planned, f => f.Random.Decimal())
            .RuleFor(b => b.Spent, f => f.Random.Decimal());
        List<EveryBudgetCore.Models.BudgetItem> budgetItems = budgetItemGenerator.Generate(2);
        budgetItems[0].CategoryId = test[0].Id;
        budgetItems[1].CategoryId = test[0].Id;
        budgetItems[2].CategoryId = test[0].Id;

        return test;
    }
}
```

### Call the databse from `Main()`

Note: Nothing is being stored yet, but this is how we start connecting things together.

```csharp
static void Main(string[] args)
{
    Console.WriteLine("Welcome to Utility Tester!");
    Console.WriteLine("");

    FakeBudget.TestFunction();

    using (var db = new EveryBudgetDbContext())
    {
        Console.WriteLine($"Categories: contains ({db.Categories.Count()})");
        foreach (var category in db.Categories)
        {
            Console.WriteLine($"  {category.Name}");
        }

        // TODO: Add ability to check if table exists
        //      NOTE: This will require custom SQL: https://stackoverflow.com/questions/6100969/entity-framework-how-to-check-if-table-exists
        Console.WriteLine($"BudgetItems: contains ({db.BudgetItems.Count()})");
        foreach (var budgetItem in db.BudgetItems)
        {
            Console.WriteLine($"  {budgetItem.Name}");
        }

        Console.WriteLine($"Transactions: contains ({db.Transactions.Count()})");
        foreach (var transaction in db.Transactions)
        {
            Console.WriteLine($"  {transaction.Vendor}");
        }
    }
}
```


### References

Console app + Postgres
- Setup EntityFramework and Npgsql to interact with Postgres from a console app
- [https://community.ibm.com/community/user/powerdeveloper/blogs/alhad-deshpande/2023/01/17/c-to-postgresql-using-efcore-crud-operations](https://community.ibm.com/community/user/powerdeveloper/blogs/alhad-deshpande/2023/01/17/c-to-postgresql-using-efcore-crud-operations)

Bogus for .NET/C#
- Data faking library for .NET languages
- [https://github.com/bchavez/Bogus](https://github.com/bchavez/Bogus)