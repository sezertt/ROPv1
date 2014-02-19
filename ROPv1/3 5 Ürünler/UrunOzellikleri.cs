using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROPv1
{
    [Serializable]
    public class UrunOzellikleri
    {
        public List<string> urunAdi;
        public List<string> porsiyonFiyati;
        public List<string> urunKategorisi;
        public string kategorininAdi;

        public UrunOzellikleri()
        {
            urunAdi = new List<string>();
            porsiyonFiyati = new List<string>();
            urunKategorisi = new List<string>();
        }
    }
}
