using System.ComponentModel.DataAnnotations.Schema;
using Npgsql;

namespace EveryBudgetApi.Models;

[Table("categories")]
public class Category
{
    /* meta-fields, for DB metrics */
    [Column("id")] public Guid Id { get; set; }
    [Column("date_created")] public DateTime DateCreated { get; set; }
    [Column("date_updated")] public DateTime DateUpdated { get; set; }

    /* data fields */
    [Column("name")] public string Name { get; set; }
}