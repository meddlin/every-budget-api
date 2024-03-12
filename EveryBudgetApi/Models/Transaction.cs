using EveryBudgetApi.Utilities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace EveryBudgetApi.Models
{
    [Table("transactions")]
    [PrimaryKey("Id")]
    public class Transaction
    {
        /* meta-fields, for DB metrics */
        [Column("id")] public Guid Id { get; set; }
        [Column("date_created")] public DateTime DateCreated { get; set; }
        [Column("date_updated")] public DateTime DateUpdated { get; set; }

        /* keys & relation fields */
        [Column("budget_id")] public Guid? BudgetId { get; set; }
        [Column("budget_item_id")] public Guid? BudgetItemId { get; set; }

        /* data fields */
        [Column("vendor")] public string? Vendor { get; set; }
        [Column("amount")] public decimal? Amount { get; set; }
        [Column("transaction_date")] public DateTime? TransactionDate { get; set; }
        [Column("notes")] public string? Notes { get; set; }

        [NotMapped]
        public BudgetItem BudgetItem { get; set; }

        public Transaction() { }

        public Transaction(string vendor, string amount, string transactionDate)
        {
            Id = Guid.NewGuid();
            DateCreated = DateUtilities.DateTimeNowKindUtc();
            DateUpdated = DateUtilities.DateTimeNowKindUtc();

            // BudgetId
            // BudgetItemId

            Vendor = vendor;
            Amount = decimal.Parse(amount, CultureInfo.InvariantCulture);
            TransactionDate = DateUtilities.MakeDateTimeKindUtc(DateTime.Parse(transactionDate));
        }
    }
}