﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace HelpService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetMessage(string name);

        [OperationContract]
        MagicData UpdateNumber(MagicData previousData);

    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class MagicData
    {
        [DataMember]
        public int Number { get; set; }
        [DataMember]
        public string Phrase { get; set; }
    }
}
