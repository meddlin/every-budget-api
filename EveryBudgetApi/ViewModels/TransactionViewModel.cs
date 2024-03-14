using EveryBudgetApi.Models;

namespace EveryBudgetApi.ViewModels;

public class TransactionViewModel
{
    public Guid Id { get; set; }
    public DateTime DateUpdated { get; set; }

    public string Vendor { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Notes { get; set; }

    
    public TransactionViewModel() {}

    public TransactionViewModel(EveryBudgetApi.Models.Transaction tran)
    {
        Id = tran.Id;
        DateUpdated = tran.DateUpdated;

        Vendor = tran.Vendor;
        Amount = tran.Amount.Value;
        TransactionDate = tran.TransactionDate.Value;
        Notes = tran.Notes;
    }
}