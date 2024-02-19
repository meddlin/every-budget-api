using EveryBudgetApi.Models;
using EveryBudgetCore.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EveryBudgetApi.Controllers
{
    [EnableCors("DevelopmentPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BudgetItemsController : ControllerBase
    {
        private readonly EveryBudgetDbContext _context;
        private readonly IConfiguration _configuration;

        public BudgetItemsController(EveryBudgetDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPut]
        public string AddBudgetItem([FromBody] BudgetItem budgetItem)
        {
            _context.Add(budgetItem);
            _context.SaveChanges();

            return "Budget Item Added";
        }

        [HttpPost]
        public string UpdateBudgetItem([FromBody] BudgetItem budgetItem)
        {
            _context.Update(budgetItem);
            _context.SaveChanges();

            return "Budget Item Updated";
        }
    }
}
