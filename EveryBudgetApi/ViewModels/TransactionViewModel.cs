namespace EveryBudgetApi.ViewModels;

public class TransactionViewModel
{
    public Guid Id { get; set; }
    public DateTime DateUpdated { get; set; }

    public string Vendor { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
}