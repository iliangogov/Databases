using NorthWind.Data;
using NorthWind.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCodeFirstLction
{
    public class Run
    {
        static void Main()
        {
            using (var dbContext = new NorthWindDbContext())
            {
                //var newCountry = new Country()
                //{
                //    Name = "Bulgaria"
                //};
                var countries = dbContext.Countries.FirstOrDefault(c => c.Id == 1);
                Console.WriteLine(countries.Name);

                
                //dbContext.SaveChanges();
            }
        }
    }
}
