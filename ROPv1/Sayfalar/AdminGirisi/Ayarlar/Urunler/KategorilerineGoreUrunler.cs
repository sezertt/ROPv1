using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROPv1
{
    [Serializable]
    public class KategorilerineGoreUrunler
    {
        public List<string> urunAdi;
        public List<string> urunFiyati;
        public List<string> urunKategorisi;
        public List<string> urunAciklamasi;
        public List<bool> urunMutfagaBildirilmeliMi;
        public List<int> urunPorsiyonSinifi; // 0 ve diğerleri ürün sadece tek porsiyon olarak satılıyor --- 1 ürün yarım porsiyon olarak satılıyor --- 2 çeyrek
        public List<int> urunKDV;
        public string kategorininAdi;


        public KategorilerineGoreUrunler()
        {
            urunAdi = new List<string>();
            urunFiyati = new List<string>();
            urunKategorisi = new List<string>();
            urunAciklamasi = new List<string>();
            urunMutfagaBildirilmeliMi = new List<bool>();
            urunPorsiyonSinifi = new List<int>();
            urunKDV = new List<int>();
        }
    }
}