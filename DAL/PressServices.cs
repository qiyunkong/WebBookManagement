using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBookManagement.Models;
using System.Data;

namespace WebBookManagement.DAL
{
    /// <summary>
    /// CustomerServices 出版社SQL服务
    /// </summary>
    public class PressServices
    {
        public PressServices()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        ///  对出版社表进行添加 
        /// </summary>
        /// <param name="dataPress">新增加的出版社对象</param>
        /// <returns></returns>
        public static int AddPress(Press dataPress)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                //向数据库添加信息
                db.Entry(dataPress).State = EntityState.Added;
                db.Press.Add(dataPress);
                return db.SaveChanges();
            };
        }
        /// <summary>
        ///  对出版社表名称进行查询  
        /// </summary>
        /// <param name="prename">出版社名称</param>
        /// <returns>返回查询结果数据表Press</returns>
        public static Press GetPressByPreName(string prename)
        {
            //数据库实例
            using (BookEntities1 db = new BookEntities1())
            {
                //根据出版社传来的prename 查询符合条件的user对象并返回
                Press Press = db.Press.FirstOrDefault(p => p.prename == prename);
                return Press;
            };

        }
        /// <summary>
        /// 对出版社表id进行查询 
        /// </summary>
        /// <param name="id">出版社id</param>
        /// <returns>返回查询结果数据表Press</returns>
        public static Press GetPressByPressId(int id)
        {
            //数据库实例
            using (BookEntities1 db = new BookEntities1())
            {
                //根据用户传来来的username 查询符合条件的user对象并返回
                Press Press = db.Press.Where<Press>(p => p.id == id).FirstOrDefault();
                return Press;
            };

        }
        /// <summary>
        ///  对出版社表进行更新
        /// </summary>
        /// <param name="Press">查找的出版社对象</param>
        /// <param name="dataPress">更新的出版社对象</param>
        /// <returns>返回查询结果数据表Press</returns>
        public static bool UpdatePress(int id, Press dataPress)
        {
            bool result;
            //数据库实例
            using (BookEntities1 db = new BookEntities1())
            {
                try
                {
                    db.Entry(dataPress).State = EntityState.Modified;
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
        ///   对出版社表的出版社名称称进行模糊查询统计
        /// </summary>
        /// <param name="prename">出版社名称</param>
        /// <returns>饭查询结果数据表的总数</returns>
        public static int GetPressCountByPreName(string prename)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                return db.Press.Where<Press>(p => p.prename.Contains(prename)).Count();
            };
        }
        /// <summary>
        /// 对出版社表的出版社名称进行模糊查询
        /// </summary>
        /// <param name="prename">出版社的出版社名称</param>
        /// <param name="pageIndex">查询的位置</param>
        /// <param name="pageSize">查询的多少</param>
        /// <returns>返回查询结果数据表List<Press></returns>
        public static List<Press> GetPressListByPreName(string prename, int pageIndex, int pageSize)
        {

            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                List<Press> list;
                list = db.Press.Where<Press>(p => p.prename.Contains(prename))
                   .OrderBy<Press, int>(u => u.id)
                   .Skip<Press>((pageIndex - 1) * pageSize) //跳过多少条
                   .Take<Press>(pageSize).ToList(); //截下取多少条

                return list;
            };
        }
        /// <summary>
        /// 对出版社表的电话称进行模糊查询统计
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static int GetPressCountByPhone(string phone)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                return db.Press.Where<Press>(p => p.phone.Contains(phone)).Count();
            };
        }
        /// <summary>
        /// 对出版社表的电话称进行模糊查询
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static List<Press> GetPressListByPhone(string phone, int pageIndex, int pageSize)
        {

            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                List<Press> list;
                list = db.Press.Where<Press>(p => p.phone.Contains(phone))
                   .OrderBy<Press, int>(u => u.id)
                   .Skip<Press>((pageIndex - 1) * pageSize) //跳过多少条
                   .Take<Press>(pageSize).ToList(); //截下取多少条

                return list;
            };
        }
        /// <summary>
        /// 对出版社表的联系人称进行模糊查询统计
        /// </summary>
        /// <param name="preperson"></param>
        /// <returns></returns>
        public static int GetPressCountByPrePerson(string preperson)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                return db.Press.Where<Press>(p => p.preperson.Contains(preperson)).Count();
            };
        }
        /// <summary>
        ///  对出版社表的联系人称进行模糊查询
        /// </summary>
        /// <param name="preperson"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static List<Press> GetPressListByPrePerson(string preperson, int pageIndex, int pageSize)
        {

            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                List<Press> list;
                list = db.Press.Where<Press>(p => p.preperson.Contains(preperson))
                   .OrderBy<Press, int>(u => u.id)
                   .Skip<Press>((pageIndex - 1) * pageSize) //跳过多少条
                   .Take<Press>(pageSize).ToList(); //截下取多少条

                return list;
            };
        }
    }
}