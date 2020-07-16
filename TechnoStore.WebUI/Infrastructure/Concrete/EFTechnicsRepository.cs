using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Runtime.InteropServices.WindowsRuntime;
using TechnoStore.WebUI.Infrastructure.Abstract;
using TechnoStore.WebUI.Models.Entities;

namespace TechnoStore.WebUI.Infrastructure.Concrete
{
    //хранилище данных
    public class EFTechnicsRepository : ITechnicsRepository
    {
        public EFDbContext context = new EFDbContext();

        public DbSet<Technics> Technics { get { return context.Technics; } set { } }
        public DbSet<Category> Categories { get { return context.Categories; } set { } }

        public DbSet<User> Users { get { return context.Users; } set { } }
        public DbSet<Role> Roles { get { return context.Roles; } set { } }

        public Technics DeleteTechnics(int technicsId)
        {
            var technics = this.context.Technics.Find(technicsId);
            if (technics != null)
            {
                this.context.Technics.Remove(technics);
                this.context.SaveChanges();
            }
            return technics;
        }

        public void SaveTechnics(Technics technics)
        {
            //if product was just created,then add to database
            //else-->update current product in database
            if (technics.TechnicsId == 0)
            {
                this.context.Technics.Add(technics);
            }
            else
            {
                var oldTechnics = this.context.Technics.Find(technics.TechnicsId);
                if (oldTechnics != null)
                {
                    oldTechnics.Name = technics.Name;
                    oldTechnics.Description = technics.Description;
                    oldTechnics.Price = technics.Price;
                    oldTechnics.CategoryId = technics.CategoryId;
                    oldTechnics.Category = technics.Category;
                }
            }

            this.context.SaveChanges();

        }

        public void SaveUser(User user)
        {
            this.context.Users.Add(user);
            this.context.SaveChanges();
        }
    }
}
