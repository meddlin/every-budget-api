using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EveryBudgetApi.ViewModels;
using EveryBudgetApi.Models;

namespace EveryBudgetApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly EveryBudgetDbContext _context;
        private readonly IConfiguration _configuration;

        public TransactionsController(EveryBudgetDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        public IEnumerable<TransactionViewModel> Get()
        {
            List<EveryBudgetCore.Models.Transaction> data = _context.Transactions.Select(t => t).ToList();
            var tranList = new List<TransactionViewModel>();

            data.ForEach(transaction => tranList.Add(new TransactionViewModel(transaction)));

            return tranList;
        }

        [HttpGet("{id}")]
        public TransactionViewModel Get(int id)
        {
            return new TransactionViewModel();
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
