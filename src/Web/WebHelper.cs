using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;

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

        public static string ToString<T>(T value)
        {
            var type = typeof (T);
            var sValue = value.ToString();

            if (!type.IsEnum)
            {
                return sValue;
            }

            var url = string.Format("~/App_GlobalResources/{0}.xml", type.Name);
            var path = HttpContext.Current.Request.MapPath(url);
            var xDoc = XDocument.Load(path);

            var q =
                from c in xDoc.Descendants("item")
                where (string) c.Attribute("value") == sValue
                select (string) c.Attribute("text");

            return q.FirstOrDefault() ?? sValue;
        }
    }
}