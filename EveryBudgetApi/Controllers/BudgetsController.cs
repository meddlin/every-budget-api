using Microsoft.AspNetCore.Mvc;
using EveryBudgetApi.ViewModels;
using Microsoft.AspNetCore.Cors;
using EveryBudgetApi.Models;

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


        [HttpGet]
        public string Test()
        {
            var test = _context.Categories.ToList();

            return $@"# of Categories: {test.Count()}";
        }

        // GET: api/values
        [HttpGet]
        public List<BudgetViewModel> Get()
        {
            var x = new BudgetViewModel() {
                Id = Guid.NewGuid(),
                DateUpdated = DateTime.Now,
                Categories = new List<CategoryViewModel>()
                {
                    new CategoryViewModel() { Id = Guid.NewGuid(), DateUpdated = DateTime.Now },
                    new CategoryViewModel() { 
                        Id = Guid.NewGuid(), 
                        DateUpdated = DateTime.Now, 
                        BudgetItems = new List<BudgetItemViewModel>()
                        {
                            new BudgetItemViewModel() { 
                                Id = Guid.NewGuid(), 
                                DateUpdated = DateTime.Now,
                                Name = "Groceries",
                                Planned = 400.00M,
                                Spent = 75.00M,
                                Transactions = new List<TransactionViewModel>()
                                {
                                    new TransactionViewModel() { 
                                        Id = Guid.NewGuid(), 
                                        DateUpdated = DateTime.Now, 
                                        Vendor = "ACME Co.", 
                                        Amount = 35.00M, 
                                        TransactionDate = DateTime.Now 
                                    },
                                    new TransactionViewModel() { 
                                        Id = Guid.NewGuid(), 
                                        DateUpdated = DateTime.Now, 
                                        Vendor = "HEB", 
                                        Amount = 25.00M, 
                                        TransactionDate = DateTime.Now 
                                    },
                                    new TransactionViewModel() { 
                                        Id = Guid.NewGuid(), 
                                        DateUpdated = DateTime.Now, 
                                        Vendor = "Walmart", 
                                        Amount = 15.00M, 
                                        TransactionDate = DateTime.Now 
                                    },
                                }
                            },
                        }
                    },
                }
            };

            return new List<BudgetViewModel>() { x};
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public BudgetViewModel Get(int id)
        {
            return new BudgetViewModel();
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

