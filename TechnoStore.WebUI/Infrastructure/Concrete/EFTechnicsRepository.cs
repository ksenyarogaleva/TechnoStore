using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TechnoStore.WebUI.Infrastructure.Abstract;
using TechnoStore.WebUI.Models.Entities;

namespace TechnoStore.WebUI.Infrastructure.Concrete
{

    public class EFTechnicsRepository : ITechnicsRepository
    {
        private EFDbContext context;

        public IEnumerable<Technic> Technics { get { return context.Technics; } }
        public IEnumerable<Category> Categories { get { return context.Categories; } }
        public IEnumerable<User> Users { get { return context.Users; } }
        public IEnumerable<Role> Roles { get { return context.Roles; } }
        public IEnumerable<Log> Logs { get { return context.Logs; } }
        public IEnumerable<RequestStatistic> RequestStatistics { get { return context.RequestStatistics; } }

        public EFTechnicsRepository(EFDbContext dbContext)
        {
            this.context = dbContext;
        }
        
        public void SaveTechnics(Technic technics)
        {
            //if product was just created,then add to database
            //else-->update current product in database
            if (technics.Id == 0)
            {
                this.context.Technics.Add(technics);
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
                }
            }

            this.context.SaveChanges();

        }

        public void SaveUser(User user)
        {
            this.context.Users.Add(user);
            this.context.SaveChanges();
        }

        public void SaveRequest(RequestStatistic request)
        {
            if (request.Id == 0)
            {
                this.context.RequestStatistics.Add(request);
            }
            else
            {
                var oldRequestStatistic = this.RequestStatistics.First(r => r.Id == request.Id);
                if (oldRequestStatistic != null)
                {
                    oldRequestStatistic.Amount = request.Amount;
                }
            }

            this.context.SaveChanges();
        }

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

        public Log DeleteError(int errorId)
        {
            var errorLog = this.context.Logs.Find(errorId);
            if (errorLog != null)
            {
                this.context.Logs.Remove(errorLog);
                this.context.SaveChanges();
            }

            return errorLog;
        }

        public void DeketeAllErrors()
        {
            var deletedErrors = this.context.Logs.ToList();
            if (this.context.Logs != null)
            {
                this.context.Logs.RemoveRange(deletedErrors);
                this.context.SaveChanges();
            }
        }
    }
}
