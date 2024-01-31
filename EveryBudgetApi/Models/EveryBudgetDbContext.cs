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