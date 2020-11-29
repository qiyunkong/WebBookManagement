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
    public class CustomerController : Controller
    {
        //数据库实例
        BookEntities1 db = new BookEntities1();

        //GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        //POST: CustomerAdd
        public ActionResult Add(Customer dataCustomer)
        {
           
            Object result;
            int state = CustomerManage.AddCustomer(dataCustomer);
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

        //POST: CustomerUpdate
        public ActionResult Update(int id, Customer dataCustomer)
        {
            Object result;
            if (CustomerManage.UpdateCustomer(id, dataCustomer))
            {
                result = new { state = 1, info = "修改成功" };
            }
            else
            {
                result = new { state = 0, info = "客户已存在" };
            }
            return Json(result);
         }

        //GET: CustomerList
        [HttpGet]
        public ActionResult SelectCustomerId()
        {
            //客户集合
            var list = CustomerManage.SelectCustomerIdList();
            return Json(list, JsonRequestBehavior.AllowGet); //JsonRequestBehavior.AllowGet 没有这个是是否返回前台数据的

        }

        //GET: CustomerInfo
        public ActionResult Info(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            //查找单个人的信息
            Customer CustomerInfo = db.Customer.Where<Customer>(u => u.id == id).FirstOrDefault();
            return Json(CustomerInfo, JsonRequestBehavior.AllowGet); //JsonRequestBehavior.AllowGet 没有这个是是否返回前台数据的
        }

        //POST: Cusomerdelete Delete()
        public ActionResult Delete(int id)
        {

            Object result;
            if (CustomerManage.DeleteCustomerById(id))
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
                //公司名称
                case 0:
                    result = CustomerManage.GetListByCustomerNamePaging(value, pageSize, pageIndex);
                    break;
                //手机号
                case 1:
                    result = CustomerManage.GetListByPhonePaging(value, pageSize, pageIndex);
                    break;
                //联系人
                case 2:
                    result = CustomerManage.GetListByConnectionPersonPaging(value, pageSize, pageIndex);
                    break;
                //如果表达式的值和以上的case后面的值都没有匹配上，那么就执行这里的代码。
                default:
                    result = CustomerManage.GetListByCustomerNamePaging(value, pageSize, pageIndex);
                    break;
            }
          
            //定义对象 返回结果
            return Json(result);
        }
       
    }
}