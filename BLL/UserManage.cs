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
    /// UserManage 用户业务
    /// </summary>
    public class UserManage
    {
        public UserManage()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 实现对管理员用户的验证
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>返回查询结果数据result</returns>
        public static int CheckUser(string username,string password)
        {
            int result;
            //根据条件获取user对象
            User user = UserServices.SelectUserByUserName(username, password);
            //判断用户名是否存在
            if (user == null)
            {
                result = -1;
            }
            else
            {
                //判断密码是否正确
                if (user.password == password)
                {
                    result = 1;
                    //将用户名放入session中
                    HttpContext.Current.Session.Timeout = 60;
                    HttpContext.Current.Session["user"] = user;
                }
                else
                {
                    result = 0;
                }
            }
            return result;
        }
        /// <summary>
        /// 实现对管理员用户的验证
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>返回查询结果数据result</returns>
        public static bool CheckUser(string username)
        {
            bool result;
            //根据条件获取user对象
            User user = UserServices.SelectUserByUserName(username);
            //判断用户名是否存在
            if (user == null)
            {
                result = true; 
            }
            else
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        ///  实现获取当前的用户信息   
        /// </summary>
        /// <returns></returns>
        public static User GetCurrentUser()
        {
            User user = (User)HttpContext.Current.Session["user"];
            return user;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static object GetCurrentUser (string username)
        {
            return UserServices.GetUserByUserName(username);
            
        }


        /// <summary>
        /// 实现对用户对象的添加
        /// </summary>
        /// <param name="customer">用户对象</param>
        /// <returns>返回查询结果数据result</returns>
        public static int AddUser(User user)
        {
            int result;
            if (CheckUser(user.username))
            {
                user.usertime = DateTime.Now;
                if (UserServices.AddUser(user) > 0)
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
        /// 实现根据用户id查找更新的用户对象
        /// </summary>
        /// <param name="id">用户id</param>
        /// <param name="dataUser">用户对象</param>
        /// <returns>返回查询结果数据result</returns>
        public static bool UpdateUser(int id,User dataUser)
        {
            bool result;
            //根据条件获取customer对象
            User user = UserServices.SelectUserByUserName(dataUser.username);

            if (user == null)
            {
                result = UserServices.UpdateUser(id, dataUser);
            }
            else
            {
                if (user.id == id)
                {
                    result = UserServices.UpdateUser(id,dataUser);
                }
                else
                {
                    result = false;
                }

            }
            return result;
        }
        /// <summary>
        /// 实现根据用户id查找删除用户对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回查询结果数据result</returns>
        public static bool DeleteUserById(int id)
        {
            try
            {
                User user = UserServices.GetUserById(id);
                BookEntities1 db = new BookEntities1();
                //将要删除的学生对象状态修改为删除
                db.Entry(user).State = EntityState.Deleted;
                db.User.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 实现根据用户id获取用户对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static User GetUserById(int id)
        {
            return UserServices.GetUserById(id);
        }

        /// <summary>
        ///  实现根据用户查找的分页
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
            rcordCount = UserServices.GetUserCount();
            if (rcordCount > 0)
            {
                //总的页数 （把总条数除于5就知道能分成几页）Ceiling是向上取整  比如： 10/4 = 3 
                pageCount = Convert.ToInt32(Math.Ceiling((double)rcordCount / pageSize));
                //不能分页到比1更小的页数
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                //不能分页到比总页还大的页数
                pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
                //跳页查询
                var list = UserServices.GetUserList(pageIndex, pageSize);
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
        ///  实现根据用户time查找的分页
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
            rcordCount = UserServices.GetUserCountByName(value);
            if (rcordCount > 0)
            {
                //总的页数 （把总条数除于5就知道能分成几页）Ceiling是向上取整  比如： 10/4 = 3 
                pageCount = Convert.ToInt32(Math.Ceiling((double)rcordCount / pageSize));
                //不能分页到比1更小的页数
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                //不能分页到比总页还大的页数
                pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
                //跳页查询
                var list = UserServices.GetUserListByName(value, pageIndex, pageSize);
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