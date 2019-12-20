using ExamReg_0._0.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ExamReg_0._0.DataRepository
{
    public class ExaminationRepository
    {
        private SqlConnection conn = new SqlConnection("Data Source=DESKTOP-K4B953D;Initial Catalog=ExamManagerApplication;Integrated Security=True");
        
        
        /*
          * Lấy danh sách các môn thi trong một kì thi sql server
        */
        public List<Examination> GetExaminations(int examperiodid, out string out_mess )
        {
            List<Examination> data = new List<Examination>();
            out_mess = "";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SHOW_ALL_SUBJECT_SAME_EXAM_PERIOD", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IN_EXAMS_ID", SqlDbType.Int);
                cmd.Parameters["@IN_EXAMS_ID"].Value = examperiodid;
                cmd.Parameters.Add("@OUT_MESS", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var ext = new Examination
                        (
                            reader[1].ToString(),
                            Convert.ToInt32(reader[2]),
                            Convert.ToInt32(reader[0])
                        );
                    data.Add(ext);
                }
            }
            catch (Exception ex)
            {
                out_mess = ex.Message;
                return null;
            }
            return data;
        }
        
        
        /*
          *Thêm một môn thi trong một kì thi sql server
        */
        public void AddSubject(string subject_name, int examperiodid, out string out_mess)
        {
            out_mess = "";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("ADD_EXAMINATION", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IN_SUBJECT_NAME", SqlDbType.NVarChar, 50);
                cmd.Parameters["@IN_SUBJECT_NAME"].Value = subject_name;
                cmd.Parameters.Add("@IN_EXAMPERIOD_ID", SqlDbType.Int);
                cmd.Parameters["@IN_EXAMPERIOD_ID"].Value = examperiodid ;
                cmd.Parameters.Add("@OUT_MESSAGE", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                out_mess = cmd.Parameters["@OUT_MESSAGE"].Value.ToString();
            }
            catch (Exception ex)
            {
                out_mess = ex.Message;
            }
        }
        
        
        /*
          * Lấy danh sách các học sinh trong một môn thi của kì thi từ sql server
        */
        public List<StudentSubject> GetStudentOfSubject(int subjectid, out string out_mess)
        {
            List<StudentSubject> data = new List<StudentSubject>();
            out_mess = "";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SHOW_STUDENT_OF_SUBJECT", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IN_SUBJECT_ID", SqlDbType.Int);
                cmd.Parameters["@IN_SUBJECT_ID"].Value = subjectid;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var ext = new StudentSubject
                        (
                            reader[0].ToString(),
                            reader[1].ToString(),
                            Convert.ToInt32(reader[3]),
                            Convert.ToBoolean(reader[2])
                        );
                    data.Add(ext);
                }
            }
            catch (Exception ex)
            {
                out_mess = ex.Message;
                return null;
            }
            return data;
        }
        
        
        /*
          * Lấy danh sách các kì thi từ sql server
        */
        public List<ExamPeriod> GetExamPeriod(out string out_mess)
        {
            List<ExamPeriod> data = new List<ExamPeriod>();
            out_mess = "";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SHOW_EXAM_PERIOD", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var ext = new ExamPeriod
                        (
                            Convert.ToInt32(reader[0]),
                            Convert.ToInt32(reader[1])

                        );
                    data.Add(ext);
                }
            }
            catch (Exception ex)
            {
                out_mess = ex.Message;
                return null;
            }
            return data;
        }


        private static DataTable CreateDataTable2(List<int> ids)
        {
            DataTable table = new DataTable();
            table.Columns.Add("IN_SUBJECT_ID", typeof(int));
            foreach (int id in ids)
            {
                table.Rows.Add(id);
            }
            return table;
        }
        
        
        /*
          * Xóa môn thi của kì thi từ sql server
        */
        public void DeleteSubject(List<int> list_id, out string out_mess)
        {
            
            out_mess = "";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE_EXAMINATION", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LIST_SUBJECT_ID", CreateDataTable2(list_id));
                cmd.Parameters["@LIST_SUBJECT_ID"].SqlDbType = SqlDbType.Structured;
                cmd.Parameters["@LIST_SUBJECT_ID"].TypeName = "LIST_SUBJECT";
                cmd.Parameters.Add("@OUT_MESS", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                out_mess = ex.Message;
            }
        }
        public void out_examperiod(int subjectid, out int begin, out int end)
        {
            begin = -1; end = -1;
            try
            {
                conn.Open();
                string getcmd = "select a.ExamPeriodBegin, a.ExamPeriodEnd from [dbo].[Examination] as a WHERE a.SubjectId =" + subjectid;
                SqlCommand cmd = new SqlCommand(getcmd, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    begin = Convert.ToInt32(reader[0]);
                    end = Convert.ToInt32(reader[1]);
                }
            }
            catch (Exception ex) { }
        }
        
        
        
        /*
          * Thêm sinh viên trong một môn thi của kì thi từ sql server
        */
        public void AddStudentInSubject(int studentId, string studentName, string studentClass, bool isTest, int subjectId, out string out_mess)
        {
            out_mess = "";
            int bit;
            if (isTest == true)
            {
                bit = 1;
            }
            else
            {
                bit = 0;
            }
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("ADD_STUDENT_IN_SUBJECT", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IN_STUDENT_CLASS", SqlDbType.NVarChar, 50);
                cmd.Parameters["@IN_STUDENT_CLASS"].Value = studentClass;
                cmd.Parameters.Add("@IN_STUDENT_ID", SqlDbType.Int);
                cmd.Parameters["@IN_STUDENT_ID"].Value = studentId;
                cmd.Parameters.Add("@IN_STUDENT_NAME", SqlDbType.NVarChar);
                cmd.Parameters["@IN_STUDENT_NAME"].Value = studentName;
                cmd.Parameters.Add("@IN_IS_TEST", SqlDbType.Bit);
                cmd.Parameters["@IN_IS_TEST"].Value = bit;
                cmd.Parameters.Add("@IN_SUBJECT_ID", SqlDbType.Int);
                cmd.Parameters["@IN_SUBJECT_ID"].Value =  subjectId;
                cmd.Parameters.Add("@OUT_MESS", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                out_mess = cmd.Parameters["@OUT_MESS"].Value.ToString();
            }
            catch (Exception ex)
            {
                out_mess = ex.Message;
            }
            conn.Close();
        }
    }
}
