using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace ExhibFlat.Core
{
    public class MixSecurity
    {
        private static string _Rsa_PubKey = System.Web.HttpUtility.HtmlDecode(ConfigurationManager.AppSettings["Rsa_PubKey"]), _Rsa_PriKey = System.Web.HttpUtility.HtmlDecode(ConfigurationManager.AppSettings["Rsa_PriKey"]);
        public static string Encrypt(string data)
        {
            RSASecurity RSA = new RSASecurity();
            data = RSA.Encrypt(data, _Rsa_PubKey);
            DESSecurity DES = new DESSecurity();
            data = DES.Encrypt(data);
            
            return data;
        }
        public static string Decrypt(string data)
        {
            DESSecurity DES = new DESSecurity();
            data = DES.Decrypt(data);
            RSASecurity RSA = new RSASecurity();
            data = RSA.Decrypt(data, _Rsa_PriKey);
            
            return data;
        }
    }
}
