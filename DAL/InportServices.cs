using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBookManagement.Models;
using System.Data;

namespace WebBookManagement.DAL
{
    /// <summary>
    /// InportServices 进货SQL服务
    /// </summary>
    public class InportServices
    {
        public InportServices()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        ///  对进货表进行添加 
        /// </summary>
        /// <param name="dataInport">新增加的进货对象</param>
        /// <returns></returns>
        public static int AddInport(Inport dataInport)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                //向数据库添加信息
                db.Entry(dataInport).State = EntityState.Added;
                db.Inport.Add(dataInport);
                return db.SaveChanges();
            };
        }
      
        /// <summary>
        /// 对进货表id进行查询 
        /// </summary>
        /// <param name="id">进货id</param>
        /// <returns>返回查询结果数据表Inport</returns>
        public static Inport GetInportByInportId(int id)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                //根据用户传来来的username 查询符合条件的user对象并返回
                Inport inport = db.Inport.Where<Inport>(p => p.id == id).FirstOrDefault();
                return inport;
            };
        }
        public static object GetInportById(int id)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                //根据用户传来来的username 查询符合条件的user对象并返回
                var inport = db.Inport.Where<Inport>(u => u.id == id).Select(u => new {
                    u.id,
                    u.Goods.goodsname,
                    u.Provider.providername,
                    u.number,
                    u.inportprice,
                    u.paytype,
                    u.operateperson,
                }).FirstOrDefault();
                return inport;
            };
        }

        /// <summary>
        ///  对进货表进行更新
        /// </summary>
        /// <param name="id">查找的进货对象</param>
        /// <param name="dataInport">更新的进货对象</param>
        /// <returns>返回查询结果数据表Inport</returns>
        public static bool UpdateInport(int id, Inport dataInport)
        {
            bool result;
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                try
                {
                    db.Entry(dataInport).State = EntityState.Modified;
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
        public static object GetInportProviderIdList()
        { 
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                //Distinct() 方法去重
                var list = db.Inport.Select(a => new { a.providerid, a.Provider.providername }).Distinct().ToList();
                return list;
            }
        }
        /// <summary>
        ///  对进货表ProviderId查询
        /// </summary>
        /// <param name="id">供应商id</param>
        /// <returns>返回查询结果数据表list</returns>
        public static object GetInportGoodsIdListByProviderId(int id)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                //Distinct() 方法去重
                var list = db.Inport.Where<Inport>(a => a.providerid == id).Select(u => new { u.Goods.goodsname, u.Goods.id }).Distinct().ToList();
                return list;
            }
        }
        /// <summary>
        /// 对进货表所有进行查询统计
        /// </summary>
        /// <returns></returns>
        public static int GetInportCount()
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                return db.Inport.Where<Inport>(u => true).Count();
            };
        }
        /// <summary>
        ///  对进货表所有进行查询
        /// </summary>
        /// <param name="Goodsperson"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static object GetInportList(int pageIndex, int pageSize)
        {

            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                var list = db.Inport.Where(u => true)
                   .OrderBy<Inport, int>(u => u.id)
                   .Skip<Inport>((pageIndex - 1) * pageSize) //跳过多少条
                   .Take<Inport>(pageSize).Select(u => new {
                       u.id,
                       u.Provider.providername,
                       u.paytype,
                       u.Goods.goodsname,
                       u.inporttime,
                       u.number,
                       u.inportprice,
                       u.operateperson,
                       u.User.name,
                       u.Goods.size,
                       u.Goods.produceplace,
                       u.Provider.providerperson
                   }).ToList(); //截下取多少条
                return list;
            };
        }

        /// <summary>
        /// 对进货表的商品id进行查询统计
        /// </summary>
        /// <returns></returns>
        public static int GetInportCountByGoodsId(int goodsid)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                return db.Inport.Where<Inport>(u => u.goodsid == goodsid).Count();
            };
        }
        /// <summary>
        ///  对进货表的商品id进行查询
        /// </summary>
        /// <param name="Goodsperson"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static object GetInportListByGoodsId(int goodsid, int pageIndex, int pageSize, DateTime start, DateTime end)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                var list = db.Inport.Where(u =>
                u.goodsid == goodsid &&
                (u.inporttime >= start &&
                u.inporttime <= end))
                   .OrderBy<Inport, int>(u => u.id)
                   .Skip<Inport>((pageIndex - 1) * pageSize) //跳过多少条
                   .Take<Inport>(pageSize).Select(u => new {
                       u.id,
                       u.Provider.providername,
                       u.paytype,
                       u.Goods.goodsname,
                       u.inporttime,
                       u.number,
                       u.inportprice,
                       u.operateperson,
                       u.User.name,
                       u.Goods.size,
                       u.Goods.produceplace,
                       u.Provider.providerperson
                   }).ToList(); //截下取多少条
                return list;
            };
        }
    }
}