using EveryBudgetApi.Utilities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using JsonConverter = Newtonsoft.Json.JsonConverter;

namespace EveryBudgetApi.Models
{
    [Table("uploaded_transactions")]
    [PrimaryKey("Id")]
    public class UploadedTransaction
    {
        [Key]
        [Required]
        [Column("id")] public Guid Id { get; set; }
        [Column("date_created")] public DateTime DateCreated { get; set; }

        [Column("budget_id")] public Guid? BudgetId { get; set; }

        [Column("amount")] public string Amount { get; set; }
        [Column("balance")] public string Balance { get; set; }
        [Column("check_number")] public string CheckNumber { get; set; }
        [Column("description")] public string Description { get; set; }
        [Column("effective_date")] public string EffectiveDate { get; set; }
        [Column("extended_description")] public string ExtendedDescription { get; set; }
        [Column("memo")] public string Memo {  get; set; }
        [Column("posting_date")] public string PostingDate { get; set; }
        [Column("reference_number")] public string ReferenceNumber { get; set; }
        [Column("transaction_category")] public string TransactionCategory { get; set; }
        [Column("transaction_id")] public string TransactionID { get; set; }
        [Column("transaction_type")] public string TransactionType { get; set; }
        [Column("type")] public string Type { get; set; }

        [NotMapped]
        public Budget Budget { get; set; }

        public UploadedTransaction() { }

        public UploadedTransaction(UploadedTransactionViewModel viewModel)
        {
            this.Id = Guid.NewGuid();
            this.DateCreated = DateUtilities.DateTimeNowKindUtc();

            this.Amount = viewModel.Amount;
            this.Balance = viewModel.Balance;
            this.CheckNumber = viewModel.CheckNumber;
            this.Description = viewModel.Description;
            this.EffectiveDate = viewModel.EffectiveDate;
            this.ExtendedDescription = viewModel.ExtendedDescription;
            this.Memo = viewModel.Memo;
            this.PostingDate = viewModel.PostingDate;
            this.ReferenceNumber = viewModel.ReferenceNumber;
            this.TransactionCategory = viewModel.TransactionCategory;
            this.TransactionID = viewModel.TransactionID;
            this.TransactionType = viewModel.TransactionType;
            this.Type = viewModel.Type;
        }
    }
}
