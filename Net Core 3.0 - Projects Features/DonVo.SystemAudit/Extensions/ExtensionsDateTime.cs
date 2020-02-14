using System;
using System.Collections.Generic;
using System.Linq;

namespace DonVo.SystemAudit.Extensions
{
    public static class ExtensionsDateTime
    {
        internal static IEnumerable<string> GetListPartitionings(this DateTime startDate, DateTime endDate)
        {
            if (startDate.Year == endDate.Year && startDate.Month == endDate.Month)
            {
                return new List<string> { $"audit_y{startDate.Year}_m{startDate.Month}" };
            }
            var listDateTime = Enumerable.Range(0, endDate.Subtract(startDate).Days + 1).Select(d => startDate.AddDays(d));
            return listDateTime.Select(dateTime => $"audit_y{dateTime.Year}_m{dateTime.Month}").ToList().Distinct();
        }
    }
}
