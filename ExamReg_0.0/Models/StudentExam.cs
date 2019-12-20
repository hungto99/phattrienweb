using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamReg_0._0.Models
{
    public class StudentExam
    {
        private int studentId;
        private String studentName;
        private int subjectId;
        private bool testAble;
        private bool checkJoin;
        private bool checkTest;
        private int caseTestId;

        public int StudentId { get => studentId; set => studentId = value; }
        public string StudentName { get => studentName; set => studentName = value; }
        public int SubjectId { get => subjectId; set => subjectId = value; }
        public bool TestAble { get => testAble; set => testAble = value; }
        public bool CheckJoin { get => checkJoin; set => checkJoin = value; }
        public bool CheckTest { get => checkTest; set => checkTest = value; }
        public int CaseTestId { get => caseTestId; set => caseTestId = value; }
    }
}
