using EveryBudgetApi.Models;
using System.ComponentModel;

namespace EveryBudgetApi.ViewModels
{
    public class BudgetItemViewModel
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public string Name { get; set; }
        public decimal Planned { get; set; }
        public decimal Spent { get; set; }

        public List<TransactionViewModel> Transactions { get; set; }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public BudgetItemViewModel() { }

        /// <summary>
        /// Convert BudgetItem to BudgetItemViewModel
        /// </summary>
        /// <param name="budgetItem"></param>
        public BudgetItemViewModel(BudgetItem budgetItem)
        {
            Id = budgetItem.Id;
            DateCreated = budgetItem.DateCreated;
            DateUpdated = budgetItem.DateUpdated;
            Name = budgetItem.Name;
            Planned = budgetItem.Planned;
            Spent = budgetItem.Spent;
            Transactions = new List<TransactionViewModel>();
        }
    }
}
