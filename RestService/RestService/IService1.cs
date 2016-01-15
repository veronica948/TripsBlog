using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RestService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate="/person", ResponseFormat = WebMessageFormat.Json)]
        List<Person> getAll(int id);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/person/{id}", ResponseFormat = WebMessageFormat.Json)]
        Person get(int id);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/person")]
        void create(Person person);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/person")]
        void update(Person person);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/person/{id}")]
        void delete(int id);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Person
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
    }

    [DataContract]
    public class Country
    {
        [DataMember]
        [Required]
        public int CountryId { get; set; }

        [DataMember]
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [DataMember]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}
