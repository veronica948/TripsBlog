using CountryService.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CountryService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public List<Country> getAll()
        {
            return db.Countries.ToList();
        }

        public Country get(int id)
        {
            return db.Countries.Find(id);
        }

        public void create(Country country)
        {
            db.Countries.Add(country);
            db.SaveChanges();
        }

        public void update(Country country)
        {
            db.Entry(country).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void delete(int id) { 
        //HttpStatusCode.BadRequest
            Country country = db.Countries.Find(id);
            db.Countries.Remove(country);
            db.SaveChanges();
        }

        
    }
}
