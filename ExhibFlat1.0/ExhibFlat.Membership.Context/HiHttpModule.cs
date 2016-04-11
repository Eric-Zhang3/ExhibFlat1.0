
namespace ExhibFlat.Membership.Context
{
    using ExhibFlat.Core;
    using ExhibFlat.Core.Configuration;
    using ExhibFlat.Core.Enums;
    using ExhibFlat.Core.Jobs;
    using ExhibFlat.Core.Urls;
    using System;
    using System.Configuration;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Security;
    using System.IO;
    using System.Net;
    using System.Text;
    using ExhibFlat.Membership.Core;
    using ExhibFlat.Membership.Core.Enums;
    

    public class HiHttpModule : IHttpModule
    {
        private bool applicationInstalled;
        private ApplicationType currentApplicationType;

        private static readonly Regex urlReg =
            new Regex(
                "(sso.aspx|loginentry.aspx|login.aspx|logout.aspx|resourcenotfound.aspx|verifycodeimage.aspx|SendPayment.aspx|PaymentReturn_url|PaymentNotify_url|InpourReturn_url|InpourNotify_url)",
                RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static String fiterUrl(String url, String param)
        {
            if (String.IsNullOrEmpty(param))
                return url;
            Regex reg = new Regex("(&|\\?|)(" + param + "(=[^&$]*){0,1})(&|$)", RegexOptions.Singleline);
            return reg.IsMatch(url) ? reg.Replace(url, "$1") : url;
        }

        private void Application_AuthorizeRequest(object source, EventArgs e)
        {
            if (this.currentApplicationType != ApplicationType.Installer)
            {
                HttpApplication application = (HttpApplication)source;
                HttpContext context = application.Context;
                HiContext current = HiContext.Current;


                if (context.Request.IsAuthenticated)
                {
                    string name = context.User.Identity.Name;
                    if (name != null)
                    {
                        string[] rolesForUser = Roles.GetRolesForUser(name);
                        if ((rolesForUser != null) && (rolesForUser.Length > 0))
                        {
                            current.RolesCacheKey = string.Join(",", rolesForUser);
                        }
                    }
                }
            }
        }

        private void Application_BeginRequest(object source, EventArgs e)
        {
            this.currentApplicationType = HiConfiguration.GetConfig().AppLocation.CurrentApplicationType;
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;
            if (context.Request.RawUrl.IndexOfAny(new char[] { '<', '>', '\'', '"' }) != -1)
            {
                context.Response.Redirect(
                    context.Request.RawUrl.Replace("<", "%3c")
                        .Replace(">", "%3e")
                        .Replace("'", "%27")
                        .Replace("\"", "%22"), false);
            }
            else
            {
                this.CheckInstall(context);
                if (this.currentApplicationType != ApplicationType.Installer)
                {
                    CheckSSL(HiConfiguration.GetConfig().SSL, context);
                    HiContext.Create(context, new UrlReWriterDelegate(HiHttpModule.ReWriteUrl));
                    if (HiContext.Current.SiteSettings.IsDistributorSettings &&
                        !((!HiContext.Current.SiteSettings.Disabled ||
                           (this.currentApplicationType != ApplicationType.Common)) ||
                          urlReg.IsMatch(context.Request.Url.AbsolutePath)))
                    {
                        context.Response.Write("站点维护中，暂停访问！");
                        context.Response.End();
                    }
                }
            }

            #region MyRegion by yongjin.C
            try
            {

                String SessionKey = context.Request.QueryString["SessionKey"];
                if (SessionKey == null)
                {
                    SessionKey = context.Request.Cookies["SSOSessionKey"] == null ? null : context.Request.Cookies["SSOSessionKey"].Value;
                }
                if (!String.IsNullOrEmpty(SessionKey) && SessionKey.ToLower() != "null")
                {
                    String AppKey = ConfigurationManager.AppSettings["AppKey"];
                    String SSOPassport = ConfigurationManager.AppSettings["SSOPassport"];

                    String url;

                    //去掉端口号
                    if (HttpContext.Current.Request.Url.ToString().ToLower().IndexOf("localhost") > -1)
                        url = context.Request.Url.ToString();
                    else
                        url = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.Url.PathAndQuery;
                    log.GetInstance().writeLog(AppKey + "-*-" + SessionKey + "-*-" + url);
                    String info = CheckLogin(AppKey, SessionKey);
                    log.GetInstance().writeLog(info);
                    String[] infoarray = info.Split('&');
                    if (!String.IsNullOrEmpty(info) && infoarray.Length == 2)
                    {
                        var sessionUserInfo = HttpContext.Current.Request.Cookies["SessionUserInfo"];
                        if (sessionUserInfo != null)
                        {
                            sessionUserInfo.Value = HttpUtility.UrlEncode(infoarray[0]);
                            sessionUserInfo["SessionKey"] = SessionKey;
                            sessionUserInfo["defer"] = infoarray[1];
                            HttpContext.Current.Response.AppendCookie(sessionUserInfo);
                        }
                        else
                        {
                            HttpCookie nCookie = new HttpCookie("SessionUserInfo", HttpUtility.UrlEncode(infoarray[0]));
                            nCookie["SessionKey"] = SessionKey;
                            nCookie["defer"] = infoarray[1];
                            HttpContext.Current.Response.Cookies.Add(nCookie);
                        }

                        //过滤SessionKey
                        url = fiterUrl(url, "SessionKey");
                        url = fiterUrl(url, "from");
                        String from = HttpContext.Current.Request.QueryString["from"];
                        StringBuilder rurl = new StringBuilder("/SSO.aspx?login=1");
                        if (!String.IsNullOrEmpty(from))
                        {
                            rurl.Append("&from=" + from);
                        }
                        rurl.Append("&returnurl=" +
                                    HttpUtility.UrlEncode(url,
                                        Encoding.UTF8));
                        HttpContext.Current.Response.Redirect(rurl.ToString(), true);
                        return;
                    }
                    else if (String.IsNullOrEmpty(info))
                    {
                        log.blog = true;
                        log.GetInstance().writeLog("用户为空");
                        url = fiterUrl(url, "SessionKey");
                        //过滤SessionKey
                        url = fiterUrl(url, "from");
                        HttpContext.Current.Response.Redirect(
                            string.Format("{0}/Passport/login?appkey={1}&redirectUrl={2}",
                                SSOPassport.Trim('/'), AppKey, HttpUtility.UrlEncode(url,
                                    Encoding.UTF8)), false);
                        return;
                    }
                }
                else
                {
                    var sessionUserInfo = HttpContext.Current.Request.Cookies["SessionUserInfo"];

                    //  var cookieSession = HttpContext.Current.Request.Cookies["SessionKey"];
                    if (sessionUserInfo != null &&
                        DateTime.Now.AddMinutes(5) >= Convert.ToDateTime(sessionUserInfo["defer"]))
                    {
                        // 定时延期 延期
                        String AppKey = ConfigurationManager.AppSettings["AppKey"];
                        String defer = null;
                        string r = toKeepSessionKey(AppKey, sessionUserInfo["SessionKey"]);
                        if (!string.IsNullOrEmpty(r))
                        {
                            var ar = r.Split('&');
                            if (ar.Length == 2)
                                defer = ar[1];
                        }
                        if (!string.IsNullOrEmpty(defer))
                            sessionUserInfo["defer"] = defer; //设置过期时间有问题
                        HttpContext.Current.Response.AppendCookie(sessionUserInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                log.blog = true;
                log.GetInstance().writeLog(ex.Message);
            }

            #endregion
        }

        #region MyRegion by yongjin.C

        private String toKeepSessionKey(string appkey, string sessionKey)
        {
            String SSOPassport = ConfigurationManager.AppSettings["SSOPassport"];
            String url = String.Format("{0}/api/sessionKeep?appKey={1}&sessionKey={2}", SSOPassport.Trim('/'), appkey,
                sessionKey);

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "GET";
            webRequest.ContentType = "application/json;charset=utf-8";
            WebResponse rsp = webRequest.GetResponse();
            Stream stream = null;
            StreamReader reader = null;

            try
            {
                stream = rsp.GetResponseStream();
                if (stream != null)
                    reader = new StreamReader(stream, Encoding.UTF8);
                return reader != null ? reader.ReadToEnd().Trim('\"').Replace("\\", "") : "";
            }
            finally
            {
                if (reader != null) reader.Close();
                if (stream != null) stream.Close();
                rsp.Close();
            }
            return null;
        }

        private static String CheckLogin(string appkey, string sessionKey)
        {
            String SSOPassport = ConfigurationManager.AppSettings["SSOPassport"];
            String url = String.Format("{0}/api/sessionCheck?appKey={1}&sessionKey={2}", SSOPassport.Trim('/'), appkey,
                sessionKey);
            log.GetInstance().writeLog(url);
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "GET";
            webRequest.ContentType = "application/json;charset=utf-8";
            WebResponse rsp = webRequest.GetResponse();
            Stream stream = null;
            StreamReader reader = null;

            try
            {
                stream = rsp.GetResponseStream();
                if (stream != null)
                    reader = new StreamReader(stream, Encoding.UTF8);
                return reader != null ? reader.ReadToEnd().Trim('\"').Replace("\\", "") : "";
            }
            finally
            {
                if (reader != null) reader.Close();
                if (stream != null) stream.Close();
                rsp.Close();
            }
            return null;
        }

        #endregion

        private void CheckInstall(HttpContext context)
        {
            if ((this.currentApplicationType == ApplicationType.Installer) && this.applicationInstalled)
            {
                context.Response.Redirect(Globals.GetSiteUrls().Home, true);
            }
            else if (!(this.applicationInstalled || (this.currentApplicationType == ApplicationType.Installer)))
            {
                context.Response.Redirect(Globals.ApplicationPath + "/installer/default.aspx", true);
            }
        }

        private static void CheckSSL(SSLSettings ssl, HttpContext context)
        {
            if (ssl == SSLSettings.All)
            {
                Globals.RedirectToSSL(context);
            }
        }

        public void Dispose()
        {
            if (this.currentApplicationType != ApplicationType.Installer)
            {
                ExhibFlat.Core.Jobs.Jobs.Instance().Stop();
            }
        }

        public void Init(HttpApplication application)
        {
            if (null != application)
            {
                application.BeginRequest += new EventHandler(this.Application_BeginRequest);
                application.AuthorizeRequest += new EventHandler(this.Application_AuthorizeRequest);
                this.applicationInstalled = ConfigurationManager.AppSettings["Installer"] == null;
                this.currentApplicationType = HiConfiguration.GetConfig().AppLocation.CurrentApplicationType;
                this.CheckInstall(application.Context);
                if (this.currentApplicationType != ApplicationType.Installer)
                {
                    try
                    {
                        ExhibFlat.Core.Jobs.Jobs.Instance().Start();
                    }
                    catch (Exception)
                    {
                    }
                    ExtensionContainer.LoadExtensions();
                }
            }
        }

        private static bool ReWriteUrl(HttpContext context)
        {
            string path = context.Request.Path;
            string filePath = UrlReWriteProvider.Instance().RewriteUrl(path, context.Request.Url.Query);
            if (filePath != null)
            {
                string queryString = null;
                int index = filePath.IndexOf('?');
                if (index >= 0)
                {
                    queryString = (index < (filePath.Length - 1)) ? filePath.Substring(index + 1) : string.Empty;
                    filePath = filePath.Substring(0, index);
                }
                context.RewritePath(filePath, null, queryString);
            }
            return (filePath != null);
        }

        public string ModuleName
        {
            get { return "HiHttpModule"; }
        }
    }
}