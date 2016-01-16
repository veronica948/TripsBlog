﻿using CountryService.Models;
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
    [ServiceContract(Namespace = "")]
    public interface IService1
    {

        
        [WebInvoke(Method = "GET", UriTemplate = "countries", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        List<Country> GetAll();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "countries/{id}", ResponseFormat = WebMessageFormat.Json)]
        Country Get(string id);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "countries", RequestFormat = WebMessageFormat.Json)]
        void Create(Country country);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "countries")]
        void Update(Country country);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/countries/{id}")]
        void Delete(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/countries/name/{start}", ResponseFormat = WebMessageFormat.Json)]
        List<Country> GetByName(string start);

    }
    /*
    [DataContract]
    public class Country
    {
       
        [DataMember]
        public int CountryId { get; set; }
      
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
    */

}
