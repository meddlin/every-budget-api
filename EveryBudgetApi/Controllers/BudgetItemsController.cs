using EveryBudgetApi.Models;
using EveryBudgetApi.Utilities;
using EveryBudgetApi.ViewModels;
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

        [HttpGet]
        public List<BudgetItem> Get()
        {
            return _context.BudgetItems.Select(x => x).ToList();
        }

        [HttpPut]
        public string AddBudgetItem([FromBody] BudgetItem budgetItem)
        {
            _context.Add(budgetItem);
            _context.SaveChanges();

            return "Budget Item Added";
        }

        [HttpPost]
        public object UpdateBudgetItem([FromBody] BudgetItemViewModel vm)
        {
            BudgetItem? data = _context.BudgetItems.Select(bi => bi).Where(bi => bi.Id == vm.Id).FirstOrDefault();
            data.DateUpdated = DateUtilities.DateTimeNowKindUtc();

            // User-controlled fields to update
            data.Name = vm.Name;
            data.Planned = vm.Planned;
            data.Spent = vm.Spent;

            _context.Update(data);
            _context.SaveChanges();

            return new { Message = "BudgetItem Update successful!" };
        }
    }
}
