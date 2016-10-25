using NorthWind.Data.Migrations;
using NorthWind.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.Data
{
    public class NorthWindDbContext: DbContext
    {
        public NorthWindDbContext():base("NorthwindDb")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NorthWindDbContext, Configuration>());
        }

        public virtual DbSet<Teacher> Teachers { get; set; }
                
        public virtual DbSet<Country> Countries { get; set; }
                
        public virtual DbSet<City> Cities { get; set; }
    }
}
