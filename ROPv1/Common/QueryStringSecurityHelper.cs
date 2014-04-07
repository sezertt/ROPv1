using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ROPv1.Common;
using System.Web;


namespace ROPv1.Common
{
        public class QueryStringSecurityHelper
        {
            public static string AddUrlParameter(string Url, string ParameterName, string ParameterValue)
            {
                string myRawUrl = "";

                string myQstr = "";

                if (Url.IndexOf("?") > 0)
                {
                    myRawUrl = Url.Split('?')[0];

                    myQstr = Url.Split('?')[1];

                    myQstr =  EncryptionUtility.Decrypt(HttpUtility.UrlDecode(myQstr));

                    myQstr += "&";
                }
                else
                {
                    myRawUrl = Url;
                }

                myQstr += ParameterName.Trim() + "=" + ParameterValue.Trim();
                string myUrl = myRawUrl + "?" + HttpUtility.UrlEncode( EncryptionUtility.Encrypt(myQstr));

                return myUrl;
            }

            public static string DecryptUrl(string SecuredUrl)
            {
                Uri myUri = new Uri(SecuredUrl);
                string myRawUrl = myUri.AbsoluteUri.Substring(0, myUri.AbsoluteUri.Length - myUri.Query.Length);

                string myQstr = myUri.Query.TrimStart('?');
                string myUrl;

                if (myQstr.Length > 0)
                {
                    myUrl = myRawUrl + "?" +  EncryptionUtility.Decrypt(HttpUtility.UrlDecode(myQstr));
                }
                else
                {
                    myUrl = myRawUrl;
                }

                return myUrl;
            }

            public static string GetParameterValue(string SecuredUrl, string ParamName)
            {

                string OpenUrl;

                OpenUrl = DecryptUrl(SecuredUrl);

                if ((String.IsNullOrEmpty(OpenUrl))) return "";

                Uri OpenUri = new Uri(OpenUrl);
                return HttpUtility.ParseQueryString(OpenUri.Query).Get(ParamName);

            }



        }
    }

