using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UkadTestTask
{
    public static class Helper
    {
        public static bool urlExists(string url)
        {
            using (var client = new WebClient())
            {
                try
                {
                    using (client.OpenRead(url))
                    {
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }
        public static string ConvertUrlToSitemap(string url)
        {
            return $"{url}/sitemap.xml";
        }
    }
}
