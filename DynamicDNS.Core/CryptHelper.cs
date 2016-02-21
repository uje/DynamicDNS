using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DynamicDNS.Core {
    public static class CryptHelper {

        /// 获取密钥
        private const string Key = @")O[NB]6,YF}+efcaj{+oESb9d8>Z'e9M";

        /// 获取向量
        private const string IV = @"L+/~f4,Ir)b$=pkf";

        /// <summary>
        /// AES加密
        /// </summary>
        public static string AESEncrypt(string plainStr, string encodingKey = Key) {
            byte[] bKey = Encoding.UTF8.GetBytes(encodingKey);
            byte[] bIV = Encoding.UTF8.GetBytes(IV);
            byte[] byteArray = Encoding.UTF8.GetBytes(plainStr);

            string encrypt = null;
            Rijndael aes = Rijndael.Create();
            try {
                using (MemoryStream mStream = new MemoryStream()) {
                    using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateEncryptor(bKey, bIV), CryptoStreamMode.Write)) {
                        cStream.Write(byteArray, 0, byteArray.Length);
                        cStream.FlushFinalBlock();
                        encrypt = Convert.ToBase64String(mStream.ToArray());
                    }
                }
            }
            catch { }
            aes.Clear();

            return encrypt;
        }

        /// <summary>
        /// AES解密
        /// </summary>
        public static string AESDecrypt(string encryptStr, string decodingKey = Key) {
            byte[] bKey = Encoding.UTF8.GetBytes(decodingKey);
            byte[] bIV = Encoding.UTF8.GetBytes(IV);
            byte[] byteArray = Convert.FromBase64String(encryptStr);

            string decrypt = null;
            Rijndael aes = Rijndael.Create();
            try {
                using (MemoryStream mStream = new MemoryStream()) {
                    using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateDecryptor(bKey, bIV), CryptoStreamMode.Write)) {
                        cStream.Write(byteArray, 0, byteArray.Length);
                        cStream.FlushFinalBlock();
                        decrypt = Encoding.UTF8.GetString(mStream.ToArray());
                    }
                }
            }
            catch { }
            aes.Clear();

            return decrypt;
        }

    }
}
