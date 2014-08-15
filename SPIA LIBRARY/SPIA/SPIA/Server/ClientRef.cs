using System;
using System.Collections.Generic;
using System.Text;

namespace SPIA.Server
{    
    /// Sunucuya baðlý olan bir clientyi temsil eder    
    public interface ClientRef
    {        
        /// Ýstemciyi temsil eden tekil ID deðeri        
        long ClientID { get; }
        
        /// Ýstemci ile baðlantýnýn doðru þekilde kurulu olup olmadýðýný verir. True ise mesaj yollanýp alýnabilir.        
        bool BaglantiVar { get; }        
        
        /// Ýstemciye bir mesaj yollamak içindir        
        /// <param name="mesaj">Yollanacak mesaj</param>
        /// <returns>Ýþlemin baþarý durumu</returns>
        bool MesajYolla(string mesaj);
        
        /// Ýstemci ile olan baðlantýyý kapatýr        
        void BaglantiyiKapat();

        void gonder(string komut, string Filename, string path);
    }
}
