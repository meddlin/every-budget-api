using EveryBudgetApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryBudgetApi.Models
{
    [Table("budgets")]
    [PrimaryKey("Id")]
    public class Budget
    {
        /* meta-fields, for DB metrics */
        [Column("id")] public Guid Id { get; set; }
        [Column("date_created")] public DateTime DateCreated { get; set; }
        [Column("date_updated")] public DateTime DateUpdated { get; set; }

        /* data fields */
        [Column("name")] public string Name { get; set; }
        [Column("description")] public string Description { get; set; }

        /* Relational fields */
        [NotMapped]
        public ICollection<Category> Categories { get; set;  } = new List<Category>();

        public void QueryFullBudget(EveryBudgetDbContext db, string budgetName)
        {
            var fullBudget = db.Budgets
                .Select(b => b)
                .Where(b => b.Name == budgetName)
                .Include(b => b.Categories)
                .ThenInclude(c => c.BudgetItems)
                .ThenInclude(bi => bi.Transactions)
                .ToList();

            Console.WriteLine(fullBudget);
        }
    }
}
