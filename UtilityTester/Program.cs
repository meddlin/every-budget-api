using Bogus;
using EveryBudgetCore.Models;
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

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Utility Tester!");
            Console.WriteLine("");

            var dbExists = new EveryBudgetDbContext().Database.EnsureCreated();
            Console.WriteLine($"Database exists: {dbExists}");

            var canConnect = new EveryBudgetDbContext().Database.CanConnect();
            Console.WriteLine($"Can connect: {canConnect}");

            // TableActions.ListTables(new EveryBudgetDbContext());

            //List<Category> categories = BudgetActions.GenerateSampleBudget();
            //using (var db = new EveryBudgetDbContext())
            //{
            //    BudgetActions.StoreCategories(db, categories);
            //}

            BudgetActions.StoreTransactions(
                new EveryBudgetDbContext(), TransactionGenerator.Generate()
            );


            Console.WriteLine("done inserting...");
        }
    }
}