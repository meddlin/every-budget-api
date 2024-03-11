using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryBudgetCore.Models
{
    [Table("budgets")]
    public class Budget
    {
        /* meta-fields, for DB metrics */
        [Column("id")] public Guid Id { get; set; }
        [Column("date_created")] public DateTime DateCreated { get; set; }
        [Column("date_updated")] public DateTime DateUpdated { get; set; }

        /* data fields */
        [Column("name")] public string Name { get; set; }
        [Column("description")] public string Description { get; set; }


        public void QueryFullBudget(DbContext db)
        {
            
        }
    }
}
