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
        public string UIUN; // kullanıcı adı
        public string UIN; // adı
        public string UIS; // soyadı
        public string UIPW; // şifresi
        public string UIU; // ünvanı
        public string UIPN; // pin kodu
        public string[] UIY; // yetkileri

        public UItemp()
        {
            UIY = new string[7];
        }
    }
}
