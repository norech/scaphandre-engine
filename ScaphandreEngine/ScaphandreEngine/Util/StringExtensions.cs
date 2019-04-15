using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ScaphandreEngine.Util
{
    public static class StringExtension
    {
        public static string ToPascalCase(this string str)
        {
            return string.Join("", str.Split(' ', '-', '_')
                         .Select(w => w.Trim())
                         .Where(w => w.Length > 0)
                         .Select(w => w.Substring(0, 1).ToUpper() + w.Substring(1).ToLower()).ToArray());
        }

        public static string ToCamelCase(this string str)
        {
            var newStr = ToPascalCase(str);

            return newStr.Substring(0, 1).ToLower() + newStr.Substring(1);
        }

        public static string ToSnakeCase(this string str)
        {
            return Regex.Replace(str.ToLower().Replace(' ', '_').Replace('-', '_'), "[^A-Za-z_]", "");
        }

        public static string ToAlphaNumeric(this string str)
        {
            return Regex.Replace(str, "[^A-Za-z0-9]", "");
        }

        public static string ToAlphabetic(this string str)
        {
            return Regex.Replace(str, "[^A-Za-z]", "");
        }

        public static string ToNumeric(this string str)
        {
            return Regex.Replace(str, "[^0-9]", "");
        }
    }
}
