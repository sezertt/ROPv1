using ROP.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDeneme
{
    class Program
    {
        static void Main(string[] args)
        {
            localhost.SiparisClient fr = new localhost.SiparisClient();
            entSiparis ds = fr.GetSiparisBilgileri();
        }
    }
}
