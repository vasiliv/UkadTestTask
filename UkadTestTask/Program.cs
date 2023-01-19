using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;

namespace UkadTestTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.Write("Please enter url address: ");
            //string urlName = Console.ReadLine();
            
            Stopwatch stopwatch = new Stopwatch();
            List<Url> urlList = new List<Url>();

            //Sitemap.xml file is placed at \UkadTestTask\UkadTestTask\bin\Debug\net5.0 folder
            XmlDocument doc = new XmlDocument();
            doc.Load("Sitemap.xml");            
            XmlNodeList nodes = doc.SelectNodes("/root/url");
            foreach (XmlNode node in nodes)
            {
                stopwatch.Start();                
                urlList.Add(new Url { UrlName = node.InnerText });
                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed;
                urlList.Add(new Url { ElapsedTime = ts.ToString() });                                
            }
            foreach (var item in urlList)
            {
                Console.WriteLine($"{item.UrlName} {item.ElapsedTime}");
            }
        }
    }
}
