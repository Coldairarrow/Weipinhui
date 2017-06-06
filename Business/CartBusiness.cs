using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Util;
using System.Web;
namespace Business
{
    public class CartBusiness : BaseBusiness<Cart>
    {
        /// <summary>
        /// 添加商品到购物车
        /// </summary>
        /// <param name="account"></param>
        /// <param name="goodsid"></param>
        /// <param name="numStr"></param>
        /// <param name="color"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public string AddGoods(string account, string goodsid, string numStr, string color, string size)
        {
            if (GetTable().Where(x => x.GoodsId == goodsid && x.Account == account).Count() == 0)
            {
                int num = numStr == null ? 0 : numStr.ToInt();
                Cart newCart = new Cart();
                newCart.Id = Guid.NewGuid().ToString();
                newCart.Account = account;
                newCart.GoodsId = goodsid;
                newCart.Number = num;
                newCart.Color = color;
                newCart.Size = size;

                Insert(newCart);

                var res = new
                {
                    State = 1,
                    Msg = "添加成功！"
                };
                return res.ToJson();
            }
            else
            {
                var res = new
                {
                    State = 0,
                    Msg = "购物车中商品已经存在！"
                };
                return res.ToJson();
            }
        }

        /// <summary>
        /// 获取购物车所有商品Json数据
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public string GetGoodsListJson(string account)
        {

            if (GetTable().Where(x => x.Account == account).Count() == 0)
            {
                var res = new
                {
                    State = -1,
                    Msg = "无商品！"
                };
                return res.ToJson();
            }
            else
            {
                var q = from a in Service.GetTable<Cart>()
                        join b in Service.GetTable<Goods>() on a.GoodsId equals b.Id
                        where a.Account == account
                        select new
                        {
                            Id = a.Id,
                            GoodsId = a.GoodsId,
                            Name = b.Name,
                            Color = a.Color,
                            Size = a.Size,
                            Price = b.Price,
                            OldPrice = b.OldPrice,
                            Number = a.Number
                        };
                var res = new
                {
                    State = 1,
                    Msg = "获取成功！",
                    GoodsList = q.ToList()
                };

                return res.ToJson();
            }
        }

        /// <summary>
        /// 获取购物车所有商品Object类型
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public List<GoodsList> GetGoodsList(string account)
        {
            List<GoodsList> resList = new List<GoodsList>();
            var q = from a in Service.GetTable<Cart>()
                    join b in Service.GetTable<Goods>() on a.GoodsId equals b.Id
                    join c in Service.GetTable<GoodsImg>() on a.GoodsId equals c.GoodsId
                    where a.Account == account
                    select new
                    {
                        Id = a.Id,
                        GoodsId = a.GoodsId,
                        Name = b.Name,
                        Color = a.Color,
                        Size = a.Size,
                        Price = b.Price,
                        OldPrice = b.OldPrice,
                        Number = a.Number,
                        Img=c.Big1
                    };
            foreach(var item in q.ToList())
            {
                GoodsList aGoods = new GoodsList();
                aGoods.Id = item.Id;
                aGoods.GoodsId = item.GoodsId;
                aGoods.Name = item.Name;
                aGoods.Color = item.Color;
                aGoods.Size = item.Size;
                aGoods.Price = (double)item.Price;
                aGoods.OldPrice = (double)item.OldPrice;
                aGoods.Number = (int)item.Number;
                aGoods.Img = item.Img;

                resList.Add(aGoods);
            }
            return resList;
        }


        /// <summary>
        /// 改变购物车物品数量
        /// </summary>
        /// <param name="account"></param>
        /// <param name="goodsId"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public string ChangeNumOfGoods(string account, string goodsId, string method)
        {
            switch (method)
            {
                case "add":
                    {
                        Cart theCart = GetTable().Where(x => x.Account == account && x.GoodsId == goodsId).First();
                        theCart.Number++;
                        Update(theCart);

                        var res = new
                        {
                            State = 1,
                            Msg = "增加成功！"
                        };
                        return res.ToJson();
                    }

                case "red":
                    {
                        Cart theCart = GetTable().Where(x => x.Account == account && x.GoodsId == goodsId).First();
                        theCart.Number--;
                        Update(theCart);

                        var res = new
                        {
                            State = 1,
                            Msg = "减少成功！"
                        };
                        return res.ToJson();
                    }
                default:
                    {
                        var res = new
                        {
                            State = -1,
                            Msg = "操作失败！"
                        };
                        return res.ToJson();
                    }
            }
        }

        /// <summary>
        /// 删除物品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DeleteGoods(string id)
        {
            Cart theCart = GetTable().Where(x => x.Id == id).First();
            Delete(theCart);

            var res = new
            {
                State = 1,
                Msg = "删除成功！"
            };

            return res.ToJson();
        }
    }

    public class GoodsList
    {
        public string Id { get; set; }
        public string GoodsId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public double Price { get; set; }
        public double OldPrice { get; set; }
        public int Number { get; set; }
        public string Img { get; set; }
    }
}
