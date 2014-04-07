using System;
using System.Collections.Generic;
using System.Text;

namespace SPIA.Server
{
    // Delegateler ////////////////////////////////////////////////////////////

    public delegate void dgYeniClientBaglandi(ClientBaglantiArgumanlari e);
    public delegate void dgClientBaglantisiKapatildi(ClientBaglantiArgumanlari e);
    public delegate void dgClientdanYeniMesajAlindi(ClientdanMesajAlmaArgumanlari e);

    // Delegate'ler için parametre sýnýflarý //////////////////////////////////

    public class ClientBaglantiArgumanlari : EventArgs
    {
        public ClientRef Client
        {
            get { return client; }
        }
        private ClientRef client;

        public ClientBaglantiArgumanlari(ClientRef client)
        {            
            this.client = client;
        }
    }

    public class ClientdanMesajAlmaArgumanlari : EventArgs
    {
        public ClientRef Client
        {
            get { return client; }
        }
        private ClientRef client;

        public string Mesaj
        {
            get { return mesaj; }
            set { mesaj = value; }
        }
        private string mesaj;

        public ClientdanMesajAlmaArgumanlari(ClientRef client, string mesaj)
        {
            this.client = client;
            this.mesaj = mesaj;
        }
    }
}
