using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROPv1
{
    [Serializable]
    public class KullaniciOzellikleri
    {
        public string kullaniciAdi;
        public string adi;
        public string soyadi;
        public string sifresi;
        public string unvani;
        public string pinKodu;
        public string[] yetkileri;

        public KullaniciOzellikleri()
        {
            yetkileri = new string[7];
        }
    }
}
