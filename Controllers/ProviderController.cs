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
    public class ProviderController : Controller
    {
        //数据库实例
        BookEntities1 db = new BookEntities1();

        //GET: Provider
        public ActionResult Index()
        {
            
            return View();
        }

        //POST: ProviderAdd
        public ActionResult Add(Provider dataProvider)
        {
            Object result;
            int state = ProviderManage.AddProvider(dataProvider);
            switch (state)
            {
                case 1:
                    result = new { state = 1, info = "添加成功" };
                    break;
                case 0:
                    result = new { state = 0, info = "添加失败" };
                    break;
                default:
                    result = new { state = 0, info = "供应商已存在" };
                    break;
            }
            return Json(result);

        }

        //POST: ProviderUpdate
        public ActionResult Update(int id, Provider dataProvider)
        {
            Object result;
            if (ProviderManage.UpdateProvider(id, dataProvider))
            {
                result = new { state = 1, info = "修改成功" };
            }
            else
            {
                result = new { state = 0, info = "供应商已存在" };
            }
            return Json(result);

        }

        //GET: ProviderList
        [HttpGet]
        public ActionResult List(int number)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Provider> list = db.Provider.Where<Provider>(u => true).ToList();
            return Json(list, JsonRequestBehavior.AllowGet); //JsonRequestBehavior.AllowGet 没有这个是是否返回前台数据的


        }

        //GET: ProviderInfo
        public ActionResult Info(int id)
        {
            //查找单个人的信息
            Provider ProviderInfo = ProviderManage.GetProviderById(id);
            return Json(ProviderInfo, JsonRequestBehavior.AllowGet); //JsonRequestBehavior.AllowGet 没有这个是是否返回前台数据的
        }

        //POST: Providerdelete Delete()
        public ActionResult Delete(int id)
        {
            Object result;
            if (ProviderManage.DeleteProviderById(id))
            {
                result = new { state = 1, info = "删除成功" };
            }
            else
            {
                result = new { state = 0, info = "删除失败" };
            }
            return Json(result);
        }

        //POST: ProviderSearch
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
                    result = ProviderManage.GetListByProviderNamePaging(value, pageSize, pageIndex);
                    break;
                //电话号
                case 1:
                    result = ProviderManage.GetListByPhonePaging(value, pageSize, pageIndex);
                    break;
                //联系人
                case 2:
                    result = ProviderManage.GetListByProviderPersonPaging(value, pageSize, pageIndex);
                    break;
                //如果表达式的值和以上的case后面的值都没有匹配上，那么就执行这里的代码。
                default:
                    result = ProviderManage.GetListByProviderNamePaging(value, pageSize, pageIndex);
                    break;
            }

            //定义对象 返回结果
            return Json(result);
        }
       
    }
}

