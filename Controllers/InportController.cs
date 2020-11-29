using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using WebBookManagement.Models;
using WebBookManagement.BLL;
using WebBookManagement.Filter;

namespace WebBookManagement.Controllers
{
    [LoginFilterAttribute]
    [RoleFilterAttribute]
    public class InportController : Controller
    {
        //数据库实例
        BookEntities1 db = new BookEntities1();

        //GET: Inport
        public ActionResult Index()
        {

            return View();
        }

        //POST: InportAdd
        public ActionResult Add(int providerid, int number , decimal inportprice, int goodsid, string paytype, string operateperson)
        {
            Object result;
            if (1 == InportManage.AddInport(providerid, number, inportprice, goodsid, paytype, operateperson))
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
        public ActionResult ProviderIdList()
        {
            //供应商集合
            var list = InportManage.SelectProviderIdList();
            return Json(list, JsonRequestBehavior.AllowGet); //JsonRequestBehavior.AllowGet 没有这个是是否返回前台数据的
           
        }

        [HttpGet]
        public ActionResult GoodsIdList(int id)
        {
            //商品集合
            var list = InportManage.SelectGoodsIdList(id);
            return Json(list, JsonRequestBehavior.AllowGet); //JsonRequestBehavior.AllowGet 没有这个是是否返回前台数据的
            
        }
        
        
        //POST: InportUpdate
        public ActionResult Update(int id, int number, decimal inportprice, string paytype, string operateperson)
        {
            Object result;
            if (InportManage.UpdateInport(id, number,inportprice,paytype,operateperson))
            {
                result = new { state = 1, info = "修改成功" };
            }
            else
            {
                result = new { state = 0, info = "供应商已存在" };
            }
            return Json(result);

        }
        
        //GET: InportList
        [HttpGet]
        public ActionResult List(int number)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Inport> list = db.Inport.Where<Inport>(u => true).ToList();
            return Json(list, JsonRequestBehavior.AllowGet); //JsonRequestBehavior.AllowGet 没有这个是是否返回前台数据的


        }

        //GET: InportInfo
        public ActionResult Info(int id)
        {
            var info = InportManage.GetInportById(id);
            return Json(info, JsonRequestBehavior.AllowGet); //JsonRequestBehavior.AllowGet 没有这个是是否返回前台数据的
        }

       
        //POST: InportSearch
        public ActionResult Search(int goodsid, string starttime, string endtime)
        {
            //db.Configuration.ProxyCreationEnabled = false;


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
                result = InportManage.GetListPaging(pageSize, pageIndex);

            }
            else {
                DateTime start = DateTime.Parse(starttime);
                DateTime end = DateTime.Parse(endtime);
                result = InportManage.GetListByGoodsIdPaging(goodsid, pageSize, pageIndex, start, end);
            }
            return Json(result);
        }
        
    }
}