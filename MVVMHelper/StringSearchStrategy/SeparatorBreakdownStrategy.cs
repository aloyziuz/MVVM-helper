using MVVMHelper.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVMHelper.StringSearchStrategy
{
    public class SeparatorBreakdownStrategy : IQueryBreakdownStrategy
    {
        private char separator;

        public SeparatorBreakdownStrategy(char separator)
        {
            this.separator = separator;
        }

        public IEnumerable<string> Breakdown(string query)
        {
            return query.Split(separator);
        }
    }
}
