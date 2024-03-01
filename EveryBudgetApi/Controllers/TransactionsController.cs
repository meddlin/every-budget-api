using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EveryBudgetApi.ViewModels;
using EveryBudgetApi.Models;
using EveryBudgetCore.Models;

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

        [HttpPost]
        public object Upload([FromBody] List<UploadedTransaction> data)
        {
            Console.WriteLine(data.ToString());

            //foreach( var item in data)
            //{
            //    // NOTE: https://stackoverflow.com/questions/2246694/how-can-i-connvert-a-json-object-to-a-custom-c-sharp-object
            //    var myCustom = Newtonsoft.Json.JsonConvert.DeserializeObject<UploadedTransaction>(item.ToString());
            //    Console.WriteLine(myCustom);
            //}

            foreach(var d in data)
            {
                Console.WriteLine(d);
            }

            return new { data = "from /upload" };
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
