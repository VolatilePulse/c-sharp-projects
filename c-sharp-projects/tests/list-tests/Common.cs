using VolatilePulse.Collection;

namespace list_tests
{
    public class Common
    {
        public static List<int> GenerateList()
        {
            int[] numbers = { 5, 10, 39, 15, 63, 9, 26, 82, 17, 12 };
            var testList = new List<int>(numbers);

            return testList;
        }
    }
}
