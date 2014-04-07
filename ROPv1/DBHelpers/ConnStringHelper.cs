using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ROPv1.Common;

namespace ROPv1.DBHelpers
{
    public class ConnStringHelper
    {
        private static Dictionary<string, string> connStrings = new Dictionary<string, string>();

        public static string SqlConnString
        {
            get
            {
                if (connStrings.ContainsKey("sqlConn") == false)
                {
                    string tmp = "";
                    //if ( ConfigurationManager.ConnectionStrings["sqlConn"] != null && !string.IsNullOrWhiteSpace(ConfigurationManager.ConnectionStrings["sqlConn"].ConnectionString))
                    //    tmp = ConfigurationManager.ConnectionStrings["sqlConn"].ConnectionString;

                    if (Common.EncryptionUtility.StringEncrypted(tmp))
                        tmp = Common.EncryptionUtility.Decrypt(tmp);
                    //else
                    //    SaveConnectionString("sqlConn", Common.EncrytionUtility.Encrypt(tmp));

                    if (connStrings.ContainsKey("sqlConn"))
                        connStrings["sqlConn"] = tmp;
                    else
                        connStrings.Add("sqlConn", tmp);
                }

                return connStrings["sqlConn"];
            }
        }

        public static string SqlConnStringCustom(string connKey)
        {
            if (connStrings.ContainsKey(connKey) == false)
            {
                string tmp = "";
                //if (ConfigurationManager.ConnectionStrings[connKey] != null && !string.IsNullOrWhiteSpace(ConfigurationManager.ConnectionStrings[connKey].ConnectionString))
                //    tmp = ConfigurationManager.ConnectionStrings[connKey].ConnectionString;

                if (Common.EncryptionUtility.StringEncrypted(tmp))
                    tmp = Common.EncryptionUtility.Decrypt(tmp);
                //else
                //    SaveConnectionString("sqlConn", Common.EncrytionUtility.Encrypt(tmp));

                if (connStrings.ContainsKey(connKey))
                    connStrings[connKey] = tmp;
                else
                    connStrings.Add(connKey, tmp);
            }

            return connStrings[connKey];
        }
    }
}

