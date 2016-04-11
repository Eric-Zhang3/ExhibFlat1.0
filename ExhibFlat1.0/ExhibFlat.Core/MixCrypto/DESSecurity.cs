using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace ExhibFlat.Core
{
    public class DESSecurity
    {
        int rep = 0;
        private string GenerateCheckCode(int codeCount)
        {
            StringBuilder sb = new StringBuilder();
            long num2 = DateTime.Now.Ticks + this.rep;
            this.rep++;
            Random random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> this.rep)));
            for (int i = 0; i < codeCount; i++)
            {
                char ch;
                int num = random.Next();
                if ((num % 2) == 0)
                {
                    ch = (char)(0x30 + ((ushort)(num % 10)));
                }
                else
                {
                    ch = (char)(0x41 + ((ushort)(num % 0x1a)));
                }
                sb.Append(ch.ToString());
            }
            return sb.ToString();
        }
        /// <summary>
        /// 随机生成KEY
        /// </summary>
        /// <returns></returns>
        public List<byte[]> GenerateKey()
        {
            DateTime now = DateTime.Now;
            string key = now.ToString("ddyyyyMM"), iv = now.ToString("MMddyyyy");
            byte[] btKey = Encoding.UTF8.GetBytes(key);
            byte[] btIV = Encoding.UTF8.GetBytes(iv);
            List<byte[]> lst = new List<byte[]>(2);
            lst.Add(btKey);
            lst.Add(btIV);
            return lst;
        }
        /// <summary>
        /// DES 加密过程
        /// </summary>
        /// <param name="dataToEncrypt">待加密数据</param>
        /// <param name="DESKey"></param>
        /// <returns></returns>
        public string Encrypt(string dataToEncrypt)
        {
            List<byte[]> kv = GenerateKey();
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                byte[] inputByteArray = Encoding.Default.GetBytes(dataToEncrypt);//把字符串放到byte数组中
                des.Key = kv[0]; //建立加密对象的密钥和偏移量
                des.IV = kv[1];
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                        StringBuilder ret = new StringBuilder();
                        foreach (byte b in ms.ToArray())
                        {
                            ret.AppendFormat("{0:x2}", b);
                        }
                        return ret.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// DES 解密过程
        /// </summary>
        /// <param name="dataToDecrypt">待解密数据</param>
        /// <param name="DESKey"></param>
        /// <returns></returns>
        public string Decrypt(string dataToDecrypt)
        {
            List<byte[]> kv = GenerateKey();
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                byte[] inputByteArray = new byte[dataToDecrypt.Length / 2];
                for (int x = 0; x < dataToDecrypt.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(dataToDecrypt.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }
                des.Key = kv[0]; //建立加密对象的密钥和偏移量
                des.IV = kv[1];
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                        return System.Text.Encoding.Default.GetString(ms.ToArray());
                    }
                }
            }
        }
    }
}