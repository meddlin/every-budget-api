using Bogus;
using EveryBudgetApi.Models;
using EveryBudgetApi.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityTester
{
    internal class TransactionGenerator
    {
        public static List<EveryBudgetApi.Models.Transaction> Generate()
        {
            Randomizer.Seed = new Random(8675309);

            var transactions = new Faker<EveryBudgetApi.Models.Transaction>()
                .RuleFor(t => t.Id, f => f.Random.Guid())
                .RuleFor(t => t.DateCreated, f => DateUtilities.MakeDateTimeKindUtc(f.Date.Past()))
                .RuleFor(t => t.DateUpdated, f => DateUtilities.MakeDateTimeKindUtc(f.Date.Recent()))

                .RuleFor(t => t.Vendor, f => f.Lorem.Word())
                .RuleFor(t => t.Amount, f => f.Finance.Amount());

            return transactions.Generate(10);
        }
    }
}
