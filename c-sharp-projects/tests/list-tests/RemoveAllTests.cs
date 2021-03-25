using Microsoft.VisualStudio.TestTools.UnitTesting;
using VolatilePulse.Collection;

namespace list_tests
{
    [TestClass]
    public class RemoveAllTests
    {
        [TestMethod]
        public void NoRemoval()
        {
            var testList = Common.GenerateList();
            var cleanList = Common.GenerateList();
            var removed = testList.RemoveAll(v => false);

            Assert.AreEqual(0, removed);
            Assert.AreEqual(cleanList.Count, testList.Count);
            Assert.AreEqual(cleanList.Capacity, testList.Capacity);
        }

        [TestMethod]
        public void RemoveFirst()
        {
            var testList = Common.GenerateList();
            var cleanList = Common.GenerateList();
            var removed = testList.RemoveAll(v => v == 5);

            Assert.AreEqual(1, removed);
            Assert.AreEqual(cleanList.Count - 1, testList.Count);
            Assert.AreEqual(cleanList[1], testList[0]);
            Assert.AreEqual(cleanList.Capacity, testList.Capacity);
        }

        [TestMethod]
        public void RemoveLast()
        {
            var testList = Common.GenerateList();
            var cleanList = Common.GenerateList();
            var removed = testList.RemoveAll(v => v == 12);

            Assert.AreEqual(1, removed);
            Assert.AreEqual(cleanList.Count - 1, testList.Count);
            Assert.AreEqual(cleanList[cleanList.Count - 1 - 1], testList[testList.Count - 1]);
            Assert.AreEqual(cleanList.Capacity, testList.Capacity);
        }

        [TestMethod]
        public void RemoveAll()
        {
            var testList = Common.GenerateList();
            var cleanList = Common.GenerateList();
            var removed = testList.RemoveAll(v => true);

            Assert.AreEqual(cleanList.Count, removed);
            Assert.AreEqual(0, testList.Count);
            Assert.AreEqual(cleanList.Capacity, testList.Capacity);
        }

        [TestMethod]
        public void RemoveMultiple()
        {
            var testList = Common.GenerateList();
            var cleanList = Common.GenerateList();
            var expected = new[] { 5, 39, 15, 63, 9, 17 };

            var removed = testList.RemoveAll(v => (v % 2) == 0);

            Assert.AreEqual(cleanList.Count - expected.Length, removed);
            Assert.AreEqual(expected.Length, testList.Count);
            Assert.AreEqual(cleanList.Capacity, testList.Capacity);

            CollectionAssert.AreEqual(expected, testList.ToArray());
        }
    }
}
