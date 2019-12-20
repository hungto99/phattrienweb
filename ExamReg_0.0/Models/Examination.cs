using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamReg_0._0.Models
{
    public class Examination
    {
        private String subjectName;
        private int examPeriodId;
        private int subjectId;

        public Examination(string subjectName, int examPeriodId, int subjectId)
        {
            this.SubjectName = subjectName;
            this.ExamPeriodId = examPeriodId;
            this.SubjectId = subjectId;
        }

        public string SubjectName { get => subjectName; set => subjectName = value; }
        public int ExamPeriodId { get => examPeriodId; set => examPeriodId = value; }
        public int SubjectId { get => subjectId; set => subjectId = value; }
    }
}
