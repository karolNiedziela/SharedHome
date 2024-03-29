﻿using System.Text;

namespace SharedHome.Shared.Extensionss
{
    public static class StringExtensions
    {
        public static string SplitByUpperCase(this string @string)
        {
            var stringBuilder = new StringBuilder();

            foreach (var character in @string)
            {
                if (char.IsUpper(character))
                {
                    if (stringBuilder.Length != 0)
                    {
                        stringBuilder.Append(' ');
                    }
                }

                stringBuilder.Append(character);
            }

            return stringBuilder.ToString()[..1] + stringBuilder.ToString()[1..].ToLower();
        }

        public static string FirstCharToUpper(this string @string)
            => @string[0].ToString().ToUpper() + @string[1..];
    }
}
