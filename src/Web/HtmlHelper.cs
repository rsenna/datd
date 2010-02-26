using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using Mvc=System.Web.Mvc;
using System.Xml.Linq;

namespace Dataweb.Dilab.Web
{
    public static class HtmlHelper
    {
        public static string EncodeDate(this Mvc.HtmlHelper helper, DateTime? date)
        {
            var str = date.HasValue? date.Value.ToString(@"dd\/MM\/yyyy") : string.Empty;
            return helper.Encode(str);
        }

        public static string EncodeTime(this Mvc.HtmlHelper helper, DateTime? time)
        {
            var str = time.HasValue? time.Value.ToString("HH:mm") : string.Empty;
            return helper.Encode(str);
        }

        public static string EncodeCurrency(this Mvc.HtmlHelper helper, decimal? currency)
        {
            var str = currency.HasValue? currency.Value.ToString("'R$' #,##0.00", CultureInfo.GetCultureInfo("pt-BR")) : string.Empty;
            return helper.Encode(str);
        }

        public static string GetCssFilePath(this Mvc.HtmlHelper helper, string tenant)
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

        public static string Encode<T>(this Mvc.HtmlHelper helper, T value)
        {
            var type = typeof (T);
            var sValue = value.ToString();

            if (!type.IsEnum)
            {
                return helper.Encode(sValue);
            }

            var url = string.Format("~/App_GlobalResources/{0}.xml", type.Name);
            var path = HttpContext.Current.Request.MapPath(url);
            var xDoc = XDocument.Load(path);

            var q =
                from c in xDoc.Descendants("item")
                where (string) c.Attribute("value") == sValue
                select (string) c.Attribute("text");

            return helper.Encode(q.FirstOrDefault() ?? sValue);
        }
    }
}