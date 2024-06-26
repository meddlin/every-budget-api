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
        public object Update([FromBody] Transaction txnData)
        {
            Console.WriteLine(txnData.ToString());

            _context.Transactions.Update(txnData);
            _context.SaveChanges();

            return new { Message = "Update successful!" };
        }

        [HttpPost]
        public object RelateToBudgetItem([FromBody] TransactionViewModel vm)
        {
            Console.WriteLine(vm.ToString());

            Transaction trn = _context.Transactions.Select(t => t).Where(t => t.Id == vm.Id).FirstOrDefault();
            BudgetItem bi = _context.BudgetItems.Select(bi => bi).Where(bi => bi.Id == vm.BudgetItemId).FirstOrDefault();

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

        //[HttpPost]
        //public object Upload([FromBody] List<UploadedTransaction> data)
        //{
        //    // Store each UploadTransaction in database
        //    // Convert each UploadTransaction to Transaction
        //    // Store transactions in database

        //    var txns = new List<Transaction>();

        //    foreach(var d in data)
        //    {
        //        var txn = new Transaction(vendor: d.Description, amount: d.Amount, transactionDate: d.EffectiveDate);
        //        _context.Transactions.Add(txn);
        //    }

        //    _context.SaveChanges();

        //    // TODO: Build a better success message to send back to client
        //    return new { Message = "Upload successful!" };
        //}

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
