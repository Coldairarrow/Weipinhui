using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Util;
namespace Weipinhui.Areas.Api.Controllers
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
            if (_customMode==100)
            {
                return;
            }
            //是否已登录
            if (HttpContext.Current.Session["Account"]==null)
            {
                var res = new
                {
                    State = -1,
                    Msg = "请登录！"
                };
                filterContext.Result = new JsonResult { Data=res, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                return;
            }
        }
    }

    public class BaseController : Controller
    {
    }
}