using System;
using System.IO;
using System.Web;

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

        public static string GetCssFilePath(string tenant)
        {
            const string BASE_URL = "~/Content";
            const string FILE_NAME = "Site.css";

            var fileUrl = string.Format("{0}/{1}/{2}", BASE_URL, tenant, FILE_NAME);
            var filePath = HttpContext.Current.Request.MapPath(fileUrl);

            if (File.Exists(filePath))
            {
                return fileUrl;
            }

            fileUrl = string.Format("{0}/{1}", BASE_URL, FILE_NAME);
            return fileUrl;
        }
    }
}
