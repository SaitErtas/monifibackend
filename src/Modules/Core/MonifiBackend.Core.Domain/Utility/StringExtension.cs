using System.Text;

namespace MonifiBackend.Core.Domain.Utility;

public static class StringExtension
{
    public static string CapitalizeFirst(this string s)
    {
        bool IsNewSentense = true;
        var result = new StringBuilder(s.Length);
        for (int i = 0; i < s.Length; i++)
        {
            if (IsNewSentense && char.IsLetter(s[i]))
            {
                result.Append(char.ToUpper(s[i]));
                IsNewSentense = false;
            }
            else
                result.Append(s[i]);

            if (s[i] == '!' || s[i] == '?' || s[i] == '.')
            {
                IsNewSentense = true;
            }
        }

        return result.ToString();
    }
    public static string CapitalizeFirstAndHideText(this string s, string hideCharacter = "*")
    {
        if (s == null)
            return null;
        bool IsNewSentense = true;
        var result = new StringBuilder(s.Length);
        for (int i = 0; i < s.Length; i++)
        {
            if (IsNewSentense && char.IsLetter(s[i]))
            {
                result.Append(char.ToUpper(s[i]));
                IsNewSentense = false;
            }
            else
                result.Append(hideCharacter);

            if (s[i] == '!' || s[i] == '?' || s[i] == '.')
            {
                IsNewSentense = true;
            }
        }

        return result.ToString();
    }
    public static string AppendApiKey(this string value, string? apiKey)
    {
        return value.Replace("{apiKey}", apiKey);
    }
    public static string AppendValue(this string query, string? value)
    {
        return query.Replace("{value}", value);
    }
    public static string AppendValue(this string query, int? value)
    {
        return AppendValue(query, value.ToString());
    }
    public static string AddQuery(this string query, string key, string value)
    {
        return query.EndsWith('&') ? $"{query}{key}={value}" : $"{query}&{key}={value}";
    }
    public static string AddQuery(this string query, string parameter)
    {
        return query.EndsWith('&') ? $"{query}{parameter}" : $"{query}&{parameter}";
    }
    public static string AddAction(this string query, string? action)
    {
        return query.EndsWith('&') ? $"{query}action={action}" : $"{query}&action={action}";
    }
}
