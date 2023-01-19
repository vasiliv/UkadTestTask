using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

            HtmlWeb web = new HtmlWeb();
            HtmlDocument htmlDoc = web.Load("https://www.ambebi.ge/");

            //Use the SelectNodes method to find all the "a" elements in the document:
            HtmlNodeCollection htmlNodes = htmlDoc.DocumentNode.SelectNodes("//a");

            //Iterate through the nodes and check the "href" attribute of each node to see if it starts with "http"
            foreach (HtmlNode node in htmlNodes)
            {
                stopwatch.Start();
                if (node.Attributes["href"].Value.StartsWith("http"))
                {
                    stopwatch.Stop();
                    TimeSpan ts = stopwatch.Elapsed;
                    urlList.Add(new Url { UrlName = node.Attributes["href"].Value, ElapsedTime = ts.ToString() });
                }
            }
            Console.WriteLine($"Urls(html documents) found after crawling a website: {urlList.Count}");

            //Sitemap.xml file is placed at \UkadTestTask\UkadTestTask\bin\Debug\net5.0 folder
            XmlDocument xmlDoc = new XmlDocument();
            List<Url> xmlList = new List<Url>();
            xmlDoc.Load("Sitemap.xml");            
            XmlNodeList xmlNodes = xmlDoc.SelectNodes("/root/url");
            foreach (XmlNode node in xmlNodes)
            {
                stopwatch.Start();                
                xmlList.Add(new Url { UrlName = node.InnerText });
                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed;
                xmlList.Add(new Url { ElapsedTime = ts.ToString() });                                
            }
            Console.WriteLine($"Urls found in sitemap: {xmlList.Count}");

            //Concatinate url and xml Lists
            List<Url> totalList = urlList.Concat(xmlList).ToList();

            foreach (var item in totalList)
            {
                Console.WriteLine($"{item.UrlName} {item.ElapsedTime}");
            }
        }
    }
}
