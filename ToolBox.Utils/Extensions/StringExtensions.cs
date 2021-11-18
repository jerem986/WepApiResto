using System;

namespace ToolBox.Utils.Extensions
{
    public static class StringExtensions
    {
        public static string AddQueryParams<T>(this string baseUri, T parameters)
        {
            UriBuilder builder = new UriBuilder(baseUri);
            foreach (var p in typeof(T).GetProperties())
            {
                string value = p.GetValue(parameters)?.ToString();
                if (value != null)
                {
                    string toAppend = $"{p.Name.ToLowerCamelCase()}={value}";
                    if (builder.Query != null && builder.Query.Length > 1)
                        builder.Query = builder.Query.Substring(1) + "&" + toAppend;
                    else
                        builder.Query = toAppend;
                }
            }
            return baseUri + builder.Query;
        }

        public static string ToLowerCamelCase(this string v)
        {
            if (v != string.Empty)
                return Char.ToLowerInvariant(v[0]) + v.Substring(1);
            return string.Empty;
        }
    }
}
