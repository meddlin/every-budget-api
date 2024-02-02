using System.ComponentModel.DataAnnotations.Schema;

namespace EveryBudgetCore.Models
{
    [Table("budget_items")]
    public class BudgetItem
    {
        /* meta-fields, for DB metrics */
        [Column("id")] public Guid Id { get; set; }
        [Column("date_created")] public DateTime DateCreated { get; set; }
        [Column("date_updated")] public DateTime DateUpdated { get; set; }

        /* keys & relation fields */
        [Column("category_id")] public Guid CategoryId { get; set; }
        // [Column("budget_id")] public Guid BudgetId { get; set; }

        /* data fields */
        [Column("name")] public string Name { get; set; }
        [Column("planned")] public decimal Planned { get; set; }
        [Column("spent")] public decimal Spent { get; set; }

    }
}
