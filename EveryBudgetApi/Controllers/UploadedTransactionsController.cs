using EveryBudgetApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EveryBudgetApi.Controllers
{
    [EnableCors("DevelopmentPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UploadedTransactionsController : ControllerBase
    {
        private readonly EveryBudgetDbContext _context;
        private readonly IConfiguration _configuration;

        public UploadedTransactionsController(EveryBudgetDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        public object Upload([FromBody] List<UploadedTransactionViewModel> data)
        {
            var tranList = new List<UploadedTransaction>();

            foreach(var d in data)
            {
                var txn = new UploadedTransaction(d);
                _context.UploadedTransactions.Add(txn);
            }
            _context.SaveChanges();

            // TODO: Build a better success message to send back to client
            return new { Message = "Upload successful!" };
        }
    }
}
