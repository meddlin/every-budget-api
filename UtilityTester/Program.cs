using Bogus;
using Microsoft.EntityFrameworkCore;

namespace UtilityTester
{
    public class EveryBudgetDbContext : DbContext
    {
        public DbSet<EveryBudgetCore.Models.Category> Categories { get; set; }
        public DbSet<EveryBudgetCore.Models.BudgetItem> BudgetItems { get; set; }
        public DbSet<EveryBudgetCore.Models.Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=192.168.1.12;Database=every-budget;Username=testuser;Password=password1");
    }

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

    internal class Program
    {
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
    }
}