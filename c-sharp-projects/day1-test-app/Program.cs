using System;
using VolatilePulse.Collection;
using VolatilePulse.String;

namespace VolatilePulse.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var myList = new List<int>();
            //Console.WriteLine(myList.Capacity);
            int[] enumer = { 5, 10, 5, 2, 8, 8, 20, 11, 50, 36, 5, 8, 12 };
            var myList2 = new List<int>(enumer);
            myList.Add(15);
            myList.Add(25);
            myList.Add(1);
            myList.Add(5);
            // Console.WriteLine(myList);
            myList2.RemoveAll(v => v > 10);
            // Console.WriteLine(myList.Capacity);

            for (int i = 0; i < myList2.Count; i++)
                Console.WriteLine(myList2[i]);

            // string myString = "Hello World!";
            // Console.WriteLine(myString.Duplicate(3));
        }
    }
}
