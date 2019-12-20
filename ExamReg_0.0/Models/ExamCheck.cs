using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamReg_0._0.Models
{
    public class ExamCheck
    {
        private int subjectId;
        private string subjectName;
        private int caseTestId;
        public string time { get; set; }
        public int examOrder { get; set; }
        public int SubjectId { get => subjectId; set => subjectId = value; }
        public string SubjectName { get => subjectName; set => subjectName = value; }
        public int CaseTestId { get => caseTestId; set => caseTestId = value; }
    }
}
