using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ExamReg_0._0.DataRepository;
using ExamReg_0._0.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ExamReg_0._0.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/exams")]
    public class ExamsController : Controller
    {

        public ExamRoomRepository erp = new ExamRoomRepository();
        public ExaminationRepository emp = new ExaminationRepository();
        public TestExamRepository tep = new TestExamRepository();
        public ExamsController()
        {
            erp = new ExamRoomRepository();
        }



        /*
          *Trả về trang phòng thi cho admin
        */
        [Route("exam room")]
        public IActionResult ExamRoom()
        {
            var x = HttpContext.Session.GetString("AdminId");
            if (x == null)
            {
                return Redirect("https://localhost:44302/Main/Login");
  
            }
            return View();
        }



        /*
          *Lấy danh sách phòng thi cho admin
        */
        [Route("examroom")]
        [HttpPost]
        public AjaxResult GetAllRoom([FromBody] List<string> room)
        {
            var x = HttpContext.Session.GetString("AdminId");
            if (x == null)
            {
                Response.Redirect("https://localhost:44302/Main/Login");
                return null;
            }
            AjaxResult _ajaxResult = new AjaxResult();
            var a = room;
            String out_mess = "";
            _ajaxResult.Data = this.erp.GetExamRooms(room[0], int.Parse(room[1]), out out_mess);
            _ajaxResult.Message = out_mess;
            _ajaxResult.Succeed = true;
            return _ajaxResult;
        }
        [Route("addroom")]
        [HttpPost]


        /*
          *Thêm phòng thi cho admin
        */
        public AjaxResult AddRoom([FromBody] List<string> roominfo)
        {
            var x = HttpContext.Session.GetString("AdminId");
            if (x == null)
            {
                Response.Redirect("https://localhost:44302/Main/Login");
                return null;
            }
            AjaxResult ajaxResult = new AjaxResult();
            string out_mess = "";
            erp.AddRoom(Convert.ToInt32(roominfo[0]), Convert.ToInt32(roominfo[1]), roominfo[2].ToString(), out out_mess);
            ajaxResult.Message = out_mess;
            return ajaxResult;
        }


        /*
          *Lấy địa điểm thi của tất cả phòng thi
        */
        [Route("examlocation")]
        [HttpGet]
        public AjaxResult GellAllRoomLocation()
        {
            var x = HttpContext.Session.GetString("AdminId");
            if (x == null)
            {
                Response.Redirect("https://localhost:44302/Main/Login");
                return null;
            }
            AjaxResult _ajaxResult = new AjaxResult();
            _ajaxResult = this.erp.GetRoomLocation();
            return _ajaxResult;
        }


        /*
          *Xóa phòng thi của
        */

        [Route("examroomid")]
        [HttpDelete]
        public AjaxResult DeleteRoom([FromBody] List<string> id)
        {
            var x = HttpContext.Session.GetString("AdminId");
            if (x == null)
            {
                Response.Redirect("https://localhost:44302/Main/Login");
                return null;
            }
            AjaxResult _ajaxResult = new AjaxResult();
            List<int> ids = new List<int>();
            foreach(var item in id)
            {
                int idc = int.Parse(item);
                ids.Add(idc);
            }
            erp.DeleteRooms(ids);
            return _ajaxResult;
        }


        /*
          * Trả về trang ca thi của kì thi cho admin
        */
        [Route("examTime")]
        public IActionResult ExamTime()
        {
            return View();
        }


        /*
          * Lấy danh sách ca thi của kì thi cho admin
        */
        [Route("examtime")]
        [HttpPost]
        public AjaxResult GetExamTime([FromBody] List<string> subjectid)
        {
            var x = HttpContext.Session.GetString("AdminId");
            if (x == null)
            {
                Response.Redirect("https://localhost:44302/Main/Login");
                return null;
            }
            var y = HttpContext.Session.GetString("ExamsId");
            if (y == null)
            {
                Response.Redirect("https://localhost:44302/admin/home/index");
                return null;
            }
            AjaxResult ajaxResult = new AjaxResult();
            String out_mess = "";
            ajaxResult.Data = tep.GetTestExam(subjectid[0], Convert.ToInt32(y), out  out_mess);
            ajaxResult.Message = out_mess;
            return ajaxResult;
        }


        /*
          *Thêm một ca thi của một môn trong kì thi
        */
        [Route("addtestexam")]
        [HttpPost]
        public AjaxResult AddTestExam([FromBody] List<string> testexam)
        {
            var z = HttpContext.Session.GetString("AdminId");
            if (z == null)
            {
                Response.Redirect("https://localhost:44302/Main/Login");
                return null;
            }
            int begin = 0;
            int end = 0;
            bool check1 = false, check2 = false;
            string out_mess = "";
            AjaxResult ajaxResult = new AjaxResult();
            emp.out_examperiod(Convert.ToInt32(testexam[1]), out begin, out end);
            int roomid = Convert.ToInt32(testexam[0]);
            int subjectid = Convert.ToInt32(testexam[1]);
            int order = Convert.ToInt32(testexam[2]);
            DateTime datetime = Convert.ToDateTime(testexam[3].ToString());
            bool istest = Convert.ToBoolean(testexam[4]);
            string x = testexam[3].ToString().Substring(0, 4);
            //if(Convert.ToInt32(x)>begin && Convert.ToInt32(x) < end)
            //{
                check1 = true;
            //}
            if(order<7 && order > 0)
            {
                check2 = true;
            }
            if(check1 && check2)
            {
                tep.AddTestExam(roomid, subjectid, order, datetime, istest, out out_mess);
            }
            else
            {
                out_mess = "Đầu vào không hợp lệ";
            }
            ajaxResult.Message = out_mess;
            return ajaxResult;
        }


        /*
          *Lấy tất cả sinh viên có trong ca thi của môn thi trong kì thi
        */
        [Route("getallstudentlist/{id}")]
        public IActionResult GetAllStudentInTestExam(string id)
        {
            TestExamInfo tei = tep.GetTestInfo(Convert.ToInt32(id));
            tep.conn.Close();
            List <Students> lsts = new List<Students>();
            string out_mess = "";
            lsts = tep.GetALlStudentOfTestExam(Convert.ToInt32(id), out out_mess);
            ViewBag.Message = out_mess;
            ViewData["ComputerCount"] = tei.ComputerCount;
            ViewData["CoumputerNumber"] = tei.ComputerNumber;
            ViewData["Date"] = tei.ExamDate.ToString("dd-MM-yyyy");
            ViewData["SubjectName"] = tei.SubjectName;
            var order = "7h-9h";
            var hour = tei.RoomOrder;
            if(hour == 1)
            {
                order = "7h-9h";
            }
            if (hour == 2)
            {
                order = "9h-11h";
            }
            if (hour == 3)
            {
                order = "11h-13h";
            }
            if (hour == 4)
            {
                order = "13h-15h";
            }
            if (hour == 5)
            {
                order = "15h-17h";
            }
            if (hour == 6)
            {
                order = "17h-19h";
            }

            ViewData["ExamOrder"] = order;
            ViewData["ClassOrder"] = tei.RoomOrder;
            ViewData["RoomLocation"] = tei.RoomLocation;
            return View("StundetTestExam",lsts);
        }
    }
}