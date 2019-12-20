using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamReg_0._0.DataRepository;
using ExamReg_0._0.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ExamReg_0._0.Controllers
{
   
    public class MainController : Controller
    {
        private MainRepository mr = new MainRepository();


        /*
          * Xác nhận phiên đăng nhập của người dùng, kiểm tra trạng thái, kiểm tra là sinh viên hay admin
        */
        public IActionResult LoginAuthencation(User user)
        {
            bool is_admin= false;
            string out_mess = "";
            int a = mr.LoginResult(out out_mess,out is_admin, user.StudentId, user.Password);
            if (a == 2 || a == 1)
            {
                ViewBag.Message = "Sai tên đăng nhập hoặc mật khẩu";
                return View("Login");
            }
          
            if (a == 0|| a== 4)
            {
                if (is_admin)
                {
                    HttpContext.Session.SetString("AdminId", user.StudentId.ToString());

                    return Redirect("/admin/home/index");

                }
                if (!is_admin)
                {
                    HttpContext.Session.SetString("StudentId", user.StudentId.ToString());
                    return Redirect("/student/home/index");
                }
            }
            return View("Login");
        }


        /*
          * Trả về trang đăng nhập, xóa tất cả biến phiên
        */
        public IActionResult Login()
        {
            HttpContext.Session.Clear();
            return View();
        }

        /*
          * Đăng xuất khỏi tài khoản
        */
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear() ;
            return Redirect("https://localhost:44302/Main/Login");
        }
      
    }
}