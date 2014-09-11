using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.Configuration;
namespace M2E.Encryption
{
    public class EncryptionClass
    {
        public static string GetEncryptionKey(string plainText, string key)
        {            
            return AES.Encrypt(plainText, key);
        }
        public static string GetDecryptionValue(string cipherText, string key)
        {
            return AES.Decrypt(cipherText, key);
        }
        public static string Md5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(Encoding.ASCII.GetBytes(text));

            //get hash result after compute it
            var result = md5.Hash;

            var strBuilder = new StringBuilder();
            foreach (var token in result)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(token.ToString("x2"));
            }

            return strBuilder.ToString();
        }
        public static Dictionary<string, string> encryptUserDetails(Dictionary<string, string> data)
        {
            var encryptedData = new Dictionary<string, string>();
            string Authkey = ConfigurationManager.AppSettings["AuthKey"];
            encryptedData["UTMZK"] = EncryptionClass.GetEncryptionKey(data["Username"], Authkey);
            encryptedData["UTMZV"] = EncryptionClass.GetEncryptionKey(data["Password"], data["userGuid"]);
            return encryptedData;
        }

        public static Dictionary<string, string> decryptUserDetails(Dictionary<string, string> data)
        {
            var decryptedData = new Dictionary<string, string>();
            decryptedData["UTMZV"] = EncryptionClass.GetDecryptionValue(data["Password"], data["userGuid"]);
            return decryptedData;
        }

        public static string decryptRefKey(string username)
        {
            string Authkey = ConfigurationManager.AppSettings["AuthKey"];
            string decryptedData = EncryptionClass.GetDecryptionValue(username, Authkey);
            return decryptedData;
        }
    }
}