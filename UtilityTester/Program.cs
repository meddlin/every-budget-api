using Bogus;
using Microsoft.EntityFrameworkCore;
using EveryBudgetApi.Models;

namespace UtilityTester
{
    public class EveryBudgetDbContext : DbContext
    {
        public DbSet<EveryBudgetApi.Models.Budget> Budgets { get; set; }
        public DbSet<EveryBudgetApi.Models.Category> Categories { get; set; }
        public DbSet<EveryBudgetApi.Models.BudgetItem> BudgetItems { get; set; }
        public DbSet<EveryBudgetApi.Models.Transaction> Transactions { get; set; }

        // TODO: Test if `localhost' or 'localhost:5432' works when the DB is running in Docker
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost:5432;Database=every-budget;Username=meddlin;Password=jailbreak");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Budget>()
                .HasMany(b => b.Categories)
                .WithOne(e => e.Budget);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.BudgetItems)
                .WithOne(bi => bi.Category);

            modelBuilder.Entity<BudgetItem>()
                .HasMany(bi => bi.Transactions)
                .WithOne(t => t.BudgetItem);
        }
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

            //BudgetActions.StoreTransactions(
            //    new EveryBudgetDbContext(), TransactionGenerator.Generate()
            //);

            var b = BudgetActions.GenerateConnectedBudgetModels();
            var _ctx = new EveryBudgetDbContext();
            var res = _ctx.Budgets.Add(b);
            _ctx.SaveChanges();
            Console.WriteLine(res);


            // Shows querying Budgets -> Categories
            var data = new EveryBudgetDbContext().Budgets.Select(b => b)
                .Include(b => b.Categories).ToList();

            // Shows querying Categories -> BudgetItems
            var data2 = new EveryBudgetDbContext().Categories.Select(c => c)
                .Include(c => c.BudgetItems).ToList();

            var data3 = new EveryBudgetDbContext().BudgetItems.Select(bi => bi)
                .Include(bi => bi.Transactions).ToList();

            // Shows able to query Categories without data relationships
            var categories = new EveryBudgetDbContext().Categories.Select(c => c).ToList();

            var all = new EveryBudgetDbContext().Budgets
                .Select(b => b)
                .Include(b => b.Categories)
                .ThenInclude(c => c.BudgetItems)
                .ThenInclude(bi => bi.Transactions)
                .ToList();

            Console.WriteLine(data);


            Console.WriteLine("done inserting...");
        }
    }
}