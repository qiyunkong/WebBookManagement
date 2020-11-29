using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookManagement.Models;
using WebBookManagement.BLL;
using WebBookManagement.Filter;

namespace WebBookManagement.Controllers
{
    [LoginFilterAttribute]
    [RoleFilterAttribute]
    public class RoleController : Controller
    {
       //数据库上下文
        BookEntities1 db = new BookEntities1();

        //GET: Role
        public ActionResult Index()
        {
            return View();
        }

        //POST: RoleAdd
        public ActionResult Add(Role dataRole)
        {
            Object result;
            int state = RoleManage.AddRole(dataRole);
            switch (state)
            {
                case 1:
                    result = new { state = 1, info = "添加成功" };
                    break;
                case 0:
                    result = new { state = 0, info = "添加失败" };
                    break;
                default:
                    result = new { state = 0, info = "用户已存在" };
                    break;
            }
            return Json(result);

        }

        //POST: RoleUpdate
        public ActionResult Update(int id, Role dataRole)
        {
            Object result;
            if (RoleManage.UpdateRole(id, dataRole))
            {
                result = new { state = 1, info = "修改成功" };
            }
            else
            {
                result = new { state = 0, info = "用户已存在" };
            }
            return Json(result);
        }
        
        //POST: Url
        public ActionResult UpdateUrl(Role dataRole)
        {
            Object result;
            if (RoleManage.UpdateRole(dataRole))
            {
                result = new { state = 1, info = "修改成功" };
            }
            else
            {
                result = new { state = 0, info = "失败成功" };
            }
            return Json(result);
        }

        //GET: RoleList
        [HttpGet]
        public ActionResult RoleIdList()
        {
            var list = RoleManage.GetRoleIdList();
            return Json(list, JsonRequestBehavior.AllowGet); //JsonRequestBehavior.AllowGet 没有这个是是否返回前台数据的


        }

        //GET: RoleInfo
        public ActionResult Info(int id)
        {
            //查找单个人的信息
            Role RoleInfo = RoleManage.GetRoleById(id);
            return Json(RoleInfo, JsonRequestBehavior.AllowGet); //JsonRequestBehavior.AllowGet 没有这个是是否返回前台数据的
        }

        //POST: Cusomerdelete Delete()
        public ActionResult Delete(int id)
        {

            Object result;
            if (RoleManage.DeleteRoleById(id))
            {
                result = new { state = 1, info = "删除成功" };
            }
            else
            {
                result = new { state = 0, info = "删除失败" };
            }
            return Json(result);


        }

        //POST: CusomerSearch
        public ActionResult Search(int option, string value)
        {

            Object result;
            //当前分页(就是第几页)
            int pageIndex;
            //一页，多少条，
            int pageSize = 12;
            //索引必须是int型，索引无值就按赋值1    
            if (!int.TryParse(Request["pageIndex"], out pageIndex))
            {
                pageIndex = 1;
            }
            switch (option)
            {
                //用户名
                case 0:
                    result = RoleManage.GetListByNamePaging(value, pageSize, pageIndex);
                    break;
                //如果表达式的值和以上的case后面的值都没有匹配上，那么就执行这里的代码。
                default:
                    result = RoleManage.GetListPaging(pageSize, pageIndex);
                    break;
            }

            //定义对象 返回结果
            return Json(result);
        }
    }
}