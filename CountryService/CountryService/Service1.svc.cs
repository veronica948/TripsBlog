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
    //[ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    public class Service1 : IService1
    {
        private CountryService.Models.ApplicationDbContext db = new CountryService.Models.ApplicationDbContext();

        public List<Country> GetAll()
        {
            
            return db.Countries.ToList();
        }

        public Country Get(string id)
        {
            WebOperationContext ctx = WebOperationContext.Current;
            Country country = null;
            if (id == null)
            {
                ctx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return country;
            }
            country = db.Countries.Find(System.Convert.ToInt32(id));
            if (country == null)
            {
                ctx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
            }
            return country;
        }

        public void Create(Country country)
        {
            WebOperationContext ctx = WebOperationContext.Current;
            if (country == null)
            {
                ctx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return;
            }
            db.Countries.Add(country);
            db.SaveChanges();
            
            ctx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.Created;
        }

        public void Update(Country country)
        {
            WebOperationContext ctx = WebOperationContext.Current;
            if (country == null)
            {
                ctx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return;
            }
            db.Entry(country).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(string id)
        {
            WebOperationContext ctx = WebOperationContext.Current;
            if (id == null)
            {
                ctx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return;
            }
            Country country = db.Countries.Find(int.Parse(id));
            db.Countries.Remove(country);
            db.SaveChanges();
        }
        public List<string> GetByName(string start)
        {
            var countriesList = db.Countries.Where(u => u.Name.StartsWith(start)).ToList();
            List<string> nameList = new List<string>();
            foreach(var country in countriesList) {
                nameList.Add(country.Name);
            }
            return nameList;
        }
        
    }
}
