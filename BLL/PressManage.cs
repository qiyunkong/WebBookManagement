using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using WebBookManagement.Models;
using WebBookManagement.DAL;

namespace WebBookManagement.BLL
{
    /// <summary>
    /// PressManage 客户业务
    /// </summary>
    public class PressManage
    {
        public PressManage()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 实现对客户的验证
        /// </summary>
        /// <param name="prename">客户名称</param>
        /// <returns>返回查询结果数据result</returns>
        public static bool CheckCustomeByPreName(string prename)
        {
            bool result;
            //根据条件获取user对象
            Press press = PressServices.GetPressByPreName(prename);
            //判断客户是否存在
            if (press != null)
            {
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }
        /// <summary>
        /// 实现对客户对象的添加
        /// </summary>
        /// <param name="Press">客户对象</param>
        /// <returns>返回查询结果数据result</returns>
        public static int AddPress(Press press)
        {
            int result;
            if (CheckCustomeByPreName(press.prename))
            {
                if (PressServices.AddPress(press) > 0)
                {
                    result = 1;
                }
                else
                {
                    result = 0;
                }

            }
            else
            {
                result = -1;
            }
            return result;
        }
        /// <summary>
        /// 实现根据客户id查找更新的客户对象
        /// </summary>
        /// <param name="id">客户id</param>
        /// <param name="dataPress">客户对象</param>
        /// <returns>返回查询结果数据result</returns>
        public static bool UpdatePress(int id, Press dataPress)
        {
            bool result;
            //根据条件获取Press对象
            Press press = PressServices.GetPressByPreName(dataPress.prename);

            if (press == null)
            {
                result = PressServices.UpdatePress(id, dataPress);
            }
            else
            {
                if (press.id == id)
                {
                    result = PressServices.UpdatePress(id, dataPress);
                }
                else
                {
                    result = false;
                }

            }
            return result;
        }
        /// <summary>
        /// 实现根据客户id查找删除客户对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回查询结果数据result</returns>
        public static bool DeletePressById(int id)
        {
            try
            {
                Press press = PressServices.GetPressByPressId(id);
                BookEntities1 db = new BookEntities1();
                //将要删除的学生对象状态修改为删除
                db.Entry(press).State = EntityState.Deleted;
                db.Press.Remove(press);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        /// <summary>
        ///  实现根据客户prename查找的分页
        /// </summary>
        /// <param name="value">查询的内容</param>
        /// <param name="rcordCount">总条数</param>
        /// <param name="pageSize">多少条一页</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="pageIndex">当前是第几页</param>
        /// <returns></returns>
        public static object GetListByPreNamePaging(string value, int pageSize, int pageIndex)
        {

            //总的条数 
            int rcordCount;
            //总的页数 
            int pageCount;
            //总条数 Contains方法是模糊查询 sql %内容% 
            rcordCount = PressServices.GetPressCountByPreName(value);
            if (rcordCount > 0)
            {
                //客户列表
                List<Press> list;
                //总的页数 （把总条数除于5就知道能分成几页）Ceiling是向上取整  比如： 10/4 = 3 
                pageCount = Convert.ToInt32(Math.Ceiling((double)rcordCount / pageSize));
                //不能分页到比1更小的页数
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                //不能分页到比总页还大的页数
                pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
                //跳页查询
                list = PressServices.GetPressListByPreName(value, pageIndex, pageSize);
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
        ///  实现根据客户Phone查找的分页
        /// </summary>
        /// <param name="value">查询的内容</param>
        /// <param name="rcordCount">总条数</param>
        /// <param name="pageSize">多少条一页</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="pageIndex">当前是第几页</param>
        /// <returns></returns>
        public static object GetListByPhonePaging(string value, int pageSize, int pageIndex)
        {

            //总的条数 
            int rcordCount;
            //总的页数 
            int pageCount;
            //总条数 Contains方法是模糊查询 sql %内容% 
            rcordCount = PressServices.GetPressCountByPhone(value);
            if (rcordCount > 0)
            {
                //客户列表
                List<Press> list;
                //总的页数 （把总条数除于5就知道能分成几页）Ceiling是向上取整  比如： 10/4 = 3 
                pageCount = Convert.ToInt32(Math.Ceiling((double)rcordCount / pageSize));
                //不能分页到比1更小的页数
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                //不能分页到比总页还大的页数
                pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
                //跳页查询
                list = PressServices.GetPressListByPhone(value, pageIndex, pageSize);
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
        ///  实现根据客户ConnectionPerson查找的分页
        /// </summary>
        /// <param name="value">查询的内容</param>
        /// <param name="rcordCount">总条数</param>
        /// <param name="pageSize">多少条一页</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="pageIndex">当前是第几页</param>
        /// <returns></returns>
        public static object GetListByPrePersonPaging(string value, int pageSize, int pageIndex)
        {

            //总的条数 
            int rcordCount;
            //总的页数 
            int pageCount;
            //总条数 Contains方法是模糊查询 sql %内容% 
            rcordCount = PressServices.GetPressCountByPrePerson(value);
            if (rcordCount > 0)
            {
                //客户列表
                List<Press> list;
                //总的页数 （把总条数除于5就知道能分成几页）Ceiling是向上取整  比如： 10/4 = 3 
                pageCount = Convert.ToInt32(Math.Ceiling((double)rcordCount / pageSize));
                //不能分页到比1更小的页数
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                //不能分页到比总页还大的页数
                pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
                //跳页查询
                list = PressServices.GetPressListByPrePerson(value, pageIndex, pageSize);
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