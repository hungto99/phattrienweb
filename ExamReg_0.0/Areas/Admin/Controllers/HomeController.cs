using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ExamReg_0._0.DataRepository;
using ExamReg_0._0.Models;

namespace ExamReg_0._0.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/home")]
    public class HomeController : Controller
    {


        /*
          * Trả về Trang chọn kì thi sau khi đăng nhập thành công cho admin
        */
        [Route("index")]
        public IActionResult Index()
        {
            var x = HttpContext.Session.GetString("AdminId");
            HttpContext.Session.SetString("ExamsId", "");
            HttpContext.Session.SetString("ExamsName", "");
            HttpContext.Session.SetString("ExamsPeriod", "");
            if (x == null)
            {
                Response.Redirect("https://localhost:44302/Main/Login");
            }
            var exams = new MainRepository();
            string out_mess = "";
            List<Exams> le = exams.GetAllExams(out out_mess);
            return View(le);
        }



        /*
          *Lấy thông tin của kì thi cho admin
        */
        [Route("getinfo")]
        [HttpGet]
        public AjaxResult GetInfoExamPeriod()
        {
            var x = HttpContext.Session.GetString("AdminId");
            if (x == null)
            {
                Response.Redirect("https://localhost:44302/Main/Login");
                return null;
            }
            AjaxResult ajaxResult = new AjaxResult();
            List<string> info = new List<string>();
            var x1 = HttpContext.Session.GetString("ExamsName");
            var x2 = HttpContext.Session.GetString("ExamsPeriod");
            info.Add(x1);
            info.Add(x2);
            ajaxResult.Data = info;
            return ajaxResult;
        }
        [Route("addexamperiod")]


        /*
          *Thêm một kì thi
        */
        public AjaxResult AddExamPeriod([FromBody] List<string> ExamPeriod)
        {
            var z = HttpContext.Session.GetString("AdminId");
            if (z == null)
            {
                Response.Redirect("https://localhost:44302/Main/Login");
                return null;
            }
            var Exam = new MainRepository();
            AjaxResult ajaxResult = new AjaxResult();
            var begin = Convert.ToInt32(ExamPeriod[1]);
            var end = Convert.ToInt32(ExamPeriod[2]);
            if (end - begin > 1 == true || begin < 0 == true || end < 0 == true || begin >= end == true)
            {
                ajaxResult.Message = "3";
                return ajaxResult;
            }
            else
            {
                string out_mess = "";
                Exam.AddExamPeriod(ExamPeriod[0].ToString(), Convert.ToInt32(ExamPeriod[1]), Convert.ToInt32(ExamPeriod[2]), out out_mess);
                if(out_mess == "1")
                {
                    ajaxResult.Message = "1";
                    return ajaxResult;
                }
                else
                {
                    Exam.conn.Close();
                    var x = Exam.GetExamsId(ExamPeriod[0].ToString(), out out_mess);
                    List<string> id = new List<string>();
                    id.Add(x.ToString());
                    id.Add(ExamPeriod[0]);
                    id.Add(ExamPeriod[1].ToString());
                    id.Add(ExamPeriod[2].ToString());
                    ajaxResult.Data = id;
                    ajaxResult.Message = "0";
                    return ajaxResult;
                }
            }
        }


        /*
          *Lấy kì thi đang được tiến hành
        */
        [Route("getexamsactive")]
        public AjaxResult GetExamActive()
        {
            AjaxResult ajaxResult = new AjaxResult();
            var exams = new MainRepository();
            string out_mess = "";
            ajaxResult.Data = exams.GetExamsActive(out out_mess);
            ajaxResult.Message = out_mess;
            return ajaxResult;
        }


        /*
          *Thay đổi kì thi đang được tiến hành
        */
        [Route("updateexamsactive")]
        public AjaxResult UpdateExamActive([FromBody] string examid)
        {
            var x = HttpContext.Session.GetString("AdminId");
            if (x == null)
            {
                Response.Redirect("https://localhost:44302/Main/Login");
                return null;
            }
            string out_mess = "";
            AjaxResult ajaxResult = new AjaxResult();
            var exams = new MainRepository();
            exams.UpdateExamsIsActive(Convert.ToInt32(examid), out out_mess);
            ajaxResult.Message = out_mess;
            return ajaxResult;
        }

    }
}