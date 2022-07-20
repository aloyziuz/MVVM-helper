using MVVMHelper.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVMHelper.StringSearchStrategy
{
    /// <summary>
    /// class will search the corpus for the given query and return true if query is found anywhere in the corpus
    /// </summary>
    public class ContainsStringSearch : IStringSearchStrategy
    {
        public bool SearchString(string complete, string query, StringComparison comparison)
        {
            return complete.IndexOf(query, comparison) >= 0;
        }
    }
}
