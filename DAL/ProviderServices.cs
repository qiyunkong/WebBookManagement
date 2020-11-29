using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBookManagement.Models;
using System.Data;

namespace WebBookManagement.DAL
{
    /// <summary>
    /// ProviderServices 供应商SQL服务
    /// </summary>
    public class ProviderServices
    {
        public ProviderServices()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        ///  对供应商表进行添加 
        /// </summary>
        /// <param name="dataProvider">新增加的供应商对象</param>
        /// <returns></returns>
        public static int AddProvider(Provider dataProvider)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                //向数据库添加信息
                db.Entry(dataProvider).State = EntityState.Added;
                db.Provider.Add(dataProvider);
                return db.SaveChanges();
            };
        }
        /// <summary>
        ///  对供应商表名称进行查询  
        /// </summary>
        /// <param name="providername">供应商名称</param>
        /// <returns>返回查询结果数据表Provider</returns>
        public static Provider GetProviderByProviderName(string providername)
        {
            //数据库实例
            using (BookEntities1 db = new BookEntities1())
            {
                //根据供应商传来的providername 查询符合条件的user对象并返回
                Provider Provider = db.Provider.FirstOrDefault(p => p.providername == providername);
                return Provider;
            };

        }
        /// <summary>
        /// 对供应商表id进行查询 
        /// </summary>
        /// <param name="id">供应商id</param>
        /// <returns>返回查询结果数据表Provider</returns>
        public static Provider GetProviderByProviderId(int id)
        {
            //数据库实例
            using (BookEntities1 db = new BookEntities1())
            {
                //根据用户传来来的username 查询符合条件的user对象并返回
                Provider Provider = db.Provider.Where<Provider>(p => p.id == id).FirstOrDefault();
                return Provider;
            };

        }
        /// <summary>
        ///  对供应商表进行更新
        /// </summary>
        /// <param name="Provider">查找的供应商对象</param>
        /// <param name="dataProvider">更新的供应商对象</param>
        /// <returns>返回查询结果数据表Provider</returns>
        public static bool UpdateProvider(int id, Provider dataProvider)
        {
            bool result;
            //数据库实例
            using (BookEntities1 db = new BookEntities1())
            {
                try
                {
                    db.Entry(dataProvider).State = EntityState.Modified;
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
        ///   对供应商表的供应商名称称进行模糊查询统计
        /// </summary>
        /// <param name="providername">供应商名称</param>
        /// <returns>饭查询结果数据表的总数</returns>
        public static int GetProviderCountByProviderName(string providername)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                return db.Provider.Where<Provider>(p => p.providername.Contains(providername)).Count();
            };
        }
        /// <summary>
        /// 对供应商表的供应商名称进行模糊查询
        /// </summary>
        /// <param name="providername">供应商的供应商名称</param>
        /// <param name="pageIndex">查询的位置</param>
        /// <param name="pageSize">查询的多少</param>
        /// <returns>返回查询结果数据表List<Provider></returns>
        public static List<Provider> GetProviderListByProviderName(string providername, int pageIndex, int pageSize)
        {

            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                List<Provider> list;
                list = db.Provider.Where<Provider>(p => p.providername.Contains(providername))
                   .OrderBy<Provider, int>(u => u.id)
                   .Skip<Provider>((pageIndex - 1) * pageSize) //跳过多少条
                   .Take<Provider>(pageSize).ToList(); //截下取多少条

                return list;
            };
        }
        /// <summary>
        /// 对供应商表的电话称进行模糊查询统计
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static int GetProviderCountByPhone(string phone)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                return db.Provider.Where<Provider>(p => p.phone.Contains(phone)).Count();
            };
        }
        /// <summary>
        /// 对供应商表的电话称进行模糊查询
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static List<Provider> GetProviderListByPhone(string phone, int pageIndex, int pageSize)
        {

            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                List<Provider> list;
                list = db.Provider.Where<Provider>(p => p.phone.Contains(phone))
                   .OrderBy<Provider, int>(u => u.id)
                   .Skip<Provider>((pageIndex - 1) * pageSize) //跳过多少条
                   .Take<Provider>(pageSize).ToList(); //截下取多少条

                return list;
            };
        }
        /// <summary>
        /// 对供应商表的联系人称进行模糊查询统计
        /// </summary>
        /// <param name="providerperson"></param>
        /// <returns></returns>
        public static int GetProviderCountByProviderPerson(string providerperson)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                return db.Provider.Where<Provider>(p => p.providerperson.Contains(providerperson)).Count();
            };
        }
        /// <summary>
        ///  对供应商表的联系人称进行模糊查询
        /// </summary>
        /// <param name="providerperson"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static List<Provider> GetProviderListByProviderPerson(string providerperson, int pageIndex, int pageSize)
        {

            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                List<Provider> list;
                list = db.Provider.Where<Provider>(p => p.providerperson.Contains(providerperson))
                   .OrderBy<Provider, int>(u => u.id)
                   .Skip<Provider>((pageIndex - 1) * pageSize) //跳过多少条
                   .Take<Provider>(pageSize).ToList(); //截下取多少条

                return list;
            };
        }
    }
}