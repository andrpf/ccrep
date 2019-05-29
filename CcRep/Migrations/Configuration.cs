namespace CcRep.Migrations
{
    using CcRep.Models.dic;
    using System;
    using System.Data.Entity;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.IO;
    using CcRep.Models._db.csv;

    internal sealed class Configuration : DbMigrationsConfiguration<CcRep.Models._dc.CcRepContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CcRep.Models._dc.CcRepContext context)
        {
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
