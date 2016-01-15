using CountryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CountryService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/country", ResponseFormat = WebMessageFormat.Json)]
        List<Country> getAll();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/country/{id}", ResponseFormat = WebMessageFormat.Json)]
        Country get(int id);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/country")]
        void create(Country person);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/country")]
        void update(Country person);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/country/{id}")]
        void delete(int id);
    }


}
