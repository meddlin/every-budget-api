using EveryBudgetCore.Models;

namespace EveryBudgetApi.ViewModels;

public class BudgetViewModel
{
    public Guid Id { get; set; }
    public DateTime DateUpdated { get; set; }

    public List<CategoryViewModel> Categories { get; set; }

    /// <summary>
    /// Empty constructor
    /// </summary>
    public BudgetViewModel() { }

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