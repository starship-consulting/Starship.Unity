using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Assets.Scripts.Extensions {

    public static class StringExtensions {

        public static bool IsAlphabetical(this char text) {
            return IsAlphabetical(text.ToString());
        }

        public static bool HasNumbers(this string text) {
            return text.Any(char.IsDigit);
        }

        public static bool IsAlphabetical(this string text) {
            return Regex.IsMatch(text, @"^[a-zA-Z]+$");
        }

        public static bool IsVowel(this string input) {
            if (input.Length != 1) {
                return false;
            }

            const string vowels = "aeiou";
            return vowels.IndexOf(input) >= 0;
        }

        public static bool IsVowel(this char input) {
            const string vowels = "aeiouAEIOU";
            return vowels.IndexOf(input) >= 0;
        }

        public static bool IsNumeric(this char input) {
            return input.ToString().IsNumeric();
        }

        public static bool IsNumeric(this string input) {
            double retNum;

            return double.TryParse(input, NumberStyles.Any, NumberFormatInfo.InvariantInfo, out retNum);
        }

        public static IEnumerable<string> ReadLines(this string input) {
            using (var reader = new StringReader(input)) {
                var line = reader.ReadLine();

                while (!line.IsEmpty()) {
                    yield return line;
                    line = reader.ReadLine();
                }
            }
        }

        public static string Parse(this string input, string startPattern, string endPattern) {
            var start = input.IndexOf(startPattern);
            var offset = startPattern.Length + start;
            var end = input.IndexOf(endPattern, offset);
            return input.Substring(offset, end - offset);
        }

        public static string Tokenize(this string input, string leftPattern, string rightPattern, Func<string, string> tokenFunction) {
            string pattern = leftPattern + @"(.*?)" + rightPattern;
            var matches = Regex.Matches(input, pattern);

            foreach (Match match in matches) {
                var matchValue = match.Value;
                var group = match.Groups[1].Value;

                input = input.Replace(matchValue, tokenFunction(group));
            }

            return input;
        }

        public static string FormatWithObject(this string input, object instance) {
            return input.Tokenize("{", "}", token => {
                var context = instance;

                // Todo:  Reflection cache
                foreach (var member in token.Split('.')) {
                    var type = context.GetType();

                    if (member.EndsWith("()")) {
                        context = type.GetMethod(member.Replace("()", "")).Invoke(context, null);
                    }
                    else if (member.EndsWith("]")) {
                        var split = member.Split('[');
                        var name = split[0];
                        var index = int.Parse(split[1].Split(']')[0]);
                        var property = type.GetProperty(name);

                        context = property.GetValue(context, new object[] {index});
                    }
                    else {
                        context = type.GetProperty(member).GetValue(context, null);
                    }
                }

                return context.ToString();
            });
        }

        public static string ReplaceNonNumbers(this string value, string replaceWith = "") {
            var digitsOnly = new Regex(@"[^\d]");
            return digitsOnly.Replace(value, replaceWith);
        }

        public static T ToEnum<T>(this string value) {
            return (T) Enum.Parse(typeof(T), value);
        }

        public static byte[] ToBytes(this string input) {
            var encoding = new UTF8Encoding(false);
            return encoding.GetBytes(input);
        }

        public static String Right(this string input, int characters) {
            return input.Substring(input.Length - characters);
        }

        public static String Left(this string input, int characters) {
            return input.Substring(0, characters);
        }

        public static bool IsEmpty(this string input) {
            return String.IsNullOrEmpty(input);
        }

        public static string Capitalize(this string input) {
            string temp = input.Substring(0, 1);
            return temp.ToUpper() + input.Remove(0, 1);
        }

        public static String CamelCase(this string input) {
            if (String.IsNullOrEmpty(input)) {
                return string.Empty;
            }

            if (input.ToUpper() == input) {
                return input.ToLower();
            }

            return Char.ToLowerInvariant(input[0]) + input.Substring(1);
        }

        public static string SplitCamelCase(this string input) {
            return Regex.Replace(Regex.Replace(input, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
        }
    }
}