using EveryBudgetCore.Models;

namespace EveryBudgetApi.ViewModels;

public class CategoryViewModel
{
    public Guid Id { get; set; }
    public DateTime DateUpdated { get; set; }

    public string Name { get; set; }
    public List<BudgetItemViewModel> BudgetItems { get; set; }

    public CategoryViewModel() { }

    public CategoryViewModel(Category category) 
    { 
        Id = category.Id;
        DateUpdated = category.DateUpdated;
        Name = category.Name;
    }

    public CategoryViewModel(Category category, IEnumerable<BudgetItem> budgetItems)
    {
        Id = category.Id;
        DateUpdated = category.DateUpdated;
        Name = category.Name;
        BudgetItems = new List<BudgetItemViewModel>();

        foreach(var item in budgetItems)
        {
            BudgetItems.Add(new BudgetItemViewModel(item));
        }
    }
}