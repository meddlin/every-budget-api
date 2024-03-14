using EveryBudgetApi.Models;
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

    public DbSet<EveryBudgetApi.Models.Budget> Budgets { get; set; }
    public DbSet<EveryBudgetApi.Models.Category> Categories { get; set; }
    public DbSet<EveryBudgetApi.Models.BudgetItem> BudgetItems { get; set; }
    public DbSet<EveryBudgetApi.Models.Transaction> Transactions { get; set; }
}