using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ROPv1.Common
{
    public class EncryptionUtility
    {
        // Fields
        private static readonly byte[] key = new byte[] { 33, 39, 185, 52, 45, 254, 35, 31, 203, 33, 8, 203, 31, 58, 35, 12, 4, 193, 17, 9, 222, 8, 11, 247, 45, 86, 75, 135, 182, 73, 245, 116 };
        private static readonly byte[] iv = new byte[] { 54, 148, 32, 84, 21, 250, 176, 253, 55, 167, 254, 58, 69, 68, 82, 52 };

        // Methods
        public static string Decrypt(string StringToDecrypt)
        {
            RijndaelManaged aesAlg = null;
            CryptoStream csDecrypt = null;
            MemoryStream msDecrypt = null;
            string plaintext = null;
            StreamReader srDecrypt = null;
            try
            {
                aesAlg = new RijndaelManaged();

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(key, iv);
                msDecrypt = new MemoryStream(Convert.FromBase64String(StringToDecrypt));
                csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                srDecrypt = new StreamReader(csDecrypt);
                plaintext = srDecrypt.ReadToEnd();
            }
            finally
            {
                if (srDecrypt != null)
                {
                    srDecrypt.Close();
                }
                if (csDecrypt != null)
                {
                    csDecrypt.Close();
                }
                if (msDecrypt != null)
                {
                    msDecrypt.Close();
                }
                if (aesAlg != null)
                {
                    aesAlg.Clear();
                }
            }
            return plaintext;
        }

        public static string Encrypt(string StringToEncrypt)
        {
            RijndaelManaged aesAlg = null;
            CryptoStream csEncrypt = null;
            MemoryStream msEncrypt = null;
            StreamWriter swEncrypt = null;
            try
            {
                aesAlg = new RijndaelManaged();
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(key, iv);
                msEncrypt = new MemoryStream();
                csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
                swEncrypt = new StreamWriter(csEncrypt);
                swEncrypt.Write(StringToEncrypt);
            }
            finally
            {
                if (swEncrypt != null)
                {
                    swEncrypt.Close();
                }
                if (csEncrypt != null)
                {
                    csEncrypt.Close();
                }
                if (msEncrypt != null)
                {
                    msEncrypt.Close();
                }
                if (aesAlg != null)
                {
                    aesAlg.Clear();
                }
            }
            return Convert.ToBase64String(msEncrypt.ToArray());
        }

        public static bool StringEncrypted(string StringToTest)
        {
            try
            {
                Decrypt(StringToTest);

                return true;
            }
            catch
            {
                return false;
            }


        }
    }
}

