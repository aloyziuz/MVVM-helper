using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVVMHelper.StringSearchStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMHelperTest
{
    [TestClass]
    public class QueryBreakdownUnitTest
    {
        [TestMethod]
        public void NoQueryBreakdownStrategyTest()
        {
            var s = new NoQueryBreakdownStrategy();
            var res = s.Breakdown("asdf");
            CollectionAssert.AreEquivalent(new List<string> { "asdf" }, res.ToList());
            res = s.Breakdown("asdf qwer");
            CollectionAssert.AreEquivalent(new List<string> { "asdf qwer" }, res.ToList());
        }

        [TestMethod]
        public void SeparatorBreakdownStrategyTest()
        {
            var s = new SeparatorBreakdownStrategy(' ');
            var res = s.Breakdown("asdf");
            CollectionAssert.AreEquivalent(new List<string> { "asdf" }, res.ToList());
            res = s.Breakdown("asdf qwer");
            CollectionAssert.AreEquivalent(new List<string> { "asdf", "qwer" }, res.ToList());

            s = new SeparatorBreakdownStrategy(',');
            res = s.Breakdown("asdf");
            CollectionAssert.AreEquivalent(new List<string> { "asdf" }, res.ToList());
            res = s.Breakdown("asdf,qwer");
            CollectionAssert.AreEquivalent(new List<string> { "asdf", "qwer" }, res.ToList());
        }
    }
}
