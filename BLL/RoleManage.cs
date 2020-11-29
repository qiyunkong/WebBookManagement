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
    /// RoleManage 角色业务
    /// </summary>
    public class RoleManage
    {
        public RoleManage()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        ///  判断权限
        /// </summary>
        /// <returns></returns>
        public static bool CheckUrl(string currenturl)
        {
            User user =  UserManage.GetCurrentUser();
            int c =  RoleServices.SelectRoleByCurrentUrl((int)user.roleid, currenturl);
            if (c > 0)
            {
                return false;
            }
            else {
                return true;
            }
            
        }
        /// <summary>
        /// 实现对角色的验证
        /// </summary>
        /// <param name="Rolename">角色名</param>
        /// <returns>返回查询结果数据result</returns>
        public static bool CheckRole(string name)
        {
            bool result;
            //根据条件获取Role对象
            Role Role = RoleServices.SelectRoleByRoleName(name);
            //判断角色名是否存在
            if (Role == null)
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
        /// 实现对角色对象的添加
        /// </summary>
        /// <param name="customer">角色对象</param>
        /// <returns>返回查询结果数据result</returns>
        public static int AddRole(Role role)
        {
            int result;
            if (CheckRole(role.rolename))
            {
                role.roletime = DateTime.Now;
                if (RoleServices.AddRole(role) > 0)
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
        /// 实现根据角色id查找更新的角色对象
        /// </summary>
        /// <param name="id">角色id</param>
        /// <param name="dataRole">角色对象</param>
        /// <returns>返回查询结果数据result</returns>
        public static bool UpdateRole(int id, Role dataRole)
        {
            bool result;
            //根据条件获取customer对象
            Role role = RoleServices.SelectRoleByRoleName(dataRole.rolename);

            if (role == null)
            {
                result = RoleServices.UpdateRole(dataRole);
            }
            else
            {
                if (role.id == id)
                {
                    result = RoleServices.UpdateRole(dataRole);
                }
                else
                {
                    result = false;
                }

            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataRole"></param>
        /// <returns></returns>
        public static bool UpdateRole(Role dataRole)
        {
            return RoleServices.UpdateUrl(dataRole);
        }

        /// <summary>
        /// 实现根据角色id查找删除角色对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回查询结果数据result</returns>
        public static bool DeleteRoleById(int id)
        {
            try
            {
                Role role = RoleServices.SelectRoleByRoleId(id);
                BookEntities1 db = new BookEntities1();
                //将要删除的学生对象状态修改为删除
                db.Entry(role).State = EntityState.Deleted;
                db.Role.Remove(role);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 实现根据角色id获取角色对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Role GetRoleById(int id)
        {
            return RoleServices.SelectRoleByRoleId(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static object GetRoleIdList()
        {
            return RoleServices.GetRoleIdList();
        }

        /// <summary>
        ///  实现根据角色查找的分页
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
            rcordCount = RoleServices.GetRoleCount();
            if (rcordCount > 0)
            {
                //总的页数 （把总条数除于5就知道能分成几页）Ceiling是向上取整  比如： 10/4 = 3 
                pageCount = Convert.ToInt32(Math.Ceiling((double)rcordCount / pageSize));
                //不能分页到比1更小的页数
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                //不能分页到比总页还大的页数
                pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
                //跳页查询
                var list = RoleServices.GetRoleList(pageIndex, pageSize);
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
        ///  实现根据角色time查找的分页
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
            rcordCount = RoleServices.GetRoleCountByName(value);
            if (rcordCount > 0)
            {
                //总的页数 （把总条数除于5就知道能分成几页）Ceiling是向上取整  比如： 10/4 = 3 
                pageCount = Convert.ToInt32(Math.Ceiling((double)rcordCount / pageSize));
                //不能分页到比1更小的页数
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                //不能分页到比总页还大的页数
                pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
                //跳页查询
                var list = RoleServices.GetRoleListByName(value, pageIndex, pageSize);
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