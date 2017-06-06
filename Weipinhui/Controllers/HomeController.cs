using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business;
namespace Weipinhui.Controllers
{
    public class HomeController : Controller
    {
        private GoodsBusiness _goodsBus = new GoodsBusiness();
        private CartBusiness _cartBus = new CartBusiness();
        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        
        /// <summary>
        /// 登陆
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 注销用户
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session["Account"] = null;
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 商品分类
        /// </summary>
        /// <returns></returns>
        public ActionResult Goods_kinds()
        {
            return View();
        }

        public ActionResult List()
        {
            return View();
        }

        /// <summary>
        /// 商品详情
        /// </summary>
        /// <param name="goodsid"></param>
        /// <returns></returns>
        public ActionResult ProductDetails(string goodsid)
        {
            ViewData["goodsid"] = goodsid;
            return View();
        }

        [HandlerLogin(1)]
        public ActionResult Cart()
        {
            return View(_cartBus.GetGoodsList(Session["Account"].ToString()));
        }
    }
}