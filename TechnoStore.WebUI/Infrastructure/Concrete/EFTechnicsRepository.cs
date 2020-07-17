using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TechnoStore.WebUI.Infrastructure.Abstract;
using TechnoStore.WebUI.Models.Entities;

namespace TechnoStore.WebUI.Infrastructure.Concrete
{
    //хранилище данных
    public class EFTechnicsRepository : ITechnicsRepository
    {
        public EFDbContext context = new EFDbContext();

        public DbSet<Technic> Technics { get { return context.Technics; } set { } }
        public DbSet<Category> Categories { get { return context.Categories; } set { } }

        public DbSet<User> Users { get { return context.Users; } set { } }
        public DbSet<Role> Roles { get { return context.Roles; } set { } }

        public Technic DeleteTechnics(int technicsId)
        {
            var technics = this.context.Technics.Find(technicsId);
            if (technics != null)
            {
                this.context.Technics.Remove(technics);
                this.context.SaveChanges();
            }
            return technics;
        }

        public void SaveTechnics(Technic technics)
        {
            //if product was just created,then add to database
            //else-->update current product in database
            if (technics.Id == 0)
            {
                this.context.Technics.Add(technics);
                this.context.SaveChanges();
                //добавляем в категорию новый товар
                //this.context.Categories.First(c => c.Id == technics.CategoryId).Technics.Add(technics);
            }
            else
            {
                var oldTechnics = this.context.Technics.First(c=>c.Id==technics.Id);
                
                if (oldTechnics != null)
                {
                    oldTechnics.Name = technics.Name;
                    oldTechnics.Description = technics.Description;
                    oldTechnics.Price = technics.Price;
                    oldTechnics.CategoryId = technics.CategoryId;
                   // oldTechnics.Category = technics.Category;

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
