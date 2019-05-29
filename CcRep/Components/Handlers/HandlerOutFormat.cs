using System.Globalization;

namespace CcRep.Components.Handlers
{
    public static class HandlerOutFormat
    {
        readonly static CultureInfo cult_info = CultureInfo.GetCultureInfo("en-US");
        public readonly static string[] FORMAT_DATES = new string[] {@"dd.MM.yyyy",@"dd.MM.yyyy", @"dd/MM/yyyy" };

        public static System.String ToStr(this decimal? val, System.String format = "0.00")
        {
            return val.HasValue ? val.Value.ToString(format, cult_info) : string.Empty;
        }

        public static System.String ToStr(this System.DateTime? date_time, int num_format = 0)
        {
            return date_time.HasValue ? date_time.Value.ToString(FORMAT_DATES[num_format], cult_info) : string.Empty;
        }
    }
}