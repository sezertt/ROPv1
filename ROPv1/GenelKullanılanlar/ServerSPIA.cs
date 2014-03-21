using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPIA;
using SPIA.Server;

namespace ROPv1
{
    public class ServerSPIA
    {
        // SPIA kütüphanesindeki sunucu nesnesi        
        private SPIAServer sunucu;

        // Sunucuya bağlı olan kullanıcıları saklayan liste        
        private List<Kullanici> kullanicilar = new List<Kullanici>();

    }
}