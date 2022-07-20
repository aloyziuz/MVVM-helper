using MVVMHelper.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVMHelper.StringSearchStrategy
{
    /// <summary>
    /// class will search for a query in the corpus and return true if the corpus starts with the query
    /// </summary>
    public class StartsWithStringSearch : IStringSearchStrategy
    {
        public bool SearchString(string complete, string query, StringComparison stringComparison)
        {
            return complete.StartsWith(query, stringComparison);
        }
    }
}
