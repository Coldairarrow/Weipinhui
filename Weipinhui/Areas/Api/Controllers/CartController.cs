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
    public class CartController : BaseController
    {
        CartBusiness _cartBus = new CartBusiness();

        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="goodsid">商品Id</param>
        /// <param name="num">数量</param>
        /// <param name="color">颜色</param>
        /// <param name="size">尺寸</param>
        /// <returns></returns>
        [HandlerLogin(1)]
        public ActionResult AddGoods(string goodsid, string num, string color, string size)
        {
            string account = Session["Account"].ToString();
            return Content(_cartBus.AddGoods(account, goodsid, num, color, size));
        }

        /// <summary>
        /// 获取购物车商品列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetGoodsList()
        {
            return Content(_cartBus.GetGoodsListJson(Session["Account"].ToString()));
        }

        /// <summary>
        /// 改变购物车中商品数量
        /// </summary>
        /// <param name="goodsid">商品Id</param>
        /// <param name="method">操作方法，增加或减少</param>
        /// <returns></returns>
        public ActionResult ChangeNumOfGoods(string goodsid, string method)
        {
            return Content(_cartBus.ChangeNumOfGoods(Session["Account"].ToString(), goodsid, method));
        }

        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="id">商品Id</param>
        /// <returns></returns>
        public ActionResult DeleteGoods(string id)
        {
            return Content(_cartBus.DeleteGoods(id));
        }
    }
}