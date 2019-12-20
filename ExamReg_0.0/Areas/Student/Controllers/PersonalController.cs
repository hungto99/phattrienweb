using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamReg_0._0.DataRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamReg_00.Areas.Student.Controllers
{
    [Area("student")]
    [Route("student/home")]
    public class PersonalController : Controller
    {
        MainRepository mer = new MainRepository();
        [Route("personal-page")]
        public IActionResult Personal()
        {
            return View();
        }
        [Route("old-class")]
        public IActionResult OldClass()
        {
            return View();
        }


        /*
         * Đổi mật khẩu
         */
        [Route("changepass")]
        public IActionResult ChangePassWord(string oldpass, string newpass, string confirmpass)
        {
            var x = HttpContext.Session.GetString("StudentId");
            if (x == null)
            {
               return Redirect("https://localhost:44302/Main/Login");
            }
            if (newpass == confirmpass)
            {
                string out_mess = "";
                mer.ChangePassWord(Convert.ToInt32(x), oldpass, newpass, out out_mess);
                if(out_mess == "2")
                {
                    return Redirect("https://localhost:44302/student/home/personal-page");
                }
                if(out_mess == "1")
                {
                    return Redirect("https://localhost:44302/Main/Login");
                }
            }
            return Redirect("https://localhost:44302/student/home/personal-page");
        }
    }
}