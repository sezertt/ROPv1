using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROPv1
{
    public class UrunMenuBilgileri
    {
        public string menuAdi;
        public double menuFiyati;
        public List<UrunOzellikleri> urun; 
        
        public UrunMenuBilgileri()
        {
            urun = new List<UrunOzellikleri>();
        }
    }
    
}
