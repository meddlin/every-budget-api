using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryBudgetCore
{
    public class DateUtilities
    {
        /// <summary>
        /// Create a new DateTime with the DateTimeKind set to UTC.
        /// </summary>
        /// <returns></returns>
        public static DateTime DateTimeNowKindUtc()
        {
            var date = DateTime.Now;
            return DateTime.SpecifyKind(date, DateTimeKind.Utc);
        }

        /// <summary>
        /// Change the DateTimeKind to UTC on an existing DateTime.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime MakeDateTimeKindUtc(DateTime date)
        {
            return DateTime.SpecifyKind(date, DateTimeKind.Utc);
        }
    }
}
