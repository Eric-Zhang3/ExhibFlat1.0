using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Web;

namespace ExhibFlat.SiteSet
{
    public class log
    {
        public static bool blog = false;
        private System.IO.StreamWriter sw;
        public static log instance;
        public static readonly object synObj = new object();
        private log()
        {
            try
            {
                sw = new System.IO.StreamWriter(HttpContext.Current.Server.MapPath("~/log.txt"), true, System.Text.Encoding.Default);
            }
            catch (Exception )
            {  
            }
        }


        public static log GetInstance()
        {
            if (instance == null)
            {
                lock (synObj)
                {
                    instance = new log();
                }
            }
            return instance;  
        }

        public void close()
        {
            if (instance != null)
            {
                sw.Close();
                sw.Dispose();
            }
        }

        public void writeLog(string str)
        {
            try
            {
                if (blog)
                {
                    lock (instance)
                    {
                        StackTrace trace = new StackTrace();  
                        string className = MethodBase.GetCurrentMethod().ReflectedType.Name;
                        string methodName =trace.GetFrame(1).GetMethod().GetType().Name;
                        sw.WriteLine(className +"."+ methodName + DateTime.Now.ToString() + "\t" + str, System.Text.Encoding.Default);
                        sw.Flush();
                    }
                }
            }
            catch (Exception)
            { 
            }
        } 
    }
}