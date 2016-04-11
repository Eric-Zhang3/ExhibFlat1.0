using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Data;
using System.Collections;

using ExhibFlat.ControlPanel.Commodities;
using ExhibFlat.Entities.Members;
using ExhibFlat.SiteSet;
namespace Flat.Web.API
{
    /// <summary>
    /// Frame 的摘要说明
    /// </summary>
    public class Frame : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
            context.Response.AddHeader("pragma", "no-cache");
            context.Response.AddHeader("cache-control", "");
            context.Response.CacheControl = "no-cache";
            string Action = context.Request["action"];                      //提交动作
            string user_Account = context.Request["user_Account"];          //账户
            string userPwd = context.Request["userPwd"];                    //密码
            
            
            switch (Action)
            {
                case "login":
                    
                    DataTable dtlogin = ProductHelper.UserLogin(user_Account.Trim(), userPwd.Trim());
                    if (dtlogin != null)
                    {
                       
                        if (dtlogin.Rows.Count != 0)
                        {
                            
                            if (dtlogin.Rows[0]["DeleteMark"].ToString() == "1")
                            {
                                if (Islogin(context, user_Account))
                                {
                                    SessionUser user = new SessionUser();
                                    user.UserId = dtlogin.Rows[0]["User_ID"].ToString();
                                    user.UserAccount = dtlogin.Rows[0]["User_Account"].ToString();
                                    user.UserName = dtlogin.Rows[0]["User_Name"].ToString() + "(" + dtlogin.Rows[0]["User_Account"].ToString() + ")";
                                    user.UserPwd = dtlogin.Rows[0]["User_Pwd"].ToString();
                                    RequestSession.AddSessionUser(user);
                                    context.Response.Write("3");//验证成功
                                    context.Response.End();
                                }
                                else
                                {
                                    context.Response.Write("6");//该用户已经登录，不允许重复登录
                                    context.Response.End();
                                }
                            }
                            else
                            {
                                //user_idao.SysLoginLog(user_Account, "2", OWNER_address);//账户被锁,联系管理员！
                                context.Response.Write("2");
                                context.Response.End();
                            }
                        }
                        else
                        {
                            //user_idao.SysLoginLog(user_Account, "0", OWNER_address);
                            context.Response.Write("4");//账户或者密码有错误！
                            context.Response.End();
                        }
                    }
                    else
                    {
                        context.Response.Write("5");//服务连接不上！
                        context.Response.End();
                    }
                    break;
                
                default:
                    break;
            }
        }
        /// <summary>
        /// 同一账号不能同时登陆
        /// </summary>
        /// <param name="context"></param>
        /// <param name="User_Account">账户</param>
        /// <returns></returns>
        public bool Islogin(HttpContext context, string User_Account)
        {
            //将Session转换为Arraylist数组
            //ArrayList list = context.Session["GLOBAL_USER_LIST"] as ArrayList;
            //if (list == null)
            //{
            //    list = new ArrayList();
            //}
            //for (int i = 0; i < list.Count; i++)
            //{
            //    if (User_Account == (list[i] as string))
            //    {
            //        //已经登录了，提示错误信息 
            //        return false; ;
            //    }
            //}
            ////将用户信息添加到list数组中
            //list.Add(User_Account);
            ////将数组放入Session
            //context.Session.Add("GLOBAL_USER_LIST", list);
            return true;
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}