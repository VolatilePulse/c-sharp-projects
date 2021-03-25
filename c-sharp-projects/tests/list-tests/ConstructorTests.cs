using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using VolatilePulse.Collection;

namespace list_tests
{
    [TestClass]
    public class ConstructorTests
    {
        [TestMethod]
        public void DefaultConstructor()
        {
            var testList = new List<int>();

            Assert.AreEqual(0, testList.Capacity);
            Assert.AreEqual(0, testList.Count);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => testList[0]);
        }

        [TestMethod]
        public void SizeConstructor()
        {
            const int LIST_SIZE = 5;

            var testList = new List<int>(LIST_SIZE);

            Assert.AreEqual(LIST_SIZE, testList.Capacity);
            Assert.AreEqual(0, testList.Count);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => testList[0]);
        }

        [TestMethod]
        public void EnumerableConstructor()
        {
            int[] input = { 5, 10, 15 };

            var testList = new List<int>(input);

            Assert.AreEqual(input.Length, testList.Capacity);
            Assert.AreEqual(input.Length, testList.Count);
            CollectionAssert.AreEqual(input, testList.ToArray());
        }
    }
}
