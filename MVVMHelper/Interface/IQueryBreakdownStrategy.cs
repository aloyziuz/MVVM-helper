using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVMHelper.Interface
{
    public interface IQueryBreakdownStrategy
    {
        /// <summary>
        /// breaks down a string into parts
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> Breakdown(string query);
    }
}
