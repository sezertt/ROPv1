using System;
using System.Collections.Generic;
using System.Text;

namespace SPIA.Server
{    
    /// Sunucuya ba�l� olan bir clientyi temsil eder    
    public interface ClientRef
    {        
        /// �stemciyi temsil eden tekil ID de�eri        
        long ClientID { get; }
        
        /// �stemci ile ba�lant�n�n do�ru �ekilde kurulu olup olmad���n� verir. True ise mesaj yollan�p al�nabilir.        
        bool BaglantiVar { get; }        
        
        /// �stemciye bir mesaj yollamak i�indir        
        /// <param name="mesaj">Yollanacak mesaj</param>
        /// <returns>��lemin ba�ar� durumu</returns>
        bool MesajYolla(string mesaj);
        
        /// �stemci ile olan ba�lant�y� kapat�r        
        void BaglantiyiKapat();

        void gonder(string komut, string Filename, string path);
    }
}
