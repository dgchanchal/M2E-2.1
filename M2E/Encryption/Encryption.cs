using System.Security.Cryptography;
using System.Text;
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
    }
}