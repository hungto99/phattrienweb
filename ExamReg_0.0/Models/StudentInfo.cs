using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamReg_0._0.Models
{
    public class StudentInfo
    {
        private int studentId;
        private String studentClass;
        private String studentName;

        public int StudentId { get => studentId; set => studentId = value; }
        public string StudentClass { get => studentClass; set => studentClass = value; }
        public string StudentName { get => studentName; set => studentName = value; }
    }
}
