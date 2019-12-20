using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamReg_0._0.Models
{
    public class ExamPeriod
    {
        private int examPeriodBegin;
        private int examPeriodEnd;

        public ExamPeriod(int examPeriodBegin, int examPeriodEnd)
        {
            this.ExamPeriodBegin = examPeriodBegin;
            this.ExamPeriodEnd = examPeriodEnd;
        }

        public int ExamPeriodBegin { get => examPeriodBegin; set => examPeriodBegin = value; }
        public int ExamPeriodEnd { get => examPeriodEnd; set => examPeriodEnd = value; }
    }
}
