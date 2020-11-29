using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBookManagement.Models;
using System.Data;

namespace WebBookManagement.DAL
{
    /// <summary>
    ///  NoticeServices SQL 服务
    /// </summary>
    public class NoticeServices
    {
        public NoticeServices()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        ///   对公告表进行id查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Notice GetNoticeById(int id)
        {
            //数据库实例
            using (BookEntities1 db = new BookEntities1())
            {
                //根据公告传来来的Noticename 查询符合条件的Notice对象并返回
                Notice notice = db.Notice.Where<Notice>(u => u.noticeid == id).FirstOrDefault();
                return notice;
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static object GetNoticeByNoticeId(int id)
        {
            //数据库实例
            using (BookEntities1 db = new BookEntities1())
            {
                //根据公告传来来的Noticename 查询符合条件的Notice对象并返回
                var notice = db.Notice.Where(u => u.noticeid == id).Select(u => new {
                    u.noticeid,
                    u.noticename,
                    u.noticetime,
                    u.User.name,
                    u.noticecontent
                }).FirstOrDefault();
                return notice;
            };

        }
        /// <summary>
        ///  对公告表进行添加 
        /// </summary>
        /// <param name="dataNotice">新增加的公告对象</param>
        /// <returns></returns>
        public static int AddNotice(Notice dataNotice)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                //向数据库添加信息
                db.Entry(dataNotice).State = EntityState.Added;
                db.Notice.Add(dataNotice);
                return db.SaveChanges();
            };
        }

        /// <summary>
        ///  对客户表进行更新
        /// </summary>
        /// <param name="dataNotice">更新的客户对象</param>
        /// <returns>返回查询结果数据表customer</returns>
        public static bool UpdateNotice(int id, Notice dataNotice)
        {
            bool result;
            //数据库实例
            using (BookEntities1 db = new BookEntities1())
            {
                try
                {
                    
                    Notice notice = GetNoticeById(id);
                    notice.noticename = dataNotice.noticename;
                    notice.noticecontent = dataNotice.noticecontent;

                    
                    db.Notice.Attach(notice);
                    db.Entry(notice).State = EntityState.Modified;
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
        /// 对进货表所有进行查询统计
        /// </summary>
        /// <returns></returns>
        public static int GetNoticeCount()
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                return db.Notice.Where<Notice>(u => true).Count();
            };
        }
        /// <summary>
        ///  对进货表所有进行查询
        /// </summary>
        /// <param name="Goodsperson"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static object GetNoticeList(int pageIndex, int pageSize)
        {

            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                var list = db.Notice.Where<Notice>(u => true)
                .OrderBy<Notice, int>(u => u.noticeid)
                .Skip<Notice>((pageIndex - 1) * pageSize) //跳过多少条
                .Take<Notice>(pageSize).Select(u => new {
                    u.noticeid,
                    u.noticename,
                    u.noticetime,
                    u.User.name,
                }).ToList(); //截下取多少条
                return list;
            };
        }

        /// <summary>
        /// 对进货表的商品id进行查询统计
        /// </summary>
        /// <returns></returns>
        public static int GetNoticeCountByName(string name)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                return db.Notice.Where<Notice>(u => u.noticename.Contains(name)).Count();
            };
        }
        /// <summary>
        ///  对进货表的商品id进行查询
        /// </summary>
        /// <param name="Goodsperson"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static object GetNoticeListByName(string name, int pageIndex, int pageSize)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                var list = db.Notice.Where(u => u.noticename.Contains(name))
                   .OrderBy<Notice, int>(u => u.noticeid)
                   .Skip<Notice>((pageIndex - 1) * pageSize) //跳过多少条
                   .Take<Notice>(pageSize).Select(u => new {
                       u.noticeid,
                       u.noticename,
                       u.noticetime,
                       u.User.name,
                   }).ToList(); //截下取多少条
                return list;
            };
        }
    }
}