using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamReg_0._0.Models
{
    public class Exams
    {
        public int ExamsId;
        public string ExamsName;
        public int ExamsPeriodBegin;
        public int ExamsPeriodEnd;

        public Exams(int examsId, string examsName, int examsPeriodBegin, int examsPeriodEnd)
        {
            ExamsId = examsId;
            ExamsName = examsName;
            ExamsPeriodBegin = examsPeriodBegin;
            ExamsPeriodEnd = examsPeriodEnd;
        }
    }
}
