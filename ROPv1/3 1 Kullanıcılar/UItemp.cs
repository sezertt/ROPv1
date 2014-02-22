using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROPv1
{
    [Serializable]
    public class UItemp
    {
        public byte[] UIUN; // kullanıcı adı
        public byte[] UIN; // adı
        public byte[] UIS; // soyadı
        public string UIPW; // şifresi
        public byte [] UIU; // ünvanı
        public string UIPN; // pin kodu
        public string[] UIY; // yetkileri

        public UItemp()
        {
            UIY = new string[8];
        }
    }
}
