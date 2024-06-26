using EveryBudgetApi.Models;
using System.ComponentModel.DataAnnotations;

namespace EveryBudgetApi.ViewModels;

public class BudgetViewModel
{
    [Required]
    public Guid Id { get; set; }

    public DateTime DateUpdated { get; set; }

    [Required]
    public string Name { get; set; }

    public string? Description { get; set; }

    public List<CategoryViewModel>? Categories { get; set; }
    public List<UploadedTransactionViewModel>? UploadedTransactions { get; set; }

    /// <summary>
    /// Empty constructor
    /// </summary>
    public BudgetViewModel() { }

    public BudgetViewModel(Budget budget)
    {
        Id = budget.Id;
        DateUpdated = budget.DateUpdated;
        Name = budget.Name;
        Description = budget.Description;

        Categories = new List<CategoryViewModel>();
        UploadedTransactions = new List<UploadedTransactionViewModel>();
        foreach (var item in budget.Categories)
        {
            Categories.Add(new CategoryViewModel(item));   
        }
        foreach(var item in budget.UploadedTransactions)
        {
            UploadedTransactions.Add(new UploadedTransactionViewModel(item));
        }
    }

    /// <summary>
    /// Constructor - to convert a collection of <c>Category</c> objects
    /// </summary>
    /// <param name="categories"></param>
    public BudgetViewModel(IEnumerable<Category> categories)
    {
        var vm = new BudgetViewModel();

        foreach (var category in categories)
        {
            vm.Categories.Add(new CategoryViewModel(category));
        }
    }

    public BudgetViewModel(IEnumerable<CategoryViewModel> categories)
    {
        Categories = categories.ToList();
    }
}