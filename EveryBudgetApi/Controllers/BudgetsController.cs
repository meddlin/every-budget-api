using Microsoft.AspNetCore.Mvc;
using EveryBudgetApi.ViewModels;
using Microsoft.AspNetCore.Cors;
using EveryBudgetApi.Models;
using Microsoft.EntityFrameworkCore;
using EveryBudgetApi.Utilities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EveryBudgetApi.Controllers
{
    [EnableCors("DevelopmentPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BudgetsController : ControllerBase
    {
        private readonly EveryBudgetDbContext _context;
        private readonly IConfiguration _configuration;

        public BudgetsController(EveryBudgetDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Get most recent <c>Budget</c>
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ViewModels.BudgetViewModel Get()
        {
            // TODO: Ensure this is actually the most recent. For now, we've cheated the
            //          data to just select one at random. The easiest implementation
            //          might be to set a flag in the database for "most recent".
            Budget budget = _context.Budgets
                                .Select(b => b)
                                .Include(b => b.UploadedTransactions)
                                .Include(b => b.Categories)
                                    .ThenInclude(c => c.BudgetItems)
                                    .ThenInclude(bi => bi.Transactions)
                                .FirstOrDefault();

            return new ViewModels.BudgetViewModel(budget);
        }

        /// <summary>
        /// Get a specific <c>Budget</c> by <c>Id</c>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ViewModels.BudgetViewModel Get(Guid id)
        {
            Budget budget = _context.Budgets
                .Select(b => b).Where(b => b.Id == id)
                    .Include(b => b.Categories)
                        .ThenInclude(c => c.BudgetItems)
                        .ThenInclude(bi => bi.Transactions)
                .FirstOrDefault();

            return new ViewModels.BudgetViewModel(budget);
        }

        [HttpPost]
        public object UpdateBudget([FromBody] BudgetViewModel vm)
        {
            Budget? data = _context.Budgets.Select(b => b).Where(b => b.Id == vm.Id).FirstOrDefault();
            data.DateUpdated = DateUtilities.DateTimeNowKindUtc();

            data.Name = vm.Name;
            // TODO : Update Budget.description too

            _context.Update(data);
            _context.SaveChanges();

            return new { Message = "Budget Update successful!" };
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

