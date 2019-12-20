using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamReg_0._0.Models
{
    public class ListStudentExam
    {
        public List<StudentsExam> studentId { get; set; }
        public bool testAble { get; set; }
        public int subjectId { get; set; }
    }
}
