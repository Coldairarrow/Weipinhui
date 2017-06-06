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
    public class GoodsBusiness : BaseBusiness<Goods>
    {
        /// <summary>
        /// 获取商品列表类形式
        /// </summary>
        /// <param name="code1Str"></param>
        /// <param name="code2Str"></param>
        /// <param name="code3Str"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<Data> GetGoods(string code1Str,string code2Str,string code3Str,string keyWord,string sortMethod)
        {
            List<Data> resList = new List<Data>();
            if(keyWord==null)
            {
                int code1 = code1Str.ToInt();
                int code2 = code2Str.ToInt();
                int code3 = code3Str.ToInt();

                if (code3==0)
                {
                    var q = from a in Service.GetTable<Goods>()
                            join b in Service.GetTable<GoodsImg>() on a.Id equals b.GoodsId
                            join c in Service.GetTable<Category>() on a.CategoryId equals c.Id
                            where c.Code1 == code1 && c.Code2 == code2 && c.Code3 != 0
                            select new
                            {
                                Id = a.Id,
                                Name = a.Name,
                                Price = a.Price,
                                OldPrice = a.OldPrice,
                                Discount = a.Discount,
                                Brand = a.Brand,
                                BigImg1 = b.Big1,
                                BigImg2 = b.Big2,
                                SmallImg1 = b.Small1,
                                SmallImg2 = b.Small2
                            };
                    foreach(var item in q.ToList())
                    {
                        Data aData = new Data();
                        aData.Id = item.Id;
                        aData.Name = item.Name;
                        aData.Price = (double)item.Price;
                        aData.OldPrice = (double)item.OldPrice;
                        aData.Discount = (double)item.Discount;
                        aData.BigImg1 = item.BigImg1;
                        aData.BigImg2 = item.BigImg2;

                        resList.Add(aData);
                    }
                }
                else
                {
                    var q = from a in Service.GetTable<Goods>()
                            join b in Service.GetTable<GoodsImg>() on a.Id equals b.GoodsId
                            join c in Service.GetTable<Category>() on a.CategoryId equals c.Id
                            where c.Code1 == code1 && c.Code2 == code2 && c.Code3==code3
                            select new
                            {
                                Id = a.Id,
                                Name = a.Name,
                                Price = a.Price,
                                OldPrice = a.OldPrice,
                                Discount = a.Discount,
                                Brand = a.Brand,
                                BigImg1 = b.Big1,
                                BigImg2 = b.Big2,
                                SmallImg1 = b.Small1,
                                SmallImg2 = b.Small2
                            };
                    foreach (var item in q.ToList())
                    {
                        Data aData = new Data();
                        aData.Id = item.Id;
                        aData.Name = item.Name;
                        aData.Price = (double)item.Price;
                        aData.OldPrice = (double)item.OldPrice;
                        aData.Discount = (double)item.Discount;
                        aData.BigImg1 = item.BigImg1;
                        aData.BigImg2 = item.BigImg2;

                        resList.Add(aData);
                    }
                }
            }
            else
            {
                var q = from a in Service.GetTable<Goods>()
                        join b in Service.GetTable<GoodsImg>() on a.Id equals b.GoodsId
                        where a.Name.Contains(keyWord)
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Price = a.Price,
                            OldPrice = a.OldPrice,
                            Discount = a.Discount,
                            Brand = a.Brand,
                            BigImg1 = b.Big1,
                            BigImg2 = b.Big2,
                            SmallImg1 = b.Small1,
                            SmallImg2 = b.Small2
                        };
                foreach (var item in q.ToList())
                {
                    Data aData = new Data();
                    aData.Id = item.Id;
                    aData.Name = item.Name;
                    aData.Price = (double)item.Price;
                    aData.OldPrice = (double)item.OldPrice;
                    aData.Discount = (double)item.Discount;
                    aData.BigImg1 = item.BigImg1;
                    aData.BigImg2 = item.BigImg2;

                    resList.Add(aData);
                }
            }

            //排序
            switch(sortMethod)
            {
                case null: return resList;
                case "priceAsc":return resList.OrderBy(x => x.Price).ToList();
                case "priceDesc": return resList.OrderByDescending(x => x.Price).ToList();
                case "discountAsc": return resList.OrderBy(x => x.Discount).ToList();
                case "discountDesc": return resList.OrderByDescending(x => x.Discount).ToList();
                default:return resList;
            }
        }

        /// <summary>
        /// 获取商品列表Json形式
        /// </summary>
        /// <param name="code1Str"></param>
        /// <param name="code2Str"></param>
        /// <param name="code3Str"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public string GetGoodsJson(string code1Str, string code2Str, string code3Str, string keyWord, string sortMethod,string priceStart,string priceEnd)
        {
            List<Data> resList = new List<Data>();
            if (keyWord == null)
            {
                int code1 = code1Str.ToInt();
                int code2 = code2Str.ToInt();
                int code3 = code3Str.ToInt();

                if (code3 == 0)
                {
                    var q = from a in Service.GetTable<Goods>()
                            join b in Service.GetTable<GoodsImg>() on a.Id equals b.GoodsId
                            join c in Service.GetTable<Category>() on a.CategoryId equals c.Id
                            where c.Code1 == code1 && c.Code2 == code2 && c.Code3 != 0
                            select new
                            {
                                Id = a.Id,
                                Name = a.Name,
                                Price = a.Price,
                                OldPrice = a.OldPrice,
                                Discount = a.Discount,
                                Brand = a.Brand,
                                BigImg1 = b.Big1,
                                BigImg2 = b.Big2,
                                SmallImg1 = b.Small1,
                                SmallImg2 = b.Small2
                            };
                    foreach (var item in q.ToList())
                    {
                        Data aData = new Data();
                        aData.Id = item.Id;
                        aData.Name = item.Name;
                        aData.Price = (double)item.Price;
                        aData.OldPrice = (double)item.OldPrice;
                        aData.Discount = (double)item.Discount;
                        aData.BigImg1 = item.BigImg1;
                        aData.BigImg2 = item.BigImg2;

                        resList.Add(aData);
                    }
                }
                else
                {
                    var q = from a in Service.GetTable<Goods>()
                            join b in Service.GetTable<GoodsImg>() on a.Id equals b.GoodsId
                            join c in Service.GetTable<Category>() on a.CategoryId equals c.Id
                            where c.Code1 == code1 && c.Code2 == code2 && c.Code3 == code3
                            select new
                            {
                                Id = a.Id,
                                Name = a.Name,
                                Price = a.Price,
                                OldPrice = a.OldPrice,
                                Discount = a.Discount,
                                Brand = a.Brand,
                                BigImg1 = b.Big1,
                                BigImg2 = b.Big2,
                                SmallImg1 = b.Small1,
                                SmallImg2 = b.Small2
                            };
                    foreach (var item in q.ToList())
                    {
                        Data aData = new Data();
                        aData.Id = item.Id;
                        aData.Name = item.Name;
                        aData.Price = (double)item.Price;
                        aData.OldPrice = (double)item.OldPrice;
                        aData.Discount = (double)item.Discount;
                        aData.BigImg1 = item.BigImg1;
                        aData.BigImg2 = item.BigImg2;

                        resList.Add(aData);
                    }
                }
            }
            else
            {
                var q = from a in Service.GetTable<Goods>()
                        join b in Service.GetTable<GoodsImg>() on a.Id equals b.GoodsId
                        where a.Name.Contains(keyWord)
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Price = a.Price,
                            OldPrice = a.OldPrice,
                            Discount = a.Discount,
                            Brand = a.Brand,
                            BigImg1 = b.Big1,
                            BigImg2 = b.Big2,
                            SmallImg1 = b.Small1,
                            SmallImg2 = b.Small2
                        };
                foreach (var item in q.ToList())
                {
                    Data aData = new Data();
                    aData.Id = item.Id;
                    aData.Name = item.Name;
                    aData.Price = (double)item.Price;
                    aData.OldPrice = (double)item.OldPrice;
                    aData.Discount = (double)item.Discount;
                    aData.BigImg1 = item.BigImg1;
                    aData.BigImg2 = item.BigImg2;

                    resList.Add(aData);
                }
            }

            //价格限制
            if(priceStart!=null&&priceEnd!=null)
            {
                double start = Convert.ToDouble(priceStart);
                double end = Convert.ToDouble(priceEnd);
                resList = resList.Where(x => x.Price >= start && x.Price <= end).ToList();
            }
            //排序
            switch (sortMethod)
            {
                case null:break;
                case "priceAsc": resList=resList.OrderBy(x => x.Price).ToList();break;
                case "priceDesc": resList= resList.OrderByDescending(x => x.Price).ToList();break;
                case "discountAsc": resList= resList.OrderBy(x => x.Discount).ToList();break;
                case "discountDesc": resList= resList.OrderByDescending(x => x.Discount).ToList();break;
                default: break;
            }

            return resList.ToJson();
        }

        /// <summary>
        /// 获取商品的详细详细
        /// </summary>
        /// <param name="goodsid"></param>
        /// <returns></returns>
        public string GetTheGoods(string goodsid)
        {
            var q = from a in Service.GetTable<Goods>()
                    join b in Service.GetTable<GoodsImg>() on a.Id equals b.GoodsId
                    where a.Id==goodsid
                    select new
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Price = a.Price,
                        OldPrice = a.OldPrice,
                        Discount = a.Discount,
                        Brand = a.Brand,
                        BigImg1 = b.Big1,
                        BigImg2 = b.Big2,
                        SmallImg1 = b.Small1,
                        SmallImg2 = b.Small2
                    };
            return q.First().ToJson();
        }
    }

    public class Data
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double OldPrice { get; set; }
        public double Discount { get; set; }
        public string BigImg1 { get; set; }
        public string BigImg2 { get; set; }
        public string SmallImg1 { get; set; }
        public string SmallImg2 { get; set; }
        public string Brand { get; set; }
    }
}
