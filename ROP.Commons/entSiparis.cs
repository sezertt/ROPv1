using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ROP.Commons
{
    public class entSiparis
    {
        [DataMember]
        public int siparisId
        {
            get;
            set;
        }
        [DataMember]
        public string siparis
        {
            get;
            set;
        }
        [DataMember]
        public string departman
        {
            get;
            set;
        }
        [DataMember]
        public string garson
        {
            get;
            set;
        }
        [DataMember]
        public int masa
        {
            get;
            set;
        }
    }
}
