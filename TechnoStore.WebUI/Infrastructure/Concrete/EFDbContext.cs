using System;
using System.Collections.Generic;
using System.Data.Entity;
using TechnoStore.WebUI.Models.Entities;

namespace TechnoStore.WebUI.Infrastructure.Concrete
{
    public class EFDbContext:DbContext
    {
        public EFDbContext():base("EFDbContext")
        {

        }

        public DbSet<Technic> Technics { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
