using EveryBudgetApi.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EveryBudgetApi.ViewModels
{
    public class UploadedTransactionViewModel
    {
        public Guid Id { get; set; }
        public string Amount { get; set; }
        public string Balance { get; set; }
        public string CheckNumber { get; set; }
        public string Description { get; set; }
        public string EffectiveDate { get; set; }
        public string ExtendedDescription { get; set; }
        public string Memo { get; set; }
        public string PostingDate { get; set; }
        public string ReferenceNumber { get; set; }
        public string TransactionCategory { get; set; }
        public string TransactionType { get; set; }
        public string Type { get; set; }

        public UploadedTransactionViewModel() { }

        public UploadedTransactionViewModel(UploadedTransaction uplTran) 
        {
            this.Id = uplTran.Id;
            this.Amount = uplTran.Amount;
            this.Balance = uplTran.Balance;
            this.CheckNumber = uplTran.CheckNumber;
            this.Description = uplTran.Description;
            this.EffectiveDate = uplTran.EffectiveDate;
            this.ExtendedDescription = uplTran.ExtendedDescription;
            this.Memo = uplTran.Memo;
            this.PostingDate = uplTran.PostingDate;
            this.ReferenceNumber = uplTran.ReferenceNumber;
            this.TransactionCategory = uplTran.TransactionCategory;
            this.TransactionType = uplTran.TransactionType;
            this.Type = uplTran.Type;
        }
    }
}
