using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamReg_0._0.Models
{
    public class User
    {
        private String password;
        private int studentId;
        private int status;
        public string Password { get => password; set => password = value; }
        public int StudentId { get => studentId; set => studentId = value; }
        public int Status { get => status; set => status = value; }
    }
}
