<%@ WebHandler Language="C#" Class="findreg" %>

using System;
using System.Web;

public class findreg : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string AppKey = System.Configuration.ConfigurationManager.AppSettings["AppKey"].ToString();
        context.Response.Write(SsoSessionKeyUtil.FindPassword + ";" + SsoSessionKeyUtil.Register + "?appkey=" + AppKey);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}