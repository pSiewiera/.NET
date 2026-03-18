using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab1;

namespace Lab1Tests
{
    [TestClass]
    public class Test1
    {
        [TestMethod]
        public void TestItemCount()
        {
            Problem p = new Problem(10, 1);
            Assert.AreEqual(10, p.Items.Count);
        }

        [TestMethod]
        public void TestIFItem()
        {
            Problem p = new Problem(5, 1);
            Result r = p.Solve(50);

            Assert.IsTrue(r.ItemIndexes.Count > 0);
        }

        [TestMethod]
        public void TestNoItemFits()
        {
            Problem p = new Problem(5, 1);
            Result r = p.Solve(0);

            Assert.AreEqual(0, r.ItemIndexes.Count);
        }

        [TestMethod]
        public void TestWeightLimit()
        {
            Problem p = new Problem(10, 2);
            Result r = p.Solve(20);

            Assert.IsTrue(r.TotalWeight <= 20);
        }

        [TestMethod]
        public void TestValues()
        {
            Problem p = new Problem(10, 3);

            foreach (var item in p.Items)
            {
                Assert.IsTrue(item.Value > 0);
                Assert.IsTrue(item.Weight > 0);
            }
        }
    }
}
