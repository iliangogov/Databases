namespace SocialNetwork.ConsoleClient
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Searcher;
    using SocialNetwork.Data;
    using SocialNetwork.Data.Migrations;
    using System.Xml.Linq;
    using Models;
    using System.Xml;
    using System.Data.SqlClient;

    public class Startup
    {
        public static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SocialNetworkDbContext, Configuration>());

            var db = new SocialNetworkDbContext();
            db.Database.CreateIfNotExists();
            db.SaveChanges();
            db.Configuration.AutoDetectChangesEnabled = true;
            db.Configuration.ValidateOnSaveEnabled = true;

            const string connectionString = "Server=.;Database=SocialNetwork;Integrated Security=true";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            XDocument doc = XDocument.Load("../../XmlFiles/Friendships-Test.xml");
            var Items = doc.Descendants("Friendships")
                .Select(x => new
                {
                    Approved = (int?)x.Attribute("Approved"),
                    CATALOG_NUMBER = (string)x.Element("CATALOG_NUMBER"),
                }).ToList();

            foreach (var item in Items)
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "Insert INTO Friendships (NUMBER,CATALOG_NUMBER,ITEM_NAME,UNIT,AMOUNT,PRI,@cenabezvat,@vat1,@cenavat,@ean1);";
                    cmd.Parameters.AddWithValue("@cislo", item.NUMBER);
                    cmd.Parameters.AddWithValue("@katalo_cislo", item.CATALOG_NUMBER);
                    cmd.Parameters.AddWithValue("@nazev_zbozi", item.ITEM_NAME);
                    cmd.Parameters.AddWithValue("@jednotka", item.UNIT);
                    cmd.Parameters.AddWithValue("@mnozstvi", item.AMOUNT);
                    cmd.Parameters.AddWithValue("@cenabezvat", item.PRICE_WITHOUT_VAT);
                    cmd.Parameters.AddWithValue("@vat1", item.VAT);
                    cmd.Parameters.AddWithValue("@cenavat", item.PRICE_VAT);
                    cmd.Parameters.AddWithValue("@ean1", item.EAN);
                    cmd.Parameters.AddWithValue("@dodav_cislo", item.SUPPLIER_ITEM_NUMBER);
                    cmd.Parameters.AddWithValue("@dodav_id", item.SUPPLIER_ID);
                    cmd.Parameters.AddWithValue("@dodav_jmeno", item.SUPPLIER_NAME);
                    cmd.Parameters.AddWithValue("@objednavkaid", item.ORDER_ITEMS_Id);
                    cmd.ExecuteNonQuery();
                    //cmd.Clone();
                }
            }
        }
    }
}
