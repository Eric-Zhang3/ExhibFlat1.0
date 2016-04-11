using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.SessionState;

namespace Flat.Web.API
{
    //using ExhibFlat.Membership.Context;
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Web;
    using ExhibFlat.SiteSet;
    using ExhibFlat.Entities.Members;

    public class LoginUser : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            string s = "";
            string str2 = context.Request.QueryString["action"];
            if (!string.IsNullOrEmpty(str2) && (str2 == "login"))
            {

                string username = "";
                SessionUser user = new SessionUser();
                user = RequestSession.GetSessionUser();
                if (user != null)
                {
                    
                    //String i = HttpUtility.UrlDecode(info.Value);
                    //JArray o = JArray.Parse(i.Split('&')[0]);
                    //JToken j = o[0];
                    //username = j.SelectToken("userName").ToString().Trim('\"');
                    username = user.UserName.ToString();
                }

                
                SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);

                
                s = ("{\"sitename\":\"" + masterSettings.SiteName + "\",") + "\"username\":\"" + username +
                    "\",\"AdminLogoUrl\":\"" + masterSettings.AdminLogoUrl + "\",\"SiteDescription\":\"" + masterSettings.SiteDescription + "\"}";
            }
            
            context.Response.ContentType = "text/json";
            context.Response.Write(s);
        }

        public bool IsReusable
        {
            get { return false; }
        }

        

        #region 获取本机Ip

        public static string GetLocalIP()
        {
            try
            {
                string HostName = Dns.GetHostName(); //得到主机名 
                IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
                for (int i = 0; i < IpEntry.AddressList.Length; i++)
                {
                    //从IP地址列表中筛选出IPv4类型的IP地址 
                    //AddressFamily.InterNetwork表示此IP为IPv4, 
                    //AddressFamily.InterNetworkV6表示此地址为IPv6类型

                    if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        return IpEntry.AddressList[i].ToString();
                    }
                }

                return "";
            }

            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion
    }
}