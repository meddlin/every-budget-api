using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EveryBudgetApi.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EveryBudgetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetsController : ControllerBase
    {
        // GET: api/values
        [HttpGet]
        public List<BudgetViewModel> Get()
        {
            var x = new BudgetViewModel() {
                Id = Guid.NewGuid(),
                DateUpdated = DateTime.Now,
                Categories = new List<CategoryViewModel>()
            };

            return new List<BudgetViewModel>() { x };
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

