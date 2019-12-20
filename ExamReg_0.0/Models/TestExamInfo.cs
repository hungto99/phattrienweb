using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamReg_0._0.Models
{
    public class TestExamInfo
    {
        public int ComputerCount;
         public DateTime ExamDate;
         public int ExamOrder;
        public int ComputerNumber;
         public int RoomOrder;
        public string RoomLocation;
        public string SubjectName;

        public TestExamInfo(int computerCount, DateTime examDate, int examOrder, int computerNumber, int roomOrder, string roomLocation, string subjectName)
        {
            ComputerCount = computerCount;
            ExamDate = examDate;
            ExamOrder = examOrder;
            ComputerNumber = computerNumber;
            RoomOrder = roomOrder;
            RoomLocation = roomLocation;
            SubjectName = subjectName;
        }
    }
}
