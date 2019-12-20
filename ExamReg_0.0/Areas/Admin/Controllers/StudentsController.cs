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
    [Route("admin/students")]
    public class StudentsController : Controller
    {
        public ClassRepository cr = new ClassRepository();


        /*
          *Trả về trang danh sách học sinh theo lớp cho admin
        */
        [Route("students")]
        public IActionResult Students()
        {
            return View("Class");
        }



        /*
          *Lấy danh sách tất cả các lớp
        */
        [Route("class")]
        [HttpGet]
        public AjaxResult GetAllClass()
        {
            var x = HttpContext.Session.GetString("AdminId");
            if (x == null)
            {
                Response.Redirect("https://localhost:44302/Main/Login");
                return null;
            }
            string out_mess = "";
            AjaxResult ajaxResult = new AjaxResult();
            ajaxResult.Data = cr.GetAllClass(out out_mess);
            ajaxResult.Message = out_mess;
            return ajaxResult;
        }



        /*
          *Lấy danh sách tất cả các học sinh trong một lớp
        */
        [Route("getlistclass/{id}")]
        public IActionResult GetAllStudentInClass (string id)
        {
            var x = HttpContext.Session.GetString("AdminId");
            if (x == null)
            {
                Response.Redirect("https://localhost:44302/Main/Login");
                return null;
            }
            string out_mess = "";
            List<StudentClass> sc = new List<StudentClass>();
            sc = cr.GetStudentClass(id, out out_mess);
            return View("Students", sc);
        }



        /*
          *Thêm một lớp
        */
        [Route("addclass")]
        [HttpPost]
        public AjaxResult AddClass([FromBody] Class a){
            var z = HttpContext.Session.GetString("AdminId");
            if (z == null)
            {
                Response.Redirect("https://localhost:44302/Main/Login");
                return null;
            }
            string out_mess = "";
            AjaxResult ajaxResult = new AjaxResult();
            int add = 0;
            int edit = 0;
            int dupli = 0;
            int error = 0;
            foreach (Students2 st in a.students)
            {
                
                bool sex = true;
                if (st.sex == "Nam")
                {
                    sex = true;
                }
                if (st.sex == "Nữ")
                {
                    sex = false;
                }
                var x = Convert.ToDateTime(st.birthDay);
                cr.AddStudentInClass(st.studentId, st.studentName, a.className, sex, st.bornPlace, "1", Convert.ToDateTime(st.birthDay), out out_mess);
                if (out_mess == "1")
                {
                    dupli++;
                }
                else if (out_mess == "2")
                {
                    edit++;
                }
                else if (out_mess == "3")
                {
                    add++;
                }
                else
                {
                    error++;
                }
            }
            string mess = String.Format("Thêm thành công {0} sinh viên" +
                "Sửa thành công {1} sinh viên" +
                "Trùng {2} sinh viên" +
                "Không thành công {3}", add, edit, dupli, error);
            ajaxResult.Message = mess;
            return ajaxResult;
        }
    }
}