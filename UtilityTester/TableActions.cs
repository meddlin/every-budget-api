using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityTester
{
    internal class TableActions
    {
        public static List<string> ListTables(EveryBudgetDbContext dbContext)
        {
            using (dbContext)
            {
                FormattableString sql = $"select table_name from information_schema.tables";
                var tables = dbContext.Database.SqlQuery<string>(sql).ToList();

                return tables;
            }
        }
    }
}
