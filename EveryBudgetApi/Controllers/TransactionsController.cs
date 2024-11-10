using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EveryBudgetApi.ViewModels;
using EveryBudgetApi.Models;
using System.Globalization;
using Microsoft.AspNetCore.Cors;
using EveryBudgetApi.Utilities;

namespace EveryBudgetApi.Controllers
{
    [EnableCors("DevelopmentPolicy")]
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
           List<EveryBudgetApi.Models.Transaction> data = _context.Transactions.Select(t => t).ToList();
            var tranList = new List<TransactionViewModel>();

            data.ForEach(transaction => tranList.Add(new TransactionViewModel(transaction)));

            return tranList;
        }

        [HttpGet("{id}")]
        public TransactionViewModel Get(int id)
        {
            return new TransactionViewModel();
        }

        [HttpPost("{id}")]
        public object Update([FromBody] TransactionViewModel txnData)
        {
            Console.WriteLine(txnData.ToString());

            //_context.Transactions.Select()

            //_context.Transactions.Update(txnData);
            //_context.SaveChanges();

            return new { Message = "Update successful!" };
        }

        [HttpPost]
        public object RelateToBudgetItem([FromBody] TransactionViewModel vm)
        {
            Console.WriteLine(vm.ToString());

            Transaction trn = _context.Transactions.Select(t => t).Where(t => t.Id == vm.Id).FirstOrDefault();
            BudgetItem bi = _context.BudgetItems.Select(bi => bi).Where(bi => bi.Id == vm.BudgetItemId).FirstOrDefault();

            // TODO: Validate Transaction and BudgetItem are set to instances of real data/objects before continuing.

            // Change amount left to spend for BudgetItem
            bi.Spent = bi.Spent + trn.Amount.Value;
            bi.DateUpdated = DateUtilities.DateTimeNowKindUtc();

            // Relate Transaction to the BudgetItem
            trn.BudgetItemId = vm.BudgetItemId;

            _context.Transactions.Update(trn);
            _context.BudgetItems.Update(bi);

            _context.SaveChanges();

            return new { Message = "Success: Transaction related to BudgetItem" };
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        [HttpDelete("{id}")]
        public object Delete(Guid id)
        {
            var tran = _context.Transactions.FirstOrDefault(x => x.Id == id);
            _context.Transactions.Remove(tran);
            _context.SaveChanges();

            return new { Message = "Transaction deleted." };
        }
    }
}
