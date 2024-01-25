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
    }
}
