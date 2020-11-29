using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBookManagement.Models;
using System.Data;

namespace WebBookManagement.DAL
{
    /// <summary>
    /// UserServices    用户SQL服务
    /// </summary>
    public class UserServices
    {
        public UserServices()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        ///   对用户表进行查询  
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>返回查询结果数据表User</returns>
        public static User SelectUserByUserName(string username, string password)
        {
            //数据库实例
            using (BookEntities1 db = new BookEntities1()) {
              //根据用户传来来的username 查询符合条件的user对象并返回
              User user =   db.User.SingleOrDefault(u => u.username == username);
               return user;
            };

        }
        /// <summary>
        ///   对用户表进行查询  
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>返回查询结果数据表User</returns>
        public static User SelectUserByUserName(string username)
        {
            //数据库实例
            using (BookEntities1 db = new BookEntities1())
            {
                //根据用户传来来的username 查询符合条件的user对象并返回
                User user = db.User.Where<User>(u => u.username == username).FirstOrDefault();
                return user;
            };

        }
        /// <summary>
        ///   对用户表进行查询  
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>返回查询结果数据表User</returns>
        public static object GetUserByUserName(string username)
        {
            //数据库实例
            using (BookEntities1 db = new BookEntities1())
            {
                //根据用户传来来的username 查询符合条件的user对象并返回
                var user = db.User.Where(u => u.username == username).Select(u=> new {u.name,u.Role.url}).FirstOrDefault();
                return user;
            };

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static User GetUserById(int id)
        {
            //数据库实例
            using (BookEntities1 db = new BookEntities1())
            {
                //根据用户传来来的username 查询符合条件的user对象并返回
                User user = db.User.SingleOrDefault(u => u.id == id);
                return user;
            };
        }
        /// <summary>
        ///  对用户表进行添加 
        /// </summary>
        /// <param name="dataUser">新增加的用户对象</param>
        /// <returns></returns>
        public static int AddUser(User dataUser)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                //向数据库添加信息
                db.Entry(dataUser).State = EntityState.Added;
                db.User.Add(dataUser);
                return db.SaveChanges();
            };
        }

        /// <summary>
        ///  对客户表进行更新
        /// </summary>
        /// <param name="dataUser">更新的客户对象</param>
        /// <returns>返回查询结果数据表customer</returns>
        public static bool UpdateUser(int id,User dataUser)
        {
            bool result;
            //数据库实例
            using (BookEntities1 db = new BookEntities1())
            {
                try
                {
                    User user =  GetUserById(id);
                    user.password = dataUser.password;
                    user.name = dataUser.name;
                    user.username = dataUser.username;
                    user.roleid = dataUser.roleid;
                    db.Entry(user).State = EntityState.Modified;
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
        public static int GetUserCount()
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                return db.User.Where<User>(u => u.username != "admin").Count();
            };
        }
        /// <summary>
        ///  对进货表所有进行查询
        /// </summary>
        /// <param name="Goodsperson"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static object GetUserList(int pageIndex, int pageSize)
        {

            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                var list = db.User.Where<User>(u => u.username != "admin")
                .OrderBy<User, int>(u => u.id)
                .Skip<User>((pageIndex - 1) * pageSize) //跳过多少条
                .Take<User>(pageSize).Select(u=>new {
                    u.id,
                    u.name,
                    u.Role.rolename,
                    u.username,
                    u.usertime,
                    u.password
                }).ToList(); //截下取多少条
                return list;
            };
        }

        /// <summary>
        /// 对进货表的商品id进行查询统计
        /// </summary>
        /// <returns></returns>
        public static int GetUserCountByName(string name)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                return db.User.Where<User>(u => u.username != "admin"&& u.name.Contains(name)).Count();
            };
        }
        /// <summary>
        ///  对进货表的商品id进行查询
        /// </summary>
        /// <param name="Goodsperson"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static object GetUserListByName(string name, int pageIndex, int pageSize)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                var list = db.User.Where(u => u.username != "admin" && u.name.Contains(name))
                   .OrderBy<User, int>(u => u.id)
                   .Skip<User>((pageIndex - 1) * pageSize) //跳过多少条
                   .Take<User>(pageSize).Select(u => new {
                       u.id,
                       u.name,
                       u.Role.rolename,
                       u.username,
                       u.usertime,
                       u.password
                   }).ToList(); //截下取多少条
                return list;
            };
        }
    }
}