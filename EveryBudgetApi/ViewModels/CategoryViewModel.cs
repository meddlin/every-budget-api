namespace EveryBudgetApi.ViewModels;

public class CategoryViewModel
{
    public Guid Id { get; set; }
    public DateTime DateUpdated { get; set; }

    public string Name { get; set; }
    public List<BudgetItemViewModel> BudgetItems { get; set; }
}