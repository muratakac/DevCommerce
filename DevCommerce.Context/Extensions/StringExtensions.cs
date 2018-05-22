using System;
using System.Collections.Generic;
using System.Text;

namespace DevCommerce.Core.Extensions
{
    public static class StringExtensions
    {
        public static string GetPluralize(this string value)
        {
            //Kuralların üzerindne geçilecek. Geçici olarak bu şekilde bırakıldı.
            //http://www.grammar.cl/Notes/Plural_Nouns.htm
            string newValue = string.Empty;

            if (value.EndsWith("y"))
                newValue = value.TrimEnd('y').Insert(value.Length - 1, "ies");
            else if (value.EndsWith("s"))
                newValue = value.TrimEnd('s').Insert(value.Length - 1, "es");
            else if (value.EndsWith("ch"))
                newValue = value.TrimEnd('h').Trim('c').Insert(value.Length - 1, "es");
            else if (value.EndsWith("sh"))
                newValue = value.TrimEnd('h').TrimEnd('s').Insert(value.Length - 1, "es");
            else if (value.EndsWith("x"))
                newValue = value.TrimEnd('x').Insert(value.Length - 1, "es");
            else if (value.EndsWith("z"))
                newValue = value.TrimEnd('z').Insert(value.Length - 1, "es");
            else
                newValue = value.Insert(value.Length, "s");

            return newValue;
        }
    }
}
