using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamReg_0._0.Models
{
    public class StudentSubject
    {
        private int studentId;
        private string studentName;
        private string studentClass;
        private bool testAble;

        public StudentSubject( string studentClass, string studentName, int studentId,  bool testAble)
        {
            this.StudentId = studentId;
            this.StudentName = studentName;
            this.StudentClass = studentClass;
            this.TestAble = testAble;
        }

        public int StudentId { get => studentId; set => studentId = value; }
        public string StudentName { get => studentName; set => studentName = value; }
        public string StudentClass { get => studentClass; set => studentClass = value; }
        public bool TestAble { get => testAble; set => testAble = value; }
    }
}
