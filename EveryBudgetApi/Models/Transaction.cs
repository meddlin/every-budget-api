namespace EveryBudgetApi.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Transaction
{
    [Column("id")] public Guid Id { get; set; }
    [Column("date_updated")] public DateTime DateUpdated { get; set; }
}