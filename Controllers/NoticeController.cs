﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookManagement.Models;
using WebBookManagement.BLL;
using WebBookManagement.Filter;

namespace WebBookManagement.Controllers
{
    public class NoticeController : Controller
    {
        //数据库上下文
        BookEntities1 db = new BookEntities1();

        // GET: Notice
        public ActionResult Index()
        {
            return View();
        }
        //POST: NoticeAdd
        public ActionResult Add(Notice dataNotice)
        {
            Object result;
            int state = NoticeManage.AddNotice(dataNotice);
            switch (state)
            {
                case 1:
                    result = new { state = 1, info = "添加成功" };
                    break;
                case 0:
                    result = new { state = 0, info = "添加失败" };
                    break;
                default:
                    result = new { state = 0, info = "公告已存在" };
                    break;
            }
            return Json(result);

        }

        //POST: NoticeUpdate
        public ActionResult Update(int noticeid, Notice dataNotice)
        {
            Object result;
            if (NoticeManage.UpdateNotice(noticeid, dataNotice))
            {
                result = new { state = 1, info = "修改成功" };
            }
            else
            {
                result = new { state = 0, info = "公告已存在" };
            }
            return Json(result);
        }
        //GET: NoticeList
        [HttpGet]
        public ActionResult List(int number)
        {
            List<Notice> list = db.Notice.Where<Notice>(u => true).ToList();
            return Json(list, JsonRequestBehavior.AllowGet); //JsonRequestBehavior.AllowGet 没有这个是是否返回前台数据的
        }

        //GET: NoticeInfo
        public ActionResult Info(int id)
        {
            //查找单个人的信息
            Notice NoticeInfo = NoticeManage.GetNoticeById(id);
            return Json(NoticeInfo, JsonRequestBehavior.AllowGet); //JsonRequestBehavior.AllowGet 没有这个是是否返回前台数据的
        }

        //POST: Cusomerdelete Delete()
        public ActionResult Delete(int id)
        {

            Object result;
            if (NoticeManage.DeleteNoticeById(id))
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
                //公告名
                case 0:
                    result = NoticeManage.GetListByNamePaging(value, pageSize, pageIndex);
                    break;
                //如果表达式的值和以上的case后面的值都没有匹配上，那么就执行这里的代码。
                default:
                    result = NoticeManage.GetListPaging(pageSize, pageIndex);
                    break;
            }

            //定义对象 返回结果
            return Json(result);
        }
    }
}