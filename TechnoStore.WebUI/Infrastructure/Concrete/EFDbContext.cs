using System;
using System.Collections.Generic;
using System.Data.Entity;
using TechnoStore.WebUI.Models.Entities;

namespace TechnoStore.WebUI.Infrastructure.Concrete
{
    public class EFDbContext:DbContext
    {
        public DbSet<Technics> Technics { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
