using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamReg_0._0.DataRepository;
using ExamReg_0._0.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamReg_0._0.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/courses")]
    public class CoursesController : Controller
    {
        ExaminationRepository emr = new ExaminationRepository();
        
        
        
        //Hàm trả về View Danh sách môn học của kì thi đã chọn, và đặt biến phiên cho kì thi
        [Route("subject")]
        public IActionResult Subject(int? examsid, string examsname, string examsbegin, string examsend)
        {
            var x = HttpContext.Session.GetString("AdminId");
            if (x == null)
            {
                Response.Redirect("https://localhost:44302/Main/Login");
                return View();
            }
            else
            {
                if (examsid != null && examsname != null && examsbegin != null && examsend != null)
                {
                    HttpContext.Session.SetString("ExamsName", examsname.ToString());
                    HttpContext.Session.SetString("ExamsPeriod", examsbegin.ToString() + "-" + examsend.ToString());
                    HttpContext.Session.SetString("ExamsId", examsid.ToString());
                    return View();
                }
                else
                {
                    return View();
                }
            }
        }


        //Hàm lấy tất cả kì học
        [Route("examperiod")]
        [HttpGet]
        public AjaxResult GetAllPeriod()
        {
            var x = HttpContext.Session.GetString("AdminId");
            if (x == null)
            {
                Response.Redirect("https://localhost:44302/Main/Login");
                return null;
            }
            string out_mess = "";
            AjaxResult ajaxResult = new AjaxResult();
            ajaxResult.Data = emr.GetExamPeriod(out out_mess);
            ajaxResult.Message = out_mess;
            return ajaxResult;
        }


        /*
          * Lấy tất cả danh sách môn học của kì học hiển thị cho admin
        */
        [Route("getsubject")]
        [HttpPost]
        public AjaxResult GetSubject([FromBody] List<string> e)
        {
            var y = HttpContext.Session.GetString("AdminId");
            if (y == null)
            {
                Response.Redirect("https://localhost:44302/Main/Login");
                return null;
            }
            var z = HttpContext.Session.GetString("ExamsId");
            if (z == null)
            {
                Response.Redirect("https://localhost:44302/admin/home/index");
                return null;
            }
            var x = HttpContext.Session.GetString("ExamsId");
            string out_mess = "";
            AjaxResult ajaxResult = new AjaxResult();
            if (x == null)
            {
                
            }
            else
            {
                ajaxResult.Data = emr.GetExaminations(Convert.ToInt32(x), out out_mess);
            }
            ajaxResult.Message = out_mess;
            return ajaxResult;
        }


        /*
          * Thêm môn học cho kì học admin
        */
        [Route("addsubject")]
        [HttpPost]
        public AjaxResult AddSubject([FromBody] List<string> subjectname)
        {
            string out_mess="";
            AjaxResult ajaxResult = new AjaxResult();
            var y = HttpContext.Session.GetString("AdminId");
            if (y == null)
            {
                Response.Redirect("https://localhost:44302/Main/Login");
                return null;
            }
            var x = HttpContext.Session.GetString("ExamsId");
            if (x == null)
            {
                Response.Redirect("https://localhost:44302/admin/home/index");
                return null;
            }
                emr.AddSubject(subjectname[0], Convert.ToInt32(x), out out_mess);
            

            ajaxResult.Message = out_mess;
            return ajaxResult;
        }


        /*
          * Lấy danh sách học sinh của môn học
        */
        [Route("getlistclass/{id}")]
        [HttpGet]
        public IActionResult GetListStudent(string id)
        {
            var y = HttpContext.Session.GetString("AdminId");
            if (y == null)
            {
                return Redirect("https://localhost:44302/Main/Login");
            }
            var z = HttpContext.Session.GetString("ExamsId");
            if (z == null)
            {
                return Redirect("https://localhost:44302/admin/home/index");
            }
            string out_mess = "";
            List<StudentSubject> ss = emr.GetStudentOfSubject(Convert.ToInt32(id), out out_mess);
            return View("Student", ss);
        }


        /*
          * Xóa một môn học của kì học
        */
        [Route("subjectid")]
        [HttpDelete]
        public AjaxResult DeleteSubject([FromBody] List<string> id)
        {
            var y = HttpContext.Session.GetString("AdminId");
            if (y == null)
            {
                Response.Redirect("https://localhost:44302/Main/Login");
                return null;
            }
            var z = HttpContext.Session.GetString("ExamsId");
            if (z == null)
            {
                Response.Redirect("https://localhost:44302/admin/home/index");
                return null;
            }
            string out_mess = "";
            AjaxResult ajaxResult = new AjaxResult();
            List<int> ids = new List<int>();
            foreach (var item in id)
            {
                int idc = int.Parse(item);
                ids.Add(idc);
            }
            emr.DeleteSubject(ids,out out_mess );
            ajaxResult.Message = out_mess;
            return ajaxResult;
}



        /*
          *Trả về danh sách học sinh
        */
        [Route("subject/listStudent")]
        public IActionResult ListStudent()
        {
            return View();

        }



        /*
          *Thêm một danh sách học sinh
        */
        [Route("addliststudent")]
        [HttpPost]
        public AjaxResult AddListStudentSubject([FromBody] ListStudentExam a)
        {
            var y = HttpContext.Session.GetString("AdminId");
            if (y == null)
            {
                Response.Redirect("https://localhost:44302/Main/Login");
                return null;
            }
            var z = HttpContext.Session.GetString("ExamsId");
            if (z == null)
            {
                Response.Redirect("https://localhost:44302/admin/home/index");
                return null;
            }
            int examination = 0;
            int dupli = 0;
            int addnewstudent = 0;
            int editstudent = 0;
            int error = 0;
            string out_mess = "";
            AjaxResult ajaxResult = new AjaxResult();
            foreach(StudentsExam st in a.studentId)
            {
                if(st.studentName == null)
                {
                    st.studentName = "0";
                }
                emr.AddStudentInSubject(st.studentId, st.studentName, st.studentClass, a.testAble,a.subjectId, out out_mess);
                if(out_mess == "1")
                {
                    examination++;
                }
                else if (out_mess == "2")
                {
                    dupli++;
                }
                else if (out_mess == "3")
                {
                    addnewstudent++;
                }
                else if (out_mess == "4")
                {
                    editstudent++;

                }
                else 
                {
                    error++;
                }
            }
            string mess = String.Format("Thêm mới {0} sinh viên " +
                "Thêm mới {1} sinh viên trong môn thi " +
                "Bị trùng {2} sinh viên " +
                "Không tồn tại môn {3}" +
                "Lỗi không thêm được {4} sinh viên", addnewstudent, editstudent, dupli, examination, error);
            ajaxResult.Message = mess;
            return ajaxResult;
        }




        [Route("term")]
        public IActionResult Term()
        {
            return View();
        }
        
    }
}