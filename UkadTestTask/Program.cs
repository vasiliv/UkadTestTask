using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using UkadTestTask;

//Console.Write("Please enter url address without / at the end: ");
//string urlName = Console.ReadLine();
string urlName = "https://jwt.io";

Stopwatch stopwatch = new Stopwatch();
List<Url> urlList = new List<Url>();

HtmlWeb web = new HtmlWeb();
HtmlDocument htmlDoc = web.Load(urlName);

XmlDocument xmlDoc = new XmlDocument();
List<Url> xmlList = new List<Url>();

//if (Helper.urlExists(urlName))
//{

//}

string sitemapUrl = Helper.ConvertUrlToSitemap(urlName);
if (Helper.urlExists(sitemapUrl))
{
    xmlDoc.Load(sitemapUrl);

    foreach (XmlNode node in xmlDoc.DocumentElement)
    {
        //add urls located in <loc> node to xmlList
        xmlList.Add(new Url { UrlName = node.FirstChild.InnerXml });
    }
}


//Use the SelectNodes method to find all the "a" elements in the document:
HtmlNodeCollection htmlNodes = htmlDoc.DocumentNode.SelectNodes("//a[@href]");

foreach (HtmlNode node in htmlNodes)
{
    stopwatch.Start();
    if (node.OuterHtml.Contains("/"))
    {
        stopwatch.Stop();
        urlList.Add(new Url { UrlName = node.Attributes["href"].Value.TrimEnd('/'), ElapsedTime = stopwatch.ElapsedMilliseconds });
    }
}
Console.WriteLine($"Urls(html documents) found after crawling a website: {urlList.Count}");

var xmlListOnlyName = xmlList.Select(x => x.UrlName);
var urlListOnlyName = urlList.Select(x => x.UrlName);

Console.WriteLine();
Console.WriteLine("1. Merge ordered by timing");
var totalList = xmlList.Concat(urlList).OrderBy(i => i.ElapsedTime);



Console.ReadLine();