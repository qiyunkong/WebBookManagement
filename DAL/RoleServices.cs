using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBookManagement.Models;
using System.Data;

namespace WebBookManagement.DAL
{
    /// <summary>
    ///  RoleServices 校色SQL服务
    /// </summary>
    public class RoleServices
    {
        public RoleServices()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int SelectRoleByCurrentUrl(int id,string url)
        {
            using (BookEntities1 db = new BookEntities1())
            {
              return  db.Role.Where(u => u.id == id && u.url.Contains(url)).Count();
            }
        }
        /// <summary>
        ///   对角色表进行查询  
        /// </summary>
        /// <param name="id">角色id</param>
        /// <returns>返回查询结果数据表Role</returns>
        public static Role SelectRoleByRoleId(int id)
        {
            //数据库实例
            using (BookEntities1 db = new BookEntities1())
            {
                //根据角色传来来的Rolename 查询符合条件的Role对象并返回
                Role Role = db.Role.SingleOrDefault(u => u.id == id);
                return Role;
            };

        }
        /// <summary>
        ///   对角色表进行查询  
        /// </summary>
        /// <param name="Rolename">角色名</param>
        /// <returns>返回查询结果数据表Role</returns>
        public static Role SelectRoleByRoleName(string name)
        {
            //数据库实例
            using (BookEntities1 db = new BookEntities1())
            {
                //根据角色传来来的Rolename 查询符合条件的Role对象并返回
                Role Role = db.Role.Where<Role>(u => u.rolename == name ).FirstOrDefault();
                return Role;
            };

        }
        /// <summary>
        ///  对角色表进行添加 
        /// </summary>
        /// <param name="dataRole">新增加的角色对象</param>
        /// <returns></returns>
        public static int AddRole(Role dataRole)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                //向数据库添加信息
                db.Entry(dataRole).State = EntityState.Added;
                db.Role.Add(dataRole);
                return db.SaveChanges();
            };
        }

        /// <summary>
        ///  对客户表进行更新
        /// </summary>
        /// <param name="dataRole">更新的客户对象</param>
        /// <returns>返回查询结果数据表customer</returns>
        public static bool UpdateRole(Role dataRole)
        {
            bool result;
            //数据库实例
            using (BookEntities1 db = new BookEntities1())
            {
                try
                {
                    Role role =  SelectRoleByRoleId(dataRole.id);
                    role.rolename =  dataRole.rolename;
                    db.Entry(role).State = EntityState.Modified;
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
        public static bool UpdateUrl(Role dataRole)
        {
            bool result;
            //数据库实例
            using (BookEntities1 db = new BookEntities1())
            {
                try
                {
                    Role role = SelectRoleByRoleId(dataRole.id);
                    role.url = dataRole.url;
                    db.Entry(role).State = EntityState.Modified;
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
        /// 
        /// </summary>
        /// <returns></returns>
        public static object GetRoleIdList()
        {

            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                var list = db.Role.Where(u => u.id !=1).Select(u=> new {
                    u.id,
                    u.rolename
                }).ToList(); //截下取多少条
                return list;
            };
        }

        /// <summary>
        /// 对进货表所有进行查询统计
        /// </summary>
        /// <returns></returns>
        public static int GetRoleCount()
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                return db.Role.Where<Role>(u => u.id != 1).Count();
            };
        }
        /// <summary>
        ///  对进货表所有进行查询
        /// </summary>
        /// <param name="Goodsperson"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static object GetRoleList(int pageIndex, int pageSize)
        {

            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                var list = db.Role.Where<Role>(u => u.id != 1)
                .OrderBy<Role, int>(u => u.id)
                .Skip<Role>((pageIndex - 1) * pageSize) //跳过多少条
                .Take<Role>(pageSize).ToList(); //截下取多少条
                return list;
            };
        }

        /// <summary>
        /// 对进货表的商品id进行查询统计
        /// </summary>
        /// <returns></returns>
        public static int GetRoleCountByName(string name)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                return db.Role.Where<Role>(u => u.id != 1&& u.rolename.Contains(name)).Count();
            };
        }
        /// <summary>
        ///  对进货表的商品id进行查询
        /// </summary>
        /// <param name="Goodsperson"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static object GetRoleListByName(string name, int pageIndex, int pageSize)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                var list = db.Role.Where(u => u.id != 1&& u.rolename.Contains(name))
                   .OrderBy<Role, int>(u => u.id)
                   .Skip<Role>((pageIndex - 1) * pageSize) //跳过多少条
                   .Take<Role>(pageSize).ToList(); //截下取多少条
                return list;
            };
        }
    }
}