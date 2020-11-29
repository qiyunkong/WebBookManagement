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
    public class PressController : Controller
    {
        //数据库实例
        BookEntities1 db = new BookEntities1();
        

        //GET: Press
        public ActionResult Index()
        {
            //查询出版社信息所有的信息
            var PressList = db.Press.Where<Press>(u => true); 

             //数据映射到页面上
             ViewData["PressList"] = PressList;
            return View();
        }

        //POST: PressAdd
        public ActionResult Add(Press dataPress)
        {
            Object result;
            int state = PressManage.AddPress(dataPress);
            switch (state)
            {
                case 1:
                    result = new { state = 1, info = "添加成功" };
                    break;
                case 0:
                    result = new { state = 0, info = "添加失败" };
                    break;
                default:
                    result = new { state = 0, info = "客户已存在" };
                    break;
            }
            return Json(result);
        }

        //POST: PressUpdate
        public ActionResult Update(int id, Press dataPress)
        {
            Object result;
            if (PressManage.UpdatePress(id, dataPress))
            {
                result = new { state = 1, info = "修改成功" };
            }
            else
            {
                result = new { state = 0, info = "客户已存在" };
            }
            return Json(result);

        }

        //GET: PressList
        [HttpGet]
        public ActionResult List(int number)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Press> list = db.Press.Where<Press>(u => true).ToList();
            return Json(list, JsonRequestBehavior.AllowGet); //JsonRequestBehavior.AllowGet 没有这个是是否返回前台数据的


        }

        //GET: PressInfo
        public ActionResult Info(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            //查找单个人的信息
            Press PressInfo = db.Press.Where<Press>(u => u.id == id).FirstOrDefault();
            return Json(PressInfo, JsonRequestBehavior.AllowGet); //JsonRequestBehavior.AllowGet 没有这个是是否返回前台数据的
        }

        //POST:  Pressdelete Delete()
        public ActionResult Delete(int id)
        {

            Object result;
            if (PressManage.DeletePressById(id))
            {
                result = new { state = 1, info = "删除成功" };
            }
            else
            {
                result = new { state = 0, info = "删除失败" };
            }
            return Json(result);
        }

        //POST:  PressSearch
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
                //公司名称
                case 0:
                    result = PressManage.GetListByPreNamePaging(value, pageSize, pageIndex);
                    break;
                //手机号
                case 1:
                    result = PressManage.GetListByPhonePaging(value, pageSize, pageIndex);
                    break;
                //联系人
                case 2:
                    result = PressManage.GetListByPrePersonPaging(value, pageSize, pageIndex);
                    break;
                //如果表达式的值和以上的case后面的值都没有匹配上，那么就执行这里的代码。
                default:
                    result = PressManage.GetListByPreNamePaging(value, pageSize, pageIndex);
                    break;
            }

            //定义对象 返回结果
            return Json(result);
        }
       
    }
}