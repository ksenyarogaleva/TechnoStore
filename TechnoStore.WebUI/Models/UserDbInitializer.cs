using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TechnoStore.WebUI.Infrastructure.Concrete;
using TechnoStore.WebUI.Models.Entities;

namespace TechnoStore.WebUI.Models
{
    public class UserDbInitializer:CreateDatabaseIfNotExists<EFDbContext>
    {
        protected override void Seed(EFDbContext context)
        {
            var roles = new List<Role>()
            {
                new Role(){RoleId=1,Name="Admin"},
                new Role(){RoleId=2,Name="User"},
            };

            context.Roles.AddRange(roles);
            var user = new User() { UserId = 1, Email = "ksenyarogaleva87@gmail.com", Password = "12345670", Role = roles.First() };
            context.Users.Add(user);
        }
    }
}