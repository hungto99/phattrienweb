using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamReg_0._0.Models
{
    public class ExamActive
    {
        private int examid;
        private string examname;

        public int Examid { get => examid; set => examid = value; }
        public string Examname { get => examname; set => examname = value; }

        public ExamActive(int examid, string examname)
        {
            this.Examid = examid;
            this.Examname = examname;
        }
    }
}
