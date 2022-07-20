using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVVMHelper.StringSearchStrategy;

namespace MVVMHelperTest
{
    [TestClass]
    public class StringSearchUnitTest
    {
        [TestMethod]
        public void ExactStringSearchTest()
        {
            var s = new ExactStringSearch();
            Assert.IsFalse(s.SearchString("1234", "asdf", StringComparison.OrdinalIgnoreCase));
            Assert.IsFalse(s.SearchString("1234", "123", StringComparison.OrdinalIgnoreCase));
            Assert.IsFalse(s.SearchString("1234", "234", StringComparison.OrdinalIgnoreCase));
            Assert.IsTrue(s.SearchString("1234", "1234", StringComparison.OrdinalIgnoreCase));
            Assert.IsTrue(s.SearchString("asdf", "asdf", StringComparison.OrdinalIgnoreCase));
            Assert.IsTrue(s.SearchString("asdf", "ASDF", StringComparison.OrdinalIgnoreCase));
            Assert.IsFalse(s.SearchString("asdf", "ASDF", StringComparison.Ordinal));
        }

        [TestMethod]
        public void ContainsStringSearch()
        {
            var s = new ContainsStringSearch();
            Assert.IsFalse(s.SearchString("asdf", "1234", StringComparison.OrdinalIgnoreCase));
            Assert.IsTrue(s.SearchString("asdf", "asdf", StringComparison.OrdinalIgnoreCase));
            Assert.IsTrue(s.SearchString("asdf", "as", StringComparison.OrdinalIgnoreCase));
            Assert.IsTrue(s.SearchString("asdf", "df", StringComparison.OrdinalIgnoreCase));
            Assert.IsTrue(s.SearchString("asdf", "asdf", StringComparison.Ordinal));
            Assert.IsTrue(s.SearchString("asdf", "as", StringComparison.Ordinal));
            Assert.IsTrue(s.SearchString("asdf", "df", StringComparison.Ordinal));
            Assert.IsFalse(s.SearchString("asdf", "ASDF", StringComparison.Ordinal));
            Assert.IsFalse(s.SearchString("asdf", "AS", StringComparison.Ordinal));
            Assert.IsFalse(s.SearchString("asdf", "DF", StringComparison.Ordinal));
        }

        [TestMethod]
        public void StartsWithStringSearch()
        {
            var s = new StartsWithStringSearch();
            Assert.IsFalse(s.SearchString("qwer", "wer", StringComparison.OrdinalIgnoreCase));
            Assert.IsTrue(s.SearchString("qwer", "qwer", StringComparison.OrdinalIgnoreCase));
            Assert.IsTrue(s.SearchString("qwer", "QWER", StringComparison.OrdinalIgnoreCase));
            Assert.IsTrue(s.SearchString("qwer", "Q", StringComparison.OrdinalIgnoreCase));
            Assert.IsFalse(s.SearchString("qwer", "wer", StringComparison.Ordinal));
            Assert.IsTrue(s.SearchString("qwer", "qwer", StringComparison.Ordinal));
            Assert.IsFalse(s.SearchString("qwer", "QWER", StringComparison.Ordinal));
            Assert.IsFalse(s.SearchString("qwer", "Q", StringComparison.Ordinal));
            Assert.IsTrue(s.SearchString("qwer", "q", StringComparison.Ordinal));
            Assert.IsTrue(s.SearchString("qwer", "qw", StringComparison.Ordinal));
            Assert.IsFalse(s.SearchString("qwer", "qwr", StringComparison.Ordinal));
        }
    }
}
