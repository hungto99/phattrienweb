using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamReg_0._0.DataRepository;
using ExamReg_0._0.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamReg_0._0.Areas.Student.Controllers
{
    [Area("student")]
    [Route("student/home")]
    public class HomeController : Controller
    {
        TestExamRepository ter = new TestExamRepository();


        /*
          *Trả về trang chủ khi sinh viên đăng ký xong
        */
        [Route("index")]
        public IActionResult Index()
        {
            var x = HttpContext.Session.GetString("StudentId");
            if (x == null)
            {
                Response.Redirect("https://localhost:44302/Main/Login");
            }
            return View();
        }


        /*
          *Trả về trang đăng ký môn
        */
        [Route("register")]
        public IActionResult Register()
        {
            var studentid = HttpContext.Session.GetString("StudentId");
            if (studentid == null)
            {
                return Redirect("https://localhost:44302/Main/Login");
            }
            else
            {
                string out_mess = "";
                var exams = new MainRepository();
                List<ExamActive> i = exams.GetExamsActive(out out_mess);
                List<AllListTest> alt = ter.GetAllTestExamStudentSubject(Convert.ToInt32(studentid), i[0].Examid, out out_mess);
                ViewBag.Message = out_mess;
                return View(alt);
            }
        }


        /*
          *In danh sách đăng ký môn
        */
        [Route("print")]
        public IActionResult Print()
        {
            return View();
        }


        /*
          *Đăng ký vào một ca thi
        */
        [Route("getjoinexam")]
        [HttpPost]
        public AjaxResult GetJoinExam([FromBody]List<ExamCheck> listcaseId)
        {
            AjaxResult ajaxResult = new AjaxResult();
            var studentid = HttpContext.Session.GetString("StudentId");
            if (studentid == null)
            {
                Response.Redirect("https://localhost:44302/Main/Login");
                return null;
            }
            else
            {
                for (int i = 0; i < listcaseId.Count; i++)
                {
                    for (int j = 0; j < listcaseId.Count; j++)
                    {
                        if ((listcaseId[i].SubjectName == listcaseId[j].SubjectName || listcaseId[i].SubjectId == listcaseId[j].SubjectId || listcaseId[i].CaseTestId == listcaseId[j].CaseTestId) && i != j)
                        {
                            ajaxResult.Message = "Danh sách môn đăng kí không hợp lệ";
                            return ajaxResult;
                        }
                        if (listcaseId[i].examOrder == listcaseId[j].examOrder && listcaseId[i].time == listcaseId[j].time && i != j)
                        {
                            ajaxResult.Message = "Trùng lịch thi";
                            return ajaxResult;
                        }
                    }
                }
                string out_mess = "";
                var exams = new MainRepository();
                List<ExamActive> exam = exams.GetExamsActive(out out_mess);
                List<AllListTest> alt = ter.GetAllTestExamStudentSubject(Convert.ToInt32(studentid), exam[0].Examid, out out_mess);
                ter.conn.Close();
                List<int> listchoice = new List<int>();
                List<int> listsubjectid = new List<int>();
                List<int> listbeforecasetestid = new List<int>();
                foreach (ExamCheck ec in listcaseId)
                {

                    foreach (AllListTest at in alt)
                    {
                        if (ec.SubjectName == at.SubjectName && ec.SubjectId == at.SubjectId && ec.CaseTestId == at.CaseTestId)
                        {
                            if (at.ComputerCount < at.ComputerNumber)
                            {
                                listchoice.Add(ec.CaseTestId);
                                listsubjectid.Add(ec.SubjectId);
                                listbeforecasetestid.Add(at.CaseTestIdIsJoin);
                            }
                        }
                    }
                }
                foreach (AllListTest at in alt)
                {
                    bool check = true;
                    if (at.CaseTestId == at.CaseTestIdIsJoin)
                    {
                        foreach (ExamCheck ec in listcaseId)
                        {
                            if (ec.CaseTestId == at.CaseTestId)
                            {
                                check = false;
                                break;
                            }
                        }
                        if (check)
                        {
                            listchoice.Add(0);
                            listsubjectid.Add(at.SubjectId);
                            listbeforecasetestid.Add(at.CaseTestId);
                        }
                    }

                }
                int count = 0;
                for (int i = 0; i < listchoice.Count; i++)
                {
                    if (listbeforecasetestid[i] != listchoice[i])
                    {
                        ter.JoinInTestExam(Convert.ToInt32(studentid), listsubjectid[i], listbeforecasetestid[i], listchoice[i], out out_mess);
                        ter.conn.Close();
                        if (out_mess == "0")
                        {
                            count++;
                        }
                    }
                }
                string mess = String.Format("Thêm thành công {0} ca thi", count);
                ajaxResult.Message = mess;
                return ajaxResult;
            }
        }


        /*
          *Lấy thông tin của sinh viên
        */
        [Route("getstudentinfo")]
        public AjaxResult GetStudentInfo()
        {
            var x = HttpContext.Session.GetString("StudentId");
            if (x == null)
            {
                Response.Redirect("https://localhost:44302/Main/Login");
                return null;
            }
            AjaxResult ajaxResult = new AjaxResult();
            ajaxResult.Data = ter.GetStudentInfo(Convert.ToInt32(x));
            return ajaxResult;
        }
    }
}