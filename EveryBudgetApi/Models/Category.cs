using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EveryBudgetApi.Models
{
    [Table("categories")]
    [PrimaryKey("Id")]
    public class Category
    {
        /* meta-fields, for DB metrics */
        [Key]
        [Required]
        [Column("id")] 
        public Guid Id { get; set; }
        [Column("date_created")] public DateTime DateCreated { get; set; }
        [Column("date_updated")] public DateTime DateUpdated { get; set; }

        [Column("budget_id")] public Guid? BudgetId { get; set; }

        /* data fields */
        [Column("name")] public string Name { get; set; }

        /* Relational fields */
        [NotMapped]
        public Budget Budget { get; set; }
        [NotMapped]
        public ICollection<BudgetItem> BudgetItems { get; set; }
    }
}