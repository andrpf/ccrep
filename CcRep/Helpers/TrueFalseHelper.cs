using System.Web.Mvc;

namespace CcRep.Helpers
{
    public static class TrueFalseHelper
    {
        public static MvcHtmlString TrueFalseValue(this HtmlHelper html, bool? val, string nullValue = null)
        {

            string resultString = string.IsNullOrEmpty(nullValue) ? "" : nullValue;
            if (val.HasValue && val.Value)
            {
                resultString = "Да";

            }
            else if (val.HasValue && !val.Value)
            {
                resultString = "Нет";
            }

            return new MvcHtmlString(resultString);
        }
    }
}