namespace TechnoStore.WebUI.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TechnoStore.WebUI.Models.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<TechnoStore.WebUI.Infrastructure.Concrete.EFDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "TechnoStore.WebUI.Infrastructure.Concrete.EFDbContext";
        }

        protected override void Seed(TechnoStore.WebUI.Infrastructure.Concrete.EFDbContext context)
        {
            

        }
    }
}
