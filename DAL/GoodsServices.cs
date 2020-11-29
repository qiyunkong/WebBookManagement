using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBookManagement.Models;
using System.Data;

namespace WebBookManagement.DAL
{
    /// <summary>
    /// GoodsServices 商品SQL服务
    /// </summary>
    public class GoodsServices
    {
        public GoodsServices()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        ///  对商品表进行添加 
        /// </summary>
        /// <param name="dataGoods">新增加的商品对象</param>
        /// <returns></returns>
        public static int AddGoods(Goods dataGoods)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                //向数据库添加信息
                db.Entry(dataGoods).State = EntityState.Added;
                db.Goods.Add(dataGoods);
                return db.SaveChanges();
            };
        }
        /// <summary>
        ///  对商品表名称进行查询  
        /// </summary>
        /// <param name="goodsname">商品名称</param>
        /// <returns>返回查询结果数据表Goods</returns>
        public static Goods GetGoodsByGoodsName(string goodsname)
        {
            //数据库实例
            using (BookEntities1 db = new BookEntities1())
            {
                //根据商品传来的goodsname 查询符合条件的user对象并返回
                Goods goods = db.Goods.FirstOrDefault(p => p.goodsname == goodsname);
                return goods;
            };

        }
        /// <summary>
        /// 对商品表id进行查询 
        /// </summary>
        /// <param name="id">商品id</param>
        /// <returns>返回查询结果数据表Goods</returns>
        public static Goods GetGoodsByGoodsId(int id)
        {
            //数据库实例
            using (BookEntities1 db = new BookEntities1())
            {
                //根据用户传来来的username 查询符合条件的user对象并返回
                Goods goods = db.Goods.Where<Goods>(p => p.id == id).FirstOrDefault();
                return goods;
            };

        }
        /// <summary>
        ///  对商品表进行更新
        /// </summary>
        /// <param name="Goods">查找的商品对象</param>
        /// <param name="dataGoods">更新的商品对象</param>
        /// <returns>返回查询结果数据表Goods</returns>
        public static bool UpdateGoods(int id, Goods dataGoods)
        {
            bool result;
            //数据库实例
            using (BookEntities1 db = new BookEntities1())
            {
                try
                {
                    db.Entry(dataGoods).State = EntityState.Modified;
                    db.SaveChanges();
                    result = true;
                }
                catch
                {
                    result = false;
                }
            };
            return result;
        }
        /// <summary>
        /// 对进货表ProviderName查询
        /// </summary>
        /// <returns>返回查询结果数据表list</returns>
        public static object GetGoodsProviderIdList()
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                //Distinct() 方法去重
                var list = db.Goods.Select(a => new { a.providerid, a.Provider.providername }).Distinct().ToList();
                return list;
            }
        }
        public static object GetGoodsList()
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                return db.Goods.Select(a => new { a.id, a.goodsname ,a.price }).ToList();
            }
                
        }
        /// <summary>
        ///  对进货表ProviderId查询
        /// </summary>
        /// <param name="id">供应商id</param>
        /// <returns>返回查询结果数据表list</returns>
        public static object GetGoodsGoodsIdListByProviderId(int id)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                //Distinct() 方法去重
                var list = db.Goods.Where<Goods>(a => a.providerid == id).Select(u => new { u.goodsname, u.id }).Distinct().ToList();
                return list;
            }
        }
        /// <summary>
        ///   对商品表的商品名称称进行模糊查询统计
        /// </summary>
        /// <param name="goodsname">商品名称</param>
        /// <returns>饭查询结果数据表的总数</returns>
        public static int GetGoodsCountByGoodsName(string goodsname)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                return db.Goods.Where<Goods>(u => u.goodsname.Contains(goodsname)).Count(); ;
            };
        }
        /// <summary>
        /// 对商品表的商品名称进行模糊查询
        /// </summary>
        /// <param name="goodsname">商品的商品名称</param>
        /// <param name="pageIndex">查询的位置</param>
        /// <param name="pageSize">查询的多少</param>
        /// <returns>返回查询结果数据表List<Goods></returns>
        public static object GetGoodsListByGoodsName(string goodsname, int pageIndex, int pageSize)
        {

            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                //List<Object> list;
               var list = db.Goods.Where(a => a.goodsname.Contains(goodsname))
                           .OrderBy<Goods, int>(a => a.id)
                           .Skip<Goods>((pageIndex - 1) * pageSize) //跳过多少条
                           .Take<Goods>(pageSize)   //截下取多少条;
                           .Select(a => new
                           { //生成新的对象
                                a.id,
                               a.goodsname,
                               a.produceplace,
                               a.size,
                               a.Press.prename,
                               a.price,
                               a.Provider.providername,
                               a.number,
                               a.dangernum,
                           }).ToList();

                return list;
            };
        }
        /// <summary>
        /// 对商品表的电话称进行模糊查询统计
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static int GetGoodsCountByProviderName(string providername)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                return db.Goods.Where<Goods>(u => u.Provider.providername.Contains(providername)).Count();
            };
        }
        /// <summary>
        /// 对商品表的电话称进行模糊查询
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static object GetGoodsListByProviderName(string providername, int pageIndex, int pageSize)
        {

            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
              
              var list = db.Goods.Where(a => a.Provider.providername.Contains(providername))
                            .OrderBy<Goods, int>(a => a.id)
                            .Skip<Goods>((pageIndex - 1) * pageSize) //跳过多少条
                            .Take<Goods>(pageSize)   //截下取多少条;
                            .Select(a => new
                            { //生成新的对象
                                a.id,
                                a.goodsname,
                                a.produceplace,
                                a.size,
                                a.Press.prename,
                                a.price,
                                a.Provider.providername,
                                a.number,
                                a.dangernum,
                            }).ToList();

                return list;
            };
        }
        /// <summary>
        /// 对商品表的联系人称进行模糊查询统计
        /// </summary>
        /// <param name="Goodsperson"></param>
        /// <returns></returns>
        public static int GetGoodsCountByPreName(string prename)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                return db.Goods.Where<Goods>(a => a.Press.prename.Contains(prename)).Count();
            };
        }
        /// <summary>
        ///  对商品表的联系人称进行模糊查询
        /// </summary>
        /// <param name="Goodsperson"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static object GetGoodsListByPreName(string prename, int pageIndex, int pageSize)
        {

            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                
                var list = db.Goods.Where(a => a.Press.prename.Contains(prename))
                           .OrderBy<Goods, int>(a => a.id)
                           .Skip<Goods>((pageIndex - 1) * pageSize) //跳过多少条
                           .Take<Goods>(pageSize)   //截下取多少条;
                           .Select(a => new { //生成新的对象
                                a.id,
                               a.goodsname,
                               a.produceplace,
                               a.size,
                               a.Press.prename,
                               a.price,
                               a.Provider.providername,
                               a.number,
                               a.dangernum,
                           }).ToList();

                return list;
            };
        }
    }
}