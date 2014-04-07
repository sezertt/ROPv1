using ROPv1.DBHelpers;
using ROPv1.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROPv1.DAL
{


    //TODO : Ayarla  ;)

    #region Constants

    #endregion

    #region Constants

    #endregion

    #region Constants

    #endregion

    public class dalGarson
    {
        public static bool denemeEkle(entGarson yeniGarson)
        {
            SqlHelper sh = new SqlHelper();
            try
            {
                string qry = "INSERT INTO Deneme (Yazi, Rakam) VALUES (@Yazi, @Rakam)";
                sh.AddParameter("@Yazi", yeniGarson.Adi);
                sh.AddParameter("@Rakam", yeniGarson.Pin);
                sh.ExecuteNonQuery(qry);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
