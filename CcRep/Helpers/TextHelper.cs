using System.Net;
using System.Text;

public static class TextHelper
{
    public static string RemoveHtmlTags(this string html)
    {
        int pos = 0;
        StringBuilder builder = new StringBuilder();
        while (pos < html.Length)
        {
            if (html[pos] == '<')
            {
                pos = SkipTag(html, pos);
                builder.Append(' ');
            }
            else builder.Append(html[pos++]);
        }
        return WebUtility.HtmlDecode(builder.ToString());
    }

    private static int SkipTag(string html, int pos)
    {
        pos++;
        while (pos < html.Length)
        {
            if (html[pos] == '"' || html[pos] == '\'')
                pos = SkipString(html, pos);
            else if (html[pos++] == '>')
                break;
        }
        return pos;
    }

    private static int SkipString(string html, int pos)
    {
        char quote = html[pos++];
        while (pos < html.Length)
        {
            if (html[pos++] == quote)
                break;
        }
        return pos;
    }
}