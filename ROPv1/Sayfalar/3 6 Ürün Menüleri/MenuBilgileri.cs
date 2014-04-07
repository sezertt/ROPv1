using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROPv1
{
    public class MenuBilgileri
    {
        public string menuAdi;
        public double menuFiyati;
        public List<MenuUrunuBilgisi> urun; 
        
        public MenuBilgileri()
        {
            urun = new List<MenuUrunuBilgisi>();
        }
    }
    
}
