using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EveryBudgetCore.Models
{
    public class Transaction
    {
        /* meta-fields, for DB metrics */
        [Column("id")] public Guid Id { get; set; }
        [Column("date_created")] public DateTime DateCreated { get; set; }
        [Column("date_updated")] public DateTime DateUpdated { get; set; }

        /* keys & relation fields */
        [Column("category_id")] public Guid CategoryId { get; set; }
        // [Column("budget_id")] public Guid BudgetId { get; set; }

        /* data fields */
        [Column("vendor")] public string Vendor { get; set; }
        [Column("amount")] public decimal Amount { get; set; }
        [Column("transaction_date")] public DateTime TransactionDate { get; set; }
    }
}