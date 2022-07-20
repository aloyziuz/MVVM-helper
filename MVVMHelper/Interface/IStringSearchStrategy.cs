using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVMHelper.Interface
{
    public  interface IStringSearchStrategy
    {
        /// <summary>
        /// search a corpus for a query. 
        /// </summary>
        /// <param name="complete"></param>
        /// <param name="query"></param>
        /// <param name="comparison"></param>
        /// <returns>returns true if corpus contains the query. false otherwise</returns>
        bool SearchString(string complete, string query, StringComparison comparison);
    }
}
