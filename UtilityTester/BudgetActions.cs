using Bogus;
using EveryBudgetApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveryBudgetApi.Utilities;

namespace UtilityTester
{
    internal class BudgetActions
    {
        public static List<Category> GenerateSampleBudget()
        {
            Randomizer.Seed = new Random(8675309);

            var categories = new Faker<EveryBudgetApi.Models.Category>()
                .RuleFor(c => c.Id, f => f.Random.Guid())
                .RuleFor(c => c.DateCreated, f => f.Date.Past())
                .RuleFor(c => c.DateUpdated, f => f.Date.Recent())
                .RuleFor(c => c.Name, f => f.Lorem.Word());

            List<EveryBudgetApi.Models.Category> test = categories.Generate(3);
            // Correct the DateTimeKind for categories
            foreach (var category in test)
            {
                category.DateCreated = DateUtilities.MakeDateTimeKindUtc(category.DateCreated);
                category.DateUpdated = DateUtilities.MakeDateTimeKindUtc(category.DateUpdated);
            }

            var budgetItemGenerator = new Faker<EveryBudgetApi.Models.BudgetItem>()
                .RuleFor(b => b.Id, f => f.Random.Guid())
                .RuleFor(b => b.DateCreated, f => f.Date.Past())
                .RuleFor(b => b.DateUpdated, f => f.Date.Recent())
                //.RuleFor(b => b.CategoryId, f => f.Random.Guid())
                .RuleFor(b => b.Name, f => f.Lorem.Word())
                .RuleFor(b => b.Planned, f => f.Random.Decimal())
                .RuleFor(b => b.Spent, f => f.Random.Decimal());
            List<EveryBudgetApi.Models.BudgetItem> budgetItems = budgetItemGenerator.Generate(3);
            budgetItems[0].CategoryId = test[0].Id;
            budgetItems[1].CategoryId = test[0].Id;
            budgetItems[2].CategoryId = test[0].Id;

            // Correct the DateTimeKind for BudgetItems
            foreach (var item in budgetItems)
            {
                item.DateCreated = DateUtilities.MakeDateTimeKindUtc(item.DateCreated);
                item.DateUpdated = DateUtilities.MakeDateTimeKindUtc(item.DateUpdated);
            }

            return test;
        }

        public static void ListDataInTables(EveryBudgetDbContext db)
        {
            using (db)
            {
                Console.WriteLine($"Categories: contains ({db.Categories.Count()})");
                foreach (var category in db.Categories)
                {
                    Console.WriteLine($"  {category.Name}");
                }

                // TODO: Add ability to check if table exists
                //      NOTE: This will require custom SQL: https://stackoverflow.com/questions/6100969/entity-framework-how-to-check-if-table-exists
                Console.WriteLine($"BudgetItems: contains ({db.BudgetItems.Count()})");
                foreach (var budgetItem in db.BudgetItems)
                {
                    Console.WriteLine($"  {budgetItem.Name}");
                }

                Console.WriteLine($"Transactions: contains ({db.Transactions.Count()})");
                foreach (var transaction in db.Transactions)
                {
                    Console.WriteLine($"  {transaction.Vendor}");
                }
            }
        }


        public static void GenerateConnectedBudgetModels()
        {
            Randomizer.Seed = new Random(8675309);

            var budgetGenerator = new Faker<Budget>()
                .RuleFor(b => b.Id, f => f.Random.Guid())
                .RuleFor(b => b.DateCreated, f => f.Date.Past())
                .RuleFor(b => b.DateUpdated, f => f.Date.Recent())
                .RuleFor(b => b.Name, f => f.Lorem.Word());

            var categoryGenerator = new Faker<EveryBudgetApi.Models.Category>()
                .RuleFor(c => c.Id, f => f.Random.Guid())
                .RuleFor(c => c.DateCreated, f => f.Date.Past())
                .RuleFor(c => c.DateUpdated, f => f.Date.Recent())
                .RuleFor(c => c.Name, f => f.Lorem.Word());

            Budget budget = budgetGenerator.Generate(1).Single();
        }

        /*
         * Store generated Categories in the database
         */
        public static void StoreCategories(EveryBudgetDbContext db, List<Category> categories)
        {
            db.Categories.AddRange(categories);
            db.SaveChanges();
        }

        public static void StoreTransactions(EveryBudgetDbContext db, List<Transaction> transactions)
        {
            db.Transactions.AddRange(transactions);
            db.SaveChanges();
        }
    }
}
