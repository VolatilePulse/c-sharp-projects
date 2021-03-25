using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using VolatilePulse.Collection;

namespace list_tests
{
    [TestClass]
    public class Capacity
    {
        [TestMethod]
        public void SetToLessThanCount()
        {
            var testList = Common.GenerateList();
            var cleanList = Common.GenerateList();

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => testList.Capacity = 0);

            Assert.AreEqual(cleanList.Count, testList.Count);
            Assert.AreEqual(cleanList.Capacity, testList.Capacity);
        }

        [TestMethod]
        public void AdjustToMatchCount()
        {
            const int STARTING_CAPACITY = 10;
            var testList = new List<int>(STARTING_CAPACITY);

            Assert.AreEqual(STARTING_CAPACITY, testList.Capacity);

            testList.Capacity = 0;

            Assert.AreEqual(testList.Capacity, testList.Count);
            Assert.AreEqual(0, testList.Capacity);
        }

        [TestMethod]
        public void PreventAccessAfterReduction()
        {
            var testList = Common.GenerateList();

            Assert.IsNotNull(testList[0]);

            testList.RemoveAll(v => true);
            testList.Capacity = 0;

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => testList[0]);
            Assert.AreEqual(testList.Capacity, testList.Count);
            Assert.AreEqual(0, testList.Capacity);
        }

        [TestMethod]
        public void NewCapacityIsInaccessible()
        {
            var testList = new List<int>();

            testList.Capacity++;

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => testList[0]);
            Assert.AreEqual(0, testList.Count);
            Assert.AreEqual(1, testList.Capacity);
        }
    }
}
