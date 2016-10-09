using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        private static JsonRequester jsonRequester;
        private static bool ExitCommandEntered = false;

        static void Main(string[] args)
        {
            Run();
        }

        static void Run()
        {
            jsonRequester = new JsonRequester("http://localhost:3000");

            while (!ExitCommandEntered)
            {
                ShowMenu();
                var command = TakeCommand();
                Execute(command);
            }
        }

        private static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Items:");
            var items = GetItems();
            items.ForEach(Console.WriteLine);
            Console.WriteLine("Enter command:");
        }

        private static List<Item> GetItems()
        {
            var content = jsonRequester.Get("api/items");
            var itemsString = JObject.Parse(content)["result"];

            var items = JsonConvert.DeserializeObject<List<Item>>(itemsString.ToString());
            return items;
        }

        private static string TakeCommand()
        {
            var command = Console.ReadLine();
            return command;
        }

        private static void Execute(string command)
        {
            if(command== "@exit")
            {
                ExitCommandEntered = true;
                return;
            }
            Add(command);
        }

        private static void Add(string command)
        {
            var item = new Item
            {
                Name = command
            };
            var json = JsonConvert.SerializeObject(item);
            jsonRequester.Post("api/items", json);
        }
    }

    internal class Item
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public override string ToString()
        {
            return string.Format("{0} (id: {1})", this.Name, this.Id);
        }

    }
}
