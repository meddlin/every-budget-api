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
    public class CategoriesController : ControllerBase
    {
        private readonly EveryBudgetDbContext _context;
        private readonly IConfiguration _configuration;

        public CategoriesController(EveryBudgetDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        public List<Category> Get()
        {
            return _context.Categories.ToList();
        }

        [HttpPut]
        public string AddCategory([FromBody] Category category)
        {
            _context.Add(category);
            _context.SaveChanges();

            return "Category Added";
        }

        [HttpPost]
        public string UpdateCategory([FromBody] Category category)
        {
            _context.Update(category);
            _context.SaveChanges();

            return "Category Updated";
        }
    }
}
