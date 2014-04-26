using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ROP.Commons;

namespace ROP.Business
{
    public class SiparisBilgisiData
    {

        public static entSiparis GetSiparisBilgileri()
        {
            entSiparis myInfo = new entSiparis();

            using (SqlConnection viewdataConnection = DataBaseSettings.GetConnection())
            {
                string queryString = String.Format("SELECT top 1 * from Siparis ");

                using (SqlCommand co = new SqlCommand(queryString, viewdataConnection))
                {
                    using (SqlDataReader myReader = co.ExecuteReader())
                    {
                        while (myReader.Read())
                        {
                            myInfo.siparisId = Convert.ToInt32(myReader["SiparisID"]);
                            myInfo.garson = Convert.ToString(myReader["Garsonu"]);
                            //myInfo.masa = Convert.ToInt32(myReader["Masa"]);
                            myInfo.siparis = Convert.ToString(myReader["YemekAdi"]);
                            //myInfo.departman = Convert.ToString(myReader["Departman"]);
                        }
                    }
                }

                DataBaseSettings.CloseConnection();
            }
            return myInfo;
        }
    }
}
