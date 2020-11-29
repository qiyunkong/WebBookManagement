using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBookManagement.Models;
using System.Data;

namespace WebBookManagement.DAL
{
    /// <summary>
    /// SalesServices   销售SQL服务
    /// </summary>
    public class SalesServices
    {
       
        public SalesServices()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        ///  对销售表进行添加 
        /// </summary>
        /// <param name="dataSales">新增加的销售对象</param>
        /// <returns></returns>
        public static int AddSales(Sales dataSales)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                //向数据库添加信息
                db.Entry(dataSales).State = EntityState.Added;
                db.Sales.Add(dataSales);
                return db.SaveChanges();
            };
        }

        /// <summary>
        /// 对销售表id进行查询 
        /// </summary>
        /// <param name="id">销售id</param>
        /// <returns>返回查询结果数据表Sales</returns>
        public static Sales GetSalesBySalesId(int id)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                //根据用户传来来的username 查询符合条件的user对象并返回
                Sales Sales = db.Sales.Where<Sales>(p => p.id == id).FirstOrDefault();
                return Sales;
            };
        }
        public static object GetSalesById(int id)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                //根据用户传来来的username 查询符合条件的user对象并返回
                var Sales = db.Sales.Where<Sales>(u => u.id == id).Select(u => new {
                    u.id,
                    u.Goods.goodsname,
                    u.Customer.customername,
                    u.number,
                    u.saleprice,
                    u.paytype,
                    u.operateperson,
                }).FirstOrDefault();
                return Sales;
            };
        }

        /// <summary>
        ///  对销售表进行更新
        /// </summary>
        /// <param name="id">查找的销售对象</param>
        /// <param name="dataSales">更新的销售对象</param>
        /// <returns>返回查询结果数据表Sales</returns>
        public static bool UpdateSales(int id, Sales dataSales)
        {
            bool result;
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                try
                {
                    db.Entry(dataSales).State = EntityState.Modified;
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
        /// 对销售表ProviderName查询
        /// </summary>
        /// <returns>返回查询结果数据表list</returns>
        public static object GetSalesCustomerIdList()
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                //Distinct() 方法去重
                var list = db.Sales.Select(a => new { a.customerid, a.Customer.customername }).Distinct().ToList();
                return list;
            }
        }
        /// <summary>
        ///  对销售表ProviderId查询
        /// </summary>
        /// <param name="id">供应商id</param>
        /// <returns>返回查询结果数据表list</returns>
        public static object GetSalesGoodsIdListByCustomerId(int id)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                //Distinct() 方法去重
                var list = db.Sales.Where<Sales>(a => a.customerid == id).Select(u => new { u.Goods.goodsname, u.goodsid }).Distinct().ToList();
                return list;
            }
        }
        /// <summary>
        /// 对销售表所有进行查询统计
        /// </summary>
        /// <returns></returns>
        public static int GetSalesCount()
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                return db.Sales.Where<Sales>(u => true).Count();
            };
        }
        /// <summary>
        ///  对销售表所有进行查询
        /// </summary>
        /// <param name="Goodsperson"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static object GetSalesList(int pageIndex, int pageSize)
        {

            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                var list = db.Sales.Where(u => true)
                   .OrderBy<Sales, int>(u => u.id)
                   .Skip<Sales>((pageIndex - 1) * pageSize) //跳过多少条
                   .Take<Sales>(pageSize).Select(u => new {
                       u.id,
                       u.Customer.customername,
                       u.paytype,
                       u.Goods.goodsname,
                       u.salestime,
                       u.number,
                       u.saleprice,
                       u.operateperson,
                       u.User.name,
                       u.Goods.size,
                       u.Goods.produceplace,
                       u.Customer.connectionperson
                   }).ToList(); //截下取多少条
                return list;
            };
        }

        /// <summary>
        /// 对销售表的商品id进行查询统计
        /// </summary>
        /// <returns></returns>
        public static int GetSalesCountByGoodsId(int goodsid)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                return db.Sales.Where<Sales>(u => u.goodsid == goodsid).Count();
            };
        }
        /// <summary>
        ///  对销售表的商品id进行查询
        /// </summary>
        /// <param name="Goodsperson"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static object GetSalesListByGoodsId(int goodsid, int pageIndex, int pageSize, DateTime start, DateTime end)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                var list = db.Sales.Where(u =>
                u.goodsid == goodsid &&
                (u.salestime >= start &&
                u.salestime <= end))
                   .OrderBy<Sales, int>(u => u.id)
                   .Skip<Sales>((pageIndex - 1) * pageSize) //跳过多少条
                   .Take<Sales>(pageSize).Select(u => new {
                       u.id,
                       u.Customer.customername,
                       u.paytype,
                       u.Goods.goodsname,
                       u.salestime,
                       u.number,
                       u.saleprice,
                       u.operateperson,
                       u.User.name,
                       u.Goods.size,
                       u.Goods.produceplace,
                       u.Customer.connectionperson
                   }).ToList(); //截下取多少条
                return list;
            };
        }
    }
}