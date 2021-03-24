using System;

namespace VolatilePulse.String
{
    public static class Class1
    {
        public static string Duplicate(this string @this, int n)
        {
            string output = "";

            for (int i = 0; i < n; i++)
            {
                output += @this;
            }

            return output;
        }
    }
}
