using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookManagement.Models;
using WebBookManagement.BLL;

namespace WebBookManagement.Controllers
{
    [LoginFilterAttribute]
    public class AdminController : Controller
    {

        //数据库实例
        BookEntities1 db = new BookEntities1();

        // GET: Admin
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Update()
        {
            return View();
        }


        [HttpGet]
        public ActionResult SignOut()
        {
            Session.Remove("user");
            return Redirect("/Home/Index");
        }


        public ActionResult Password(string oldpassword,string newpassword,string newpassword2)
        {
            Object result;
            //操作员的id
            var account = HttpContext.Session["usrName"].ToString();
            User UserInfo = db.User.Where<User>(a => a.username == account).FirstOrDefault();

            if (UserInfo.password == oldpassword)
            {

                if (newpassword == newpassword2)
                {
                    UserInfo.password = newpassword2;
                    db.SaveChanges();

                    result = new { state = 1, info = "成功" };
                }
                else
                {
                    result = new { state = 0, info = "两次密码不一致" };
                }

            }
            else
            {
                result = new { state = 0, info = "旧密码错误" };
            }

            
            return Json(result);
                
        }

        [HttpPost]
        public ActionResult News()
        {
            List<Notice> list = db.Notice.Where<Notice>(u => true).ToList();
            return Json(list);
        }

        public ActionResult News(int id)
        {
             var news = NoticeManage.GetNewsById(id);
            
            ViewData["news"] = news;

            return View();
        }




    }
}