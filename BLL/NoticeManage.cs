using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBookManagement.Models;
using WebBookManagement.DAL;
using System.Data;

namespace WebBookManagement.BLL
{
    /// <summary>
    /// NoticeManage 业务
    /// </summary>
    public class NoticeManage
    {
        public NoticeManage()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }







        /// <summary>
        /// 实现对公告对象的添加
        /// </summary>
        /// <param name="notice">公告对象</param>
        /// <returns>返回查询结果数据result</returns>
        public static int AddNotice(Notice notice)
        {
            int result;
           User user =  (User)UserManage.GetCurrentUser();
            notice.noticetime = DateTime.Now;
            notice.userid = user.id;
            if (NoticeServices.AddNotice(notice) > 0)
            {
                result = 1;
            }
            else
            {
                result = 0;
            }
            return result;
        }
        /// <summary>
        /// 实现根据公告id查找更新的公告对象
        /// </summary>
        /// <param name="id">公告id</param>
        /// <param name="dataNotice">公告对象</param>
        /// <returns>返回查询结果数据result</returns>
        public static bool UpdateNotice(int id, Notice dataNotice)
        {
            bool result;
            //根据条件获取customer对象
            result = NoticeServices.UpdateNotice(id, dataNotice);
            return result;
        }
        /// <summary>
        /// 实现根据公告id查找删除公告对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回查询结果数据result</returns>
        public static bool DeleteNoticeById(int id)
        {
            try
            {
                Notice Notice = NoticeServices.GetNoticeById(id);
                BookEntities1 db = new BookEntities1();
                //将要删除的学生对象状态修改为删除
                db.Entry(Notice).State = EntityState.Deleted;
                db.Notice.Remove(Notice);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 实现根据公告id获取公告对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Notice GetNoticeById(int id)
        {
            return NoticeServices.GetNoticeById(id);
        }
        public static object GetNewsById(int id)
        {
            return NoticeServices.GetNoticeByNoticeId(id);
        }
        /// <summary>
        ///  实现根据公告查找的分页
        /// </summary>
        /// <param name="pageSize">多少条一页</param>
        /// <param name="pageIndex">当前是第几页</param>
        /// <returns></returns>
        public static object GetListPaging(int pageSize, int pageIndex)
        {

            //总的条数 
            int rcordCount;
            //总的页数 
            int pageCount;
            //总条数 Contains方法是模糊查询 sql %内容% 
            rcordCount = NoticeServices.GetNoticeCount();
            if (rcordCount > 0)
            {
                //总的页数 （把总条数除于5就知道能分成几页）Ceiling是向上取整  比如： 10/4 = 3 
                pageCount = Convert.ToInt32(Math.Ceiling((double)rcordCount / pageSize));
                //不能分页到比1更小的页数
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                //不能分页到比总页还大的页数
                pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
                //跳页查询
                var list = NoticeServices.GetNoticeList(pageIndex, pageSize);
                return new { list, pageIndex, pageCount };

            }
            else
            {
                List<int> list = new List<int>();
                pageCount = 0;
                return new { list, pageIndex, pageCount };
            }

        }
        /// <summary>
        ///  实现根据公告time查找的分页
        /// </summary>
        /// <param name="value">查询的内容</param>
        /// <param name="pageSize">多少条一页</param>
        /// <param name="pageIndex">当前是第几页</param>
        /// <returns></returns>
        public static object GetListByNamePaging(string value, int pageSize, int pageIndex)
        {

            //总的条数 
            int rcordCount;
            //总的页数 
            int pageCount;
            //总条数 Contains方法是模糊查询 sql %内容% 
            rcordCount = NoticeServices.GetNoticeCountByName(value);
            if (rcordCount > 0)
            {
                //总的页数 （把总条数除于5就知道能分成几页）Ceiling是向上取整  比如： 10/4 = 3 
                pageCount = Convert.ToInt32(Math.Ceiling((double)rcordCount / pageSize));
                //不能分页到比1更小的页数
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                //不能分页到比总页还大的页数
                pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
                //跳页查询
                var list = NoticeServices.GetNoticeListByName(value, pageIndex, pageSize);
                return new { list, pageIndex, pageCount };

            }
            else
            {
                List<int> list = new List<int>();
                pageCount = 0;
                return new { list, pageIndex, pageCount };
            }
        }
    }
}