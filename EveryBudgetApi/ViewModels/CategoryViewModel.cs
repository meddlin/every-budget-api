using EveryBudgetApi.Models;
using System.ComponentModel.DataAnnotations;

namespace EveryBudgetApi.ViewModels;

public class CategoryViewModel
{
    [Required]
    public Guid Id { get; set; }

    public DateTime DateUpdated { get; set; }

    [Required]
    public string Name { get; set; }

    public List<BudgetItemViewModel>? BudgetItems { get; set; }


    public CategoryViewModel() { }

    public CategoryViewModel(Category category) 
    { 
        Id = category.Id;
        DateUpdated = category.DateUpdated;
        Name = category.Name;

        BudgetItems = new List<BudgetItemViewModel>();
        foreach(var item in category.BudgetItems)
        {
            BudgetItems.Add(new BudgetItemViewModel(item));
        }
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