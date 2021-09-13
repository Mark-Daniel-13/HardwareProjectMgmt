using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebPOS.Business.Helpers
{
    public class Encryption
    {
        // Hash an input string and return the hash as
        // a 32 character hexadecimal string.
        public static string EncryptMD5(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        public static bool CheckMD5(string input, string hash)
        {
            // Hash the input.
            string hashOfInput = EncryptMD5(input);

            // Create a StringComparer an comare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool EcryptFile(string filepath, string pass, bool decrypt = false)
        {
            try
            {
                byte[] content = File.ReadAllBytes(filepath);
                using (var crypter = new DESCryptoServiceProvider())
                {
                    crypter.IV = Encoding.UTF8.GetBytes(pass);
                    crypter.Key = Encoding.UTF8.GetBytes(pass);
                    crypter.Mode = CipherMode.CBC;
                    crypter.Padding = PaddingMode.PKCS7;

                    var type = decrypt ? crypter.CreateDecryptor() : crypter.CreateEncryptor();

                    using (var stream = new MemoryStream())
                    {
                        CryptoStream cryptoStream = new CryptoStream(stream, type, CryptoStreamMode.Write);
                        cryptoStream.Write(content, 0, content.Length);
                        cryptoStream.FlushFinalBlock();
                        File.WriteAllBytes(filepath, stream.ToArray());
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                string msg = e.Message;
                return false;
            }

        }
    }
}
