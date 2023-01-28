﻿using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using UkadTestTask;

List<Url> urlList = new List<Url>();
List<Url> xmlList = new List<Url>();

//Console.Write("Please enter url address without / at the end: ");
//string urlName = Console.ReadLine();

string urlName = "https://jwt.io";
//string urlName = "https://lenta.ru";

HtmlWeb web = new HtmlWeb();
if (Helper.UrlExists(urlName))
{
    HtmlDocument htmlDoc = web.Load(urlName);

    //Use the SelectNodes method to find all the "a" elements in the document:
    HtmlNodeCollection htmlNodes = htmlDoc.DocumentNode.SelectNodes("//a[@href]");

    foreach (HtmlNode node in htmlNodes)
    {
        if (node.OuterHtml.Contains("/"))
        {
            urlList.Add(new Url { UrlName = node.Attributes["href"].Value.TrimEnd('/'), ElapsedTime = Helper.GetResponseTime(urlName) });
        }        
    }
}
else
{
    Console.WriteLine("Url does not exist");
}

XmlDocument xmlDoc = new XmlDocument();

string sitemapUrl = Helper.ConvertUrlToSitemap(urlName);
if (Helper.UrlExists(sitemapUrl))
{
    xmlDoc.Load(sitemapUrl);

    foreach (XmlNode node in xmlDoc.DocumentElement)
    {
        //add urls located in <loc> node to xmlList
        xmlList.Add(new Url { UrlName = node.FirstChild.InnerXml, ElapsedTime = Helper.GetResponseTime(sitemapUrl) });
    }
}
else
{
    Console.WriteLine("Sitemap.xml does not exist");
}

Console.WriteLine($"Urls(html documents) found after crawling a website: {urlList.Count}");
Console.WriteLine($"Urls found in sitemap.xml: {xmlList.Count}");

var xmlListOnlyName = xmlList.Select(x => x.UrlName);
var urlListOnlyName = urlList.Select(x => x.UrlName);

Console.WriteLine();
Console.WriteLine("1. Merge ordered by timing");
var totalList = xmlList.Concat(urlList).ToList();
Helper.PrintOrderedByTime(totalList);

Console.WriteLine();
Console.WriteLine("2. Urls founded in sitemap.xml but not founded after crawling a web site:");
var exceptXml = xmlListOnlyName.Except(urlListOnlyName).ToList();
Helper.Print(exceptXml);

Console.WriteLine();
Console.WriteLine("3. Urls founded by the crawling the website but not in sitemap.xml");
var exceptUrl = urlListOnlyName.Except(xmlListOnlyName).ToList();
Helper.Print(exceptUrl);

Console.ReadLine();