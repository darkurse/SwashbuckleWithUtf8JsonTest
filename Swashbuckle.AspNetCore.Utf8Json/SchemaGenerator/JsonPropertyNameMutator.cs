using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swashbuckle.AspNetCore.SwaggerGen
{
    /// <summary>
    /// This class is 100% coming from github repository neuecc/Utf8Json. Big thanks to neuecc
    /// ref: https://github.com/neuecc/Utf8Json/blob/master/src/Utf8Json/Internal/StringMutator.cs
    /// </summary>
    internal static class JsonPropertyNameMutator
    {
        /// <summary>
        /// MyProperty -> MyProperty
        /// </summary>
        public static string Original(string s)
        {
            return s;
        }

        /// <summary>
        /// MyProperty -> myProperty
        /// </summary>
        public static string ToCamelCase(string s)
        {
            if (string.IsNullOrEmpty(s) || char.IsLower(s, 0))
            {
                return s;
            }

            var array = s.ToCharArray();
            array[0] = char.ToLowerInvariant(array[0]);
            return new string(array);
        }

        /// <summary>
        /// MyProperty -> my_property
        /// </summary>
        public static string ToSnakeCase(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;

            var sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                var c = s[i];

                if (Char.IsUpper(c))
                {
                    // first
                    if (i == 0)
                    {
                        sb.Append(char.ToLowerInvariant(c));
                    }
                    else if (char.IsUpper(s[i - 1])) // WriteIO => write_io
                    {
                        sb.Append(char.ToLowerInvariant(c));
                    }
                    else
                    {
                        sb.Append("_");
                        sb.Append(char.ToLowerInvariant(c));
                    }
                }
                else
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }
    }
}
