namespace Cars.ConsoleClient
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Cars.ConsoleClient.Models;
    using Cars.Data;
    using Cars.Models;
    using Newtonsoft.Json;   

    public static class JsonImporter
    {
        public static void Import(CarsDbContext dbContext)
        {
            var jsonCars = new List<JsonCar>();

            var files = Directory.GetFiles("../../JsonFiles").Where(fileName => fileName.EndsWith(".json")).ToList();

            foreach (var file in files)
            {
                var fileContent = File.ReadAllText(file);
                var fileCars = JsonConvert.DeserializeObject<IEnumerable<JsonCar>>(fileContent);
                jsonCars.AddRange(fileCars);
                Console.WriteLine("{0} read.", file);
            }

            var manufacturerNames = new HashSet<string>();
            var dealersNames = new HashSet<string>();
            var cityNames = new HashSet<string>();

            foreach (var car in jsonCars)
            {
                if (!manufacturerNames.Contains(car.ManufacturerName))
                {
                    manufacturerNames.Add(car.ManufacturerName);
                    dbContext.Manufacturers.Add(new Manufacturer() { Name = car.ManufacturerName });
                    dbContext.SaveChanges();
                }

                string city = car.Dealer.City;
                if (!cityNames.Contains(city))
                {
                    cityNames.Add(city);
                    dbContext.Cities.Add(new City() { Name = city });
                    dbContext.SaveChanges();
                }

                if (!dealersNames.Contains(car.Dealer.Name))
                {
                    dealersNames.Add(car.Dealer.Name);
                    var cities = new List<City>();
                    cities.Add(dbContext.Cities.FirstOrDefault(c => c.Name == car.Dealer.City));
                    dbContext.Dealers.Add(new Dealer() { Name = car.Dealer.Name, Cities = cities });
                    dbContext.SaveChanges();
                }
                else
                {
                    if (!dbContext.Dealers.FirstOrDefault(d => d.Name == car.Dealer.Name).Cities.Any(c => c.Name == city))
                    {
                        dbContext.Dealers.FirstOrDefault(d => d.Name == car.Dealer.Name).Cities.Add(dbContext.Cities.FirstOrDefault(c => c.Name == car.Dealer.City));
                    }

                    dbContext.SaveChanges();
                }

                dbContext.Cars.Add(new Car()
                {
                    ManufacturerId = dbContext.Manufacturers.FirstOrDefault(m => m.Name == car.ManufacturerName).Id,
                    Model = car.Model,
                    Year = car.Year,
                    Price = car.Price,
                    TransmissionType = car.TransmissionType,
                    DealerId = dbContext.Dealers.FirstOrDefault(d => d.Name == car.Dealer.Name).Id
                });
            }

            dbContext.SaveChanges();
        }
    }
}
