using log4net;
using System;
using System.IO;
using System.Web;

namespace Util
{
    /// <summary>
    /// 日志帮助类(使用log4net)
    /// </summary>
    public class LogHelper
    {
        static LogHelper()
        {
            FileInfo configFile = new FileInfo(HttpContext.Current.Server.MapPath("/Config/log4net.config"));
            log4net.Config.XmlConfigurator.Configure(configFile);
        }

        public static ILog GetLogger(Type type)
        {
            return LogManager.GetLogger(type);
        }

        public static ILog GetLogger(string str)
        {
            return LogManager.GetLogger(str);
        }
    }
}
