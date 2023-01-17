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

            Dictionary <string,double> xmlDict = new Dictionary<string,double>();
            Stopwatch stopwatch = new Stopwatch();
            //Sitemap.xml file is placed at \UkadTestTask\UkadTestTask\bin\Debug\net5.0 folder
            XmlDocument doc = new XmlDocument();
            doc.Load("Sitemap.xml");
            //Console.WriteLine(doc.InnerXml);
            XmlNodeList nodes = doc.SelectNodes("/root/url");
            foreach (XmlNode node in nodes)
            {
                stopwatch.Start();
                xmlDict.Add(node.InnerText, stopwatch.ElapsedMilliseconds);
                stopwatch.Stop();
                Console.WriteLine($"{node.InnerText} {stopwatch.ElapsedMilliseconds}");                
            }            
        }
    }
}
