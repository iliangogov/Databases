using System.Collections.Generic;

namespace NorthWind.Data.Models
{
    public class Country
    {
        private ICollection<City> cities;

        public Country()
        {
            this.Cities = new HashSet<City>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<City> Cities
        {
            get { return this.cities; }
            set { this.cities = value; }
        }
    }
}