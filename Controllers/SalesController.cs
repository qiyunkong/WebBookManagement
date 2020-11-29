using System;
using System.Collections.Generic;
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
    public class SalesController : Controller
    {
        //数据库实例
        BookEntities1 db = new BookEntities1();

        //GET: Sales
        public ActionResult Index()
        {
            return View();
        }

        //POST: SalesAdd
        public ActionResult Add(int customerid, int number, decimal saleprice, int goodsid, string paytype, string operateperson)
        {
            Object result;
            if (1 ==SalesManage.AddSales(customerid, number, saleprice, goodsid, paytype, operateperson))
            {
                result = new { state = 1, info = "添加成功" };
            }
            else
            {
                result = new { state = 0, info = "添加失败" };
            }
            return Json(result);

        }


        [HttpGet]
        public ActionResult SelectCustomerId()
        {

            //客户集合
            var list = SalesManage.SelectCustomerIdList();
            return Json(list, JsonRequestBehavior.AllowGet); //JsonRequestBehavior.AllowGet 没有这个是是否返回前台数据的

        }
        [HttpGet]
        public ActionResult SelectGoodsId(int id)
        {
            //商品集合
            var list = SalesManage.SelectGoodsIdList(id);
            return Json(list, JsonRequestBehavior.AllowGet); //JsonRequestBehavior.AllowGet 没有这个是是否返回前台数据的

        }



        //POST: SalesUpdate
        public ActionResult Update(int id, int number, string paytype, string operateperson)
        {
            Object result;
            if (SalesManage.UpdateSales(id, number, paytype, operateperson))
            {
                result = new { state = 1, info = "修改成功" };
            }
            else
            {
                result = new { state = 0, info = "供应商已存在" };
            }
            return Json(result);

        }

        //GET: SalesList
        [HttpGet]
        public ActionResult List(int number)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Sales> list = db.Sales.Where<Sales>(u => true).ToList();
            return Json(list, JsonRequestBehavior.AllowGet); //JsonRequestBehavior.AllowGet 没有这个是是否返回前台数据的


        }

        //GET: SalesInfo
        public ActionResult Info(int id)
        {
            //查找单个人的信息
            var SalesInfo = SalesManage.GetSalesById(id);
            return Json(SalesInfo, JsonRequestBehavior.AllowGet); //JsonRequestBehavior.AllowGet 没有这个是是否返回前台数据的
        }




        //POST: SalesSearch
        public ActionResult Search(int goodsid, string starttime, string endtime)
        {
            //定义list集合 存放查询数据
            object result;
            //当前分页(就是第几页)
            int pageIndex;
            //一页，多少条，
            int pageSize = 12;

            if (!int.TryParse(Request["pageIndex"], out pageIndex))
            {
                pageIndex = 1;
            }

            if (goodsid == -1)
            {
                result = SalesManage.GetListPaging(pageSize, pageIndex);

            }
            else
            {
                DateTime start = DateTime.Parse(starttime);
                DateTime end = DateTime.Parse(endtime);
                result = SalesManage.GetListByGoodsIdPaging(goodsid, pageSize, pageIndex, start, end);
            }
            return Json(result);
        }
    }
}