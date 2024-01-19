using System.ComponentModel.DataAnnotations.Schema;
using Npgsql;

namespace EveryBudgetApi.Models;

[Table("categories")]
public class Category
{
    [Column("id")] public Guid Id { get; set; }
    [Column("date_updated")] public DateTime DateUpdated { get; set; }
}