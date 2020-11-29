using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookManagement.BLL;
using WebBookManagement.Filter;
using WebBookManagement.Models;

namespace WebBookManagement.Controllers
{
    [LoginFilterAttribute]
    [RoleFilterAttribute]
    public class GoodsController : Controller
    {
        //数据库实例
        BookEntities1 db = new BookEntities1();


        //GET: Goods
        public ActionResult Index()
        {
            return View();
        }

        //POST: GoodsAdd
        public ActionResult Add(Goods dataGoods)
        {
            Object result;
            int state = GoodsManage.AddGoods(dataGoods);
            switch (state)
            {
                case 1:
                    result = new { state = 1, info = "添加成功" };
                    break;
                case 0:
                    result = new { state = 0, info = "添加失败" };
                    break;
                default:
                    result = new { state = 0, info = "商品已存在" };
                    break;
            }
            return Json(result);

        }

        //POST: GoodsUpdate
        public ActionResult Update(int id, Goods dataGoods)
        {
            Object result;
            if (GoodsManage.UpdateGoods(id, dataGoods))
            {
                result = new { state = 1, info = "修改成功" };
            }
            else
            {
                result = new { state = 0, info = "商品已存在" };
            }
            return Json(result);

        }

        //GET: GoodsList
        [HttpGet]
        public ActionResult SelectGoodsId()
        {
            var reslut = GoodsManage.GetGoodsList();
           
            return Json(reslut, JsonRequestBehavior.AllowGet); //JsonRequestBehavior.AllowGet 没有这个是是否返回前台数据的


        }

        //GET: GoodsInfo
        public ActionResult Info(int id)
        {
            //查找单个人的信息
            Goods GoodsInfo = GoodsManage.GetGoodsById(id);
            return Json(GoodsInfo, JsonRequestBehavior.AllowGet); //JsonRequestBehavior.AllowGet 没有这个是是否返回前台数据的
        }

        //POST: Cusomerdelete Delete()
        public ActionResult Delete(int id)
        {

            Object result;
            if (GoodsManage.DeleteGoodsById(id))
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
                //商品名称
                case 0:
                    result = GoodsManage.GetListByGoodsNamePaging(value, pageSize, pageIndex);
                    break;
                //供应商
                case 1:
                    result = GoodsManage.GetListByProviderNamePaging(value, pageSize, pageIndex);
                    break;
                //出版社
                case 2:
                    result = GoodsManage.GetListByPreNamePaging(value, pageSize, pageIndex);
                    break;
                //如果表达式的值和以上的case后面的值都没有匹配上，那么就执行这里的代码。
                default:
                    result = GoodsManage.GetListByGoodsNamePaging(value, pageSize, pageIndex);
                    break;
            }

            //定义对象 返回结果
            return Json(result);
        }


       
        [HttpPost]
        public ActionResult UploadImg()
        {
            HttpPostedFileBase postFile = Request.Files["pic"]; //接受文件数据
            Object result;
            if (postFile == null)
            {
                 result = new { state = 0, info = "请选择上传文件" };
                
            }
            else
            {

                string fileName = Path.GetFileName(postFile.FileName); //文件名称
                string fileExt = Path.GetExtension(fileName); //文件的扩展名称

                if (fileExt == ".jpg")
                {
                    //路径
                    string dir = "/ImagePath/" + DateTime.Now.Year +
                        "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";

                    //创建文件夹 如果存在就不创建
                    Directory.CreateDirectory(Path.GetDirectoryName(Request.MapPath(dir)));

                    //新的文件名
                    string newfileName = Guid.NewGuid().ToString();

                    string fileDir = dir + newfileName + fileExt; //完整的路径

                    postFile.SaveAs(Request.MapPath(fileDir)); //保存文件

                    //定义对象 返回结果
                   
                     result = new { state = 1, info = "上传成功", fileDir };
                   
                   

                }
                else
                {
                     result = new { state = 0, info = "格式不对"};
                   
                }

            }
            return Json(result);



        }


        [HttpGet]
        public ActionResult ProviderIdList()
        {
            //供应商集合
            var list = GoodsManage.SelectProviderIdList();
            return Json(list, JsonRequestBehavior.AllowGet); //JsonRequestBehavior.AllowGet 没有这个是是否返回前台数据的

        }

        [HttpGet]
        public ActionResult GoodsIdList(int id)
        {
            //商品集合
            var list = GoodsManage.SelectGoodsIdList(id);
            return Json(list, JsonRequestBehavior.AllowGet); //JsonRequestBehavior.AllowGet 没有这个是是否返回前台数据的

        }

        [HttpGet]
        public ActionResult Providerid()
        {
            var list = db.Provider.Select(a => new {a.id,a.providername});
            return Json(list, JsonRequestBehavior.AllowGet); //JsonRequestBehavior.AllowGet 没有这个是是否返回前台数据的
           
        }

        [HttpGet]
        public ActionResult Pressid()
        {
            var list = db.Press.Select(a => new { a.id, a.prename });
            return Json(list, JsonRequestBehavior.AllowGet); //JsonRequestBehavior.AllowGet 没有这个是是否返回前台数据的
            
        }



    }
}