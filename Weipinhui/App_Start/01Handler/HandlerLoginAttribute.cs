using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Weipinhui
{
    /// <summary>
    /// 描 述：登录认证（会话验证组件）
    /// </summary>
    public class HandlerLoginAttribute : AuthorizeAttribute
    {
        private int _customMode;
        /// <summary>默认构造</summary>
        /// <param name="Mode">认证模式</param>
        public HandlerLoginAttribute(int Mode)
        {
            _customMode = Mode;
        }
        /// <summary>
        /// 响应前执行登录验证,查看当前用户是否有效 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //登录拦截是否忽略，超级模式是否开启
            if (_customMode == 100)
            {
                return;
            }
            //是否已登录
            if (filterContext.HttpContext.Session["Account"] == null)
            {
                filterContext.Result = new RedirectResult("/Home/Login");
                return;
            }
        }
    }
}