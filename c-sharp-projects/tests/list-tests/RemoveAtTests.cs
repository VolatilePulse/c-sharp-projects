using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using VolatilePulse.Collection;

namespace list_tests
{
    [TestClass]
    public class RemoveAtTests
    {
        [TestMethod]
        public void IndexLessThanZero()
        {
            var testList = Common.GenerateList();
            var cleanList = Common.GenerateList();

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => testList.RemoveAt(-1));

            Assert.AreEqual(cleanList.Count, testList.Count);
            Assert.AreEqual(cleanList.Capacity, testList.Capacity);
        }

        [TestMethod]
        public void IndexHigherThanRange()
        {
            var testList = Common.GenerateList();
            var cleanList = Common.GenerateList();

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => testList.RemoveAt(testList.Count));

            Assert.AreEqual(cleanList.Count, testList.Count);
            Assert.AreEqual(cleanList.Capacity, testList.Capacity);
        }

        [TestMethod]
        public void RemoveFirstItem()
        {
            var testList = Common.GenerateList();
            var cleanList = Common.GenerateList();

            testList.RemoveAt(0);

            Assert.AreEqual(cleanList.Count - 1, testList.Count);
            Assert.AreEqual(cleanList[1], testList[0]);
            Assert.AreEqual(cleanList.Capacity, testList.Capacity);
        }

        [TestMethod]
        public void RemoveLastItem()
        {
            var testList = Common.GenerateList();
            var cleanList = Common.GenerateList();

            testList.RemoveAt(testList.Count - 1);

            Assert.AreEqual(cleanList.Count - 1, testList.Count);
            Assert.AreEqual(cleanList[cleanList.Count - 1 - 1], testList[testList.Count - 1]);
            Assert.AreEqual(cleanList.Capacity, testList.Capacity);
        }
    }
}
