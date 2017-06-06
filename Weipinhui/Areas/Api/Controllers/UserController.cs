using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business;
using Entity;
using Util;

namespace Weipinhui.Areas.Api.Controllers
{
    public class UserController : BaseController
    {
        
        private UserBusiness _userbus = new UserBusiness();

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="newUser">用户信息</param>
        /// <returns></returns>
        public ActionResult Register(User newUser)
        {
            return Content(_userbus.Register(newUser));
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public ActionResult Login(string account, string password)
        {
            if(_userbus.Login(account,password)==1)
            {
                Session["Account"] = account;
                var res = new
                {
                    State = 1,
                    Msg = "登陆成功！"
                };

                return Content(res.ToJson());
            }
            else
            {
                var res = new
                {
                    State = -1,
                    Msg = "账号/密码错误！"
                };

                return Content(res.ToJson());
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetUserInfo()
        {
            if(Session["Account"]!=null)
            {
                string account = Session["Account"].ToString();
                var res = new
                {
                    State = 1,
                    Info = _userbus.GetTable().Where(x => x.Account == account).First(),
                    Msg = "获取成功！"
                };
                return Content(res.ToJson());
            }
            else
            {
                var res = new
                {
                    State = -1,
                    Msg = "获取失败！"
                };

                return Content(res.ToJson());
            }
        }
    }
}