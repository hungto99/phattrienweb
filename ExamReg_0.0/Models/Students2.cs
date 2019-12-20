using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamReg_0._0.Models
{
    public class Students2
    {
        public Students2(int studentId, string studentName, string sex, string bornPlace, DateTime birthDay)
        {
            this.studentId = studentId;
            this.studentName = studentName;
            this.sex = sex;
            this.bornPlace = bornPlace;
            this.birthDay = birthDay;
        }

        public int studentId { get; set; }
        public string studentName { get; set; }
        public string sex { get; set; }
        public string bornPlace { get; set; }
        public DateTime birthDay { get; set; }

    }
}
