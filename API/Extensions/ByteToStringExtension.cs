using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class ByteToStringExtension
    {
        public static string ConvertToString(this byte[] array)
        {
            string output = "";

            for (int i = 0; i < array.Length; i++)           
                output += (char)array[i];

            return output;
        }
    }
}
