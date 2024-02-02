using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EveryBudgetApi.Models;


public class Budget 
{
    public Guid Id { get; set; }
    public DateTime DateUpdated { get; set; }

    public List<EveryBudgetCore.Models.Category> Categories { get; set; }
}