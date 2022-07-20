using MVVMHelper.Interface;
using MVVMHelper.StringSearchStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVVMHelper
{
    public class Searcher
    {
        private IStringSearchStrategy stringsearch;
        private StringComparison comparison;
        private IQueryBreakdownStrategy breakdown;

        /// <summary>
        /// default searcher configuration will search the query anywhere in the corpus. 
        /// query will be broken down to words first with space as separator.
        /// comparison is ordinal ignore case. 
        /// </summary>
        public Searcher()
        {
            stringsearch = new ContainsStringSearch();
            comparison = StringComparison.OrdinalIgnoreCase;
            breakdown = new SeparatorBreakdownStrategy(' ');
        }

        public Searcher(IStringSearchStrategy strat, IQueryBreakdownStrategy breakstrat, StringComparison comparison)
        {
            this.stringsearch = strat;
            this.comparison = comparison;
            this.breakdown = breakstrat;
        }

        /// <summary>
        /// filters a sequence based on a query string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">string to be searched</param>
        /// <param name="completeList">sequence of objects to be searched</param>
        /// <param name="selector">designates a property to search in</param>
        /// <returns> subset of the input sequence that contains the input query string in the designated property</returns>
        public IEnumerable<T> Filter<T>(string query, IEnumerable<T> completeList, Func<T, string> selector)
        {
            foreach(T obj in completeList)
            {
                if (StringSearch(query, obj, selector))
                    yield return obj;
            }
        }

        /// <summary>
        /// deterime whether the object contains the string or not in the given attribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="candidate"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public bool StringSearch<T>(string query, T candidate, Func<T, string> selector)
        {
            if (string.IsNullOrWhiteSpace(query))
                return true;

            var comps = breakdown.Breakdown(query);
            foreach (var queryComponent in comps)
            {
                if (!stringsearch.SearchString(selector(candidate), queryComponent, comparison))
                    return false;
            }
            return true;
        }
    }
}