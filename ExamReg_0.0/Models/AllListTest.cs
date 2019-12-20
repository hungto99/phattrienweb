using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamReg_0._0.Models
{
    public class AllListTest
    {
        string subjectName;
        int caseTestIdIsJoin;
        bool checkJoin;
        int subjectId;
        int caseTestId;
        int computerCount;
        int examOrder;
        DateTime examDate;
        int computerNumber;
        int roomOrder;
        string roomLocation;
        int roomId;
        public AllListTest(string subjectName, int caseTestIdIsJoin, bool checkJoin, int subjectId, int caseTestId, int computerCount, int examOrder, DateTime examDate, int computerNumber, int roomOrder, string roomLocation, int roomId)
        {
            this.subjectName = subjectName;
            this.caseTestIdIsJoin = caseTestIdIsJoin;
            this.checkJoin = checkJoin;
            this.subjectId = subjectId;
            this.caseTestId = caseTestId;
            this.computerCount = computerCount;
            this.examOrder = examOrder;
            this.examDate = examDate;
            this.computerNumber = computerNumber;
            this.roomOrder = roomOrder;
            this.roomLocation = roomLocation;
            this.roomId = roomId;
        }

        public string SubjectName { get => subjectName; set => subjectName = value; }
        public int CaseTestIdIsJoin { get => caseTestIdIsJoin; set => caseTestIdIsJoin = value; }
        public bool CheckJoin { get => checkJoin; set => checkJoin = value; }
        public int SubjectId { get => subjectId; set => subjectId = value; }
        public int CaseTestId { get => caseTestId; set => caseTestId = value; }
        public int ComputerCount { get => computerCount; set => computerCount = value; }
        public int ExamOrder { get => examOrder; set => examOrder = value; }
        public DateTime ExamDate { get => examDate; set => examDate = value; }
        public int ComputerNumber { get => computerNumber; set => computerNumber = value; }
        public int RoomOrder { get => roomOrder; set => roomOrder = value; }
        public string RoomLocation { get => roomLocation; set => roomLocation = value; }
        public int RoomId { get => roomId; set => roomId = value; }
    }
}
