using System;
using VolatilePulse.Collection;
using VolatilePulse.String;

namespace VolatilePulse.App
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] enumer = { 5, 10, 5, 2, 8, 8, 20, 11, 50, 36, 5, 8, 12 };
            var myList = new List<int>(enumer);

            myList.RemoveAll(v => v > 10);
            Console.WriteLine(myList);

            // string myString = "Hello World!";
            // Console.WriteLine(myString.Duplicate(3));
        }
    }
}
