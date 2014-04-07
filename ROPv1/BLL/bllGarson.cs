using ROPv1.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ROPv1.DAL;

namespace ROPv1.BLL
{


    public class bllGarson
    {
        #region Constants

        #endregion

        #region Events

        #endregion


        #region Methods

        public static bool denemeEkle(entGarson yeniGarson) 
        {
            return  dalGarson.denemeEkle(yeniGarson);
        }
        #endregion


    }
}
