using MVVMHelper.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVMHelper.StringSearchStrategy
{
    public class NoQueryBreakdownStrategy : IQueryBreakdownStrategy
    {
        public IEnumerable<string> Breakdown(string query)
        {
            return new List<string> { query };
        }
    }
}
