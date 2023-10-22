using System.Text.RegularExpressions;

namespace microscore.application.utils
{
    public static class ObfuscatorUtils
    {
        public static string? IdentificationNullableObfuscator(this string? identification)
        {
            if (identification == null)
            {
                return null;
            }
            return Regex.Replace(identification, @"(?<!^.?).(?!.?$)", "X");
        }
        public static string IdentificationObfuscator(this string identification)
        {
            return Regex.Replace(identification, @"(?<!^.?).(?!.?$)", "X");
        }

        public static string? ToVarchar(this string? value, int len)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            value = value.Trim().ToUpper().PadRight(len);
            return value;
        }
    }
}
