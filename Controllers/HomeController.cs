using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookManagement.Models;
using WebBookManagement.BLL;


namespace WebBookManagement.Controllers
{
    /// <summary>
    /// HomeController 控制器
    /// </summary>
    public class HomeController : Controller
    {
        //BookEntities1 db = new BookEntities1();

        // GET: Home
        public ActionResult Index()
        {
            //返回到登录页面
            return View();
        }

        /// <summary>
        ///  用户登录时响应Login方法   
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>返回查询结果数据result对象</returns>
        public ActionResult Login(string username,string password)
        {
            Object result;
            int state = UserManage.CheckUser(username, password);
            switch (state)
            {
                case 1:
                    var u = UserManage.GetCurrentUser(username);
                    result = new {u , state = 1, info = "登陆成功" };
                    break;
                case 0:
                    result = new { state = 0, info = "密码错误" };
                    break;
                default:
                    result = new { state = 0, info = "用户不存在" };
                    break;
            }
            return Json(result); 
        }
    }
}