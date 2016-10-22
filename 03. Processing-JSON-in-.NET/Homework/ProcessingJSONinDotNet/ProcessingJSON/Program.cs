using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Xml;

namespace ProcessingJSON
{
    class Program
    {
        static void Main(string[] args)
        {
            var rssLocalFile = "../../RssFeed.xml";
            var url = "https://www.youtube.com/feeds/videos.xml?channel_id=UCLC-vbm7OWvpbqzXaoAMGGw";
            var WebClient = new WebClient();
            WebClient.DownloadFile(url, rssLocalFile);

            XmlDocument doc = new XmlDocument();
            doc.Load(rssLocalFile);

            string jsonText = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented);
            var jsonObj = JObject.Parse(jsonText);

            var titles =
                     jsonObj.Descendants()
                     .OfType<JProperty>()
                     .Where(t => t.Name == "title")
                     .Values();


            foreach (var t in titles)
            {
                Console.WriteLine(t);
            }
        }
    }
}
