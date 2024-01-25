namespace EveryBudgetApi.ViewModels;

public class BudgetViewModel
{
    public Guid Id { get; set; }
    public DateTime DateUpdated { get; set; }

    public List<CategoryViewModel> Categories { get; set; }
}