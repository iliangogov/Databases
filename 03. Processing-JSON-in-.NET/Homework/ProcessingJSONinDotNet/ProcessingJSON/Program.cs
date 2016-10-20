using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
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
            WebClient.DownloadFile(url,rssLocalFile);

            XmlDocument doc = new XmlDocument();
            doc.Load(rssLocalFile);
            string jsonText = JsonConvert.SerializeXmlNode(doc);
            var videoTitles = jsonText;//TODO
            Console.WriteLine(jsonText);
        }
    }
}
