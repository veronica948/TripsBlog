using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RestService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        Dictionary<int, Person> db = new Dictionary<int, Person>();
        public Service1()
        {
            db.Add(1, new Person() { Id = 1, Name = "Tom" });
            db.Add(2, new Person() { Id = 2, Name = "Mark" });

        }
        public List<Person> getAll(int id) {
            return null;//db.All();
        }
        public Person get(int id) {
            return null;
        }

        public void create(Person person)
        {
            if (person != null)
            {
                db.Add(person.Id, person);
            }
        }

        public void update(Person person)
        {
          
        }

        public void delete(int id)
        {

        }
    }
}
