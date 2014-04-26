using ROP.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ROP.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISiparis" in both code and config file together.
    [ServiceContract]
    public interface ISiparis
    {
        [OperationContract]
        entSiparis GetSiparisBilgileri();
    }
}
