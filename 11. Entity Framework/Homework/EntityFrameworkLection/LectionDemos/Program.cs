using LectionDemos.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectionDemos
{
    public class Program
    {
        static void Main()
        {
            var dbContext = new NORTHWINDEntities();
            var categories = dbContext.Categories;
            foreach (var c in categories)
            {
                Console.WriteLine(c.CategoryName);
            }


        }
    }
}