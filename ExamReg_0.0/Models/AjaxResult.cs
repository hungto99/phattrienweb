using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamReg_0._0.Models
{
    public class AjaxResult
    {
        private object data;
        private String message;
        private bool succeed;

        public object Data { get => data; set => data = value; }
        public string Message { get => message; set => message = value; }
        public bool Succeed { get => succeed; set => succeed = value; }
    }
}
