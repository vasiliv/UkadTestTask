using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public static int GetResponseTime(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            Stopwatch timer = new Stopwatch();

            timer.Start();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            response.Close();

            timer.Stop();

            return timer.Elapsed.Milliseconds;            
        }
    }
}
