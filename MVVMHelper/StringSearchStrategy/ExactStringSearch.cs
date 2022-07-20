using MVVMHelper.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVMHelper.StringSearchStrategy
{
    /// <summary>
    /// class will search for a query in the corpus and return true if the query is exact match for the corpus
    /// </summary>
    public class ExactStringSearch : IStringSearchStrategy
    {
        public bool SearchString(string complete, string query, StringComparison comparison)
        {
            return complete.Equals(query, comparison);
        }
    }
}
