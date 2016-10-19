using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace XMLinDotNetHW
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            ExtractArtistUsingDomParser();
            ExtractArtistUsingXPath();
            DeleteAlbumAbovePrice(40);
            ExtractSongsUsingXmlReader();
            ExtractSongsUsingXDocumentAndLINQ();
            CreateXMLfileFromTxtFile();
            ExtractFromCatalogToAlbums();
        }

        static void ExtractArtistUsingDomParser()
        {
            XmlDocument document = new XmlDocument();
            document.Load("../../Catalogue.xml");
            XmlNode rootNode = document.DocumentElement;
            Dictionary<string, int> ArtistAlbums = new Dictionary<string, int>();

            foreach (XmlNode album in rootNode.ChildNodes)
            {
                string artist = album["artist"].InnerText;

                if (!ArtistAlbums.ContainsKey(artist))
                {
                    ArtistAlbums.Add(artist, 1);
                }
                else
                {
                    ArtistAlbums[artist] += 1;
                }
            }

            foreach (KeyValuePair<string, int> pair in ArtistAlbums)
            {
                Console.WriteLine("Artist: {0}, has {1} {2}", pair.Key, pair.Value, pair.Value == 1 ? "album" : "albums");
            }

            Console.WriteLine("------------------------------");
        }

        static void ExtractArtistUsingXPath()
        {
            XmlDocument document = new XmlDocument();
            document.Load("../../Catalogue.xml");
            string xPathQuery = "/albums/album";
            XmlNodeList rootNode = document.SelectNodes(xPathQuery);
            Dictionary<string, int> ArtistAlbums = new Dictionary<string, int>();

            foreach (XmlNode album in rootNode)
            {
                string artistName = album.SelectSingleNode("artist").InnerText;

                if (!ArtistAlbums.ContainsKey(artistName))
                {
                    ArtistAlbums.Add(artistName, 1);
                }
                else
                {
                    ArtistAlbums[artistName] += 1;
                }
            }

            foreach (KeyValuePair<string, int> pair in ArtistAlbums)
            {
                Console.WriteLine("Artist: {0}, has {1} {2}", pair.Key, pair.Value, pair.Value == 1 ? "album" : "albums");
            }

            Console.WriteLine("------------------------------");
        }

        static void DeleteAlbumAbovePrice(decimal price)
        {
            XmlDocument document = new XmlDocument();
            document.Load("../../Catalogue.xml");
            XmlNode rootNode = document.DocumentElement;
            Dictionary<string, int> ArtistAlbums = new Dictionary<string, int>();

            foreach (XmlNode album in rootNode.ChildNodes)
            {
                string artist = album["artist"].InnerText;
                decimal albumPrice = decimal.Parse(album["price"].InnerText.TrimEnd('$'));

                if (albumPrice > price)
                {
                    rootNode.RemoveChild(album);
                    //if you physicaly want to change the xml file. It will affect the rest of the homework
                    // document.Save("../../Catalogue.xml");
                }
                else
                {
                    if (!ArtistAlbums.ContainsKey(artist))
                    {
                        ArtistAlbums.Add(artist, 1);
                    }
                    else
                    {
                        ArtistAlbums[artist] += 1;
                    }
                }
            }

            foreach (KeyValuePair<string, int> pair in ArtistAlbums)
            {
                Console.WriteLine("Artist: {0}, has {1} {2}", pair.Key, pair.Value, pair.Value == 1 ? "album" : "albums");
            }

            Console.WriteLine("------------------------------");
        }

        static void ExtractSongsUsingXmlReader()
        {
            using (XmlReader reader = XmlReader.Create("../../Catalogue.xml"))
            {
                Console.WriteLine("All songs in the albums:");

                while (reader.Read())
                {
                    if ((reader.NodeType == XmlNodeType.Element) &&
                        (reader.Name == "title"))
                    {
                        Console.WriteLine(reader.ReadElementString());
                    }
                }
            }

            Console.WriteLine("------------------------------");
        }

        static void ExtractSongsUsingXDocumentAndLINQ()
        {
            XDocument xmlDoc = XDocument.Load("../../Catalogue.xml");
            var albums =
                from album in xmlDoc.Descendants("album")
                select new
                {
                    Name = album.Element("name").Value,
                    Artist = album.Element("artist").Value,
                    songs =
                     from song in album.Descendants("songs").Elements("song")
                     select new Song
                     {
                         Title = (string)song.Element("title").Value,
                         Duration = (string)song.Element("duration").Value
                     }

                };

            Console.WriteLine("Found {0} albums:", albums.Count());
            foreach (var album in albums)
            {
                Console.WriteLine("Album name: {0} (by {1})\nSongs:", album.Name, album.Artist);
                foreach (var song in album.songs)
                {
                    Console.WriteLine(" ----- {0} ({1})", song.Title, song.Duration);
                }
            }

            Console.WriteLine("------------------------------");
        }

        static void CreateXMLfileFromTxtFile()
        {
            string targetUrl = "../../Person.txt";
            string targetText = File.ReadAllText(targetUrl, Encoding.UTF8);
            string[] personInfo = targetText.Split(',');
            string personName = personInfo[0];
            string personAddress = personInfo[1];
            string personPhone = personInfo[2];

            string fileName = "../../Person.xml";
            Encoding encoding = Encoding.GetEncoding("windows-1251");
            using (XmlTextWriter writer = new XmlTextWriter(fileName, encoding))
            {
                writer.Formatting = Formatting.Indented;
                writer.IndentChar = '\t';
                writer.Indentation = 1;

                writer.WriteStartDocument();
                writer.WriteStartElement("persons");
                writer.WriteStartElement("person");
                writer.WriteElementString("name", personName);
                writer.WriteElementString("address", personAddress);
                writer.WriteElementString("phone", personPhone);
                writer.WriteEndDocument();

                //you need to chek up solution folder for the new file!
            }

            Console.WriteLine("Document {0} created.", fileName);
            Console.WriteLine("------------------------------");
        }

        static void ExtractFromCatalogToAlbums()
        {
            Dictionary<string, string> AlbumsArtists = new Dictionary<string, string>();

            using (XmlReader reader = XmlReader.Create("../../Catalogue.xml"))
            {
                while (reader.Read())
                {
                    if ((reader.NodeType == XmlNodeType.Element) &&
                        (reader.Name == "album"))
                    {
                        string album = "";
                        string artist = "";

                        while (reader.Read())
                        {
                            if (reader.Name == "name")
                            {
                                album = reader.ReadElementString();
                            }

                            if ((reader.NodeType == XmlNodeType.Element) &&
                                (reader.Name == "artist"))
                            {
                                artist = reader.ReadElementString();
                                AlbumsArtists.Add(album, artist);
                            }
                        }
                    }
                }
            }

            string fileName = "../../Album.xml";
            Encoding encoding = Encoding.GetEncoding("windows-1251");

            using (XmlTextWriter writer = new XmlTextWriter(fileName, encoding))
            {
                writer.Formatting = Formatting.Indented;
                writer.IndentChar = '\t';
                writer.Indentation = 1;

                writer.WriteStartDocument();
                writer.WriteStartElement("albums");
                foreach (KeyValuePair<string, string> pair in AlbumsArtists)
                {
                    writer.WriteStartElement("album");
                    writer.WriteElementString("name", pair.Key);
                    writer.WriteElementString("author", pair.Value);
                    writer.WriteEndElement();
                    Console.WriteLine("Album: {0}, Author: {1}", pair.Key, pair.Value);
                }

                writer.WriteEndDocument();
            }

            Console.WriteLine("------------------------------");
        }

        static void ExtractFromDirectoryFilesTreeToXML(string targetDirectory)
        {
            
        }
    }
}

