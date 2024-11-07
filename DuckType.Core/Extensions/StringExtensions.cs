using System.Text.RegularExpressions;

namespace DuckType.Core.Extensions
{
    public static class StringExtensions
    {
        public static string Interpolate(this string self, object interpolationContext)
        {
            var placeholders = Regex.Matches(self, @"\{(.*?)\}");
            foreach (Match placeholder in placeholders)
            {
                var placeholderValue = placeholder.Value;
                var placeholderPropertyName = placeholderValue.Replace("{", "").Replace("}", "");
                var value = interpolationContext.GetPropertyValue(placeholderPropertyName)?.ToString() ?? "";
                self = self.Replace(placeholderValue, value);
            }

            return self;
        }
    }
}