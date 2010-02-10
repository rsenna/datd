using System;

namespace Dataweb.Dilab.Web
{
    public static class WebHelper
    {
        public static string FormatDate(DateTime? value)
        {
            return value.HasValue? FormatDate(value.Value) : string.Empty;
        }

        public static string FormatDate(DateTime value)
        {
            return value.ToString(@"dd\/MM\/yyyy");
        }

        public static string FormatTime(DateTime value)
        {
            return value.ToString("HH:mm");
        }
    }
}
