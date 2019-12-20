using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamReg_0._0.Models
{
    public class TestExam
    {
        private int caseTestId;
        private string examOrder;
        private int subjectId;
        private int roomId;
        private int roomOrder;
        private bool isTest;
        private string examDate;
        private int computerCount;
        private string roomLocation;
        private int computerNumber;
        private string subjectName;

        public TestExam(int caseTestId, int computerCount, string examDate, bool isTest, int roomId, int computerNumber, string roomLocation, int roomOrder, string subjectName,int subjectId, string examOrder)
        {
            this.caseTestId = caseTestId;
            this.RoomOrder = roomOrder;
            this.subjectId = subjectId;
            this.roomId = roomId;
            this.isTest = isTest;
            this.examDate = examDate;
            this.computerCount = computerCount;
            this.roomLocation = roomLocation;
            this.computerNumber = computerNumber;
            this.subjectName = subjectName;
            this.examOrder = examOrder;
        }
        public int CaseTestId { get => caseTestId; set => caseTestId = value; }
        public string ExamOrder { get => examOrder; set => examOrder = value; }
        public int SubjectId { get => subjectId; set => subjectId = value; }
        public int RoomId { get => roomId; set => roomId = value; }
        public bool IsTest { get => isTest; set => isTest = value; }
        public string ExamDate { get => examDate; set => examDate = value; }
        public int ComputerCount { get => computerCount; set => computerCount = value; }
        public string RoomLocation { get => roomLocation; set => roomLocation = value; }
        public int ComputerNumber { get => computerNumber; set => computerNumber = value; }
        public string SubjectName { get => subjectName; set => subjectName = value; }
        public int RoomOrder { get => roomOrder; set => roomOrder = value; }
    }
}
