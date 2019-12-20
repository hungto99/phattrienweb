using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamReg_0._0.Models
{
    public class StudentClass
    {
        private int studentId;
        private string studentName;
        private string passWord;
        private string status;

        public StudentClass(int studentId, string studentName, string passWord, string status)
        {
            this.StudentId = studentId;
            this.StudentName = studentName;
            this.PassWord = passWord;
            this.Status = status;
        }

        public int StudentId { get => studentId; set => studentId = value; }
        public string StudentName { get => studentName; set => studentName = value; }
        public string PassWord { get => passWord; set => passWord = value; }
        public string Status { get => status; set => status = value; }
    }
}
