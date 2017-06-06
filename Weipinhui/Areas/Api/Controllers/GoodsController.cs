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
    public class GoodsController : BaseController
    {
        GoodsBusiness _goodsBus = new GoodsBusiness();

        /// <summary>
        /// 获取所有符合条件的商品并转为Json数据格斯
        /// </summary>
        /// <param name="code1">一级分类</param>
        /// <param name="code2">二级分类</param>
        /// <param name="code3">三级分类</param>
        /// <param name="keyWord">查询关键字</param>
        /// <param name="sortMethod">排序方法</param>
        /// <param name="priceStart">最低价格</param>
        /// <param name="priceEnd">最高价格</param>
        /// <returns></returns>
        public ActionResult GetGoodsJson(string code1, string code2, string code3, string keyWord,string sortMethod,string priceStart,string priceEnd)
        {
            return Content(_goodsBus.GetGoodsJson(code1, code2, code3,keyWord,sortMethod,priceStart,priceEnd));
        }

        /// <summary>
        /// 获取商品详细信息
        /// </summary>
        /// <param name="goodsid">商品主键Id</param>
        /// <returns></returns>
        public ActionResult GetTheGoods(string goodsid)
        {
            return Content(_goodsBus.GetTheGoods(goodsid));
        }
    }
}