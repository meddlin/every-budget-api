using EveryBudgetApi.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EveryBudgetApi.ViewModels
{
    public class BudgetItemViewModel
    {
        [Required]
        public Guid Id { get; set; }
        public DateTime DateUpdated { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Planned { get; set; }
        [Required]
        public decimal Spent { get; set; }

        public List<TransactionViewModel>? Transactions { get; set; }

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
            DateUpdated = budgetItem.DateUpdated;

            Name = budgetItem.Name;
            Planned = budgetItem.Planned;
            Spent = budgetItem.Spent;

            Transactions = new List<TransactionViewModel>();
            foreach(var item in budgetItem.Transactions)
            {
                Transactions.Add(new TransactionViewModel(item));
            }
        }
    }
}
