using ExamReg_0._0.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ExamReg_0._0.DataRepository
{
    public class MainRepository
    {
        public SqlConnection conn = new SqlConnection("Data Source=DESKTOP-K4B953D;Initial Catalog=ExamManagerApplication;Integrated Security=True");
        
        /*
          * Trả về kết quả của đăng nhập có vượt qua hay không, xét xem là học sinh hay là admin của kì thi từ sql server
        */
        public int LoginResult(out string out_mess,out bool is_admin, int studentId, string password)
        {
            is_admin = false;
            out_mess = "";
            int result;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("LOGIN", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@StudentId", SqlDbType.Int);
                cmd.Parameters["@StudentId"].Value = studentId;
                cmd.Parameters.Add("@MatKhau", SqlDbType.NVarChar);
                cmd.Parameters["@MatKhau"].Value = password;
                cmd.Parameters.Add("@IS_ADMIN", SqlDbType.Bit, 50).Direction = ParameterDirection.Output;
                object a = cmd.ExecuteScalar();
                result = Convert.ToInt32(a);
                is_admin = Convert.ToBoolean(cmd.Parameters["@IS_ADMIN"].Value);
            }
            catch (Exception ex)
            {
                out_mess = ex.Message;
                return 9;
            }
            return result;
        }
        
        
        /*
          * Lấy tất cả các kì thi từ sql server
        */
        public List<Exams> GetAllExams(out string out_mess)
        {
            List<Exams> data = new List<Exams>();
            out_mess = "";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GET_ALL_EXAMS", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var ext = new Exams
                        (
                            Convert.ToInt32(reader[1]),
                            reader[0].ToString(),
                            Convert.ToInt32(reader[3]),
                            Convert.ToInt32(reader[2])
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
          * Thêm một kì thi
        */
        public void AddExamPeriod(string periodname, int begin, int end, out string out_mess)
        {
            out_mess = "";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("ADD_EXAM_PERIOD", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IN_EXAMPERIOD_NAME", SqlDbType.NVarChar, 50);
                cmd.Parameters["@IN_EXAMPERIOD_NAME"].Value = periodname;
                cmd.Parameters.Add("@IN_EXAMPERIOD_BEGIN", SqlDbType.Int);
                cmd.Parameters["@IN_EXAMPERIOD_BEGIN"].Value = begin;
                cmd.Parameters.Add("@IN_EXAMPERIOD_END", SqlDbType.Int);
                cmd.Parameters["@IN_EXAMPERIOD_END"].Value = end;
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
          * Lấy mã của Kì Thi từ sql server
        */
        public int GetExamsId(string periodname, out string out_mess)
        {
            int x = -1;
            out_mess = "";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GET_EXAMS_ID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IN_EXAMS_NAME ", SqlDbType.NVarChar, 50);
                cmd.Parameters["@IN_EXAMS_NAME "].Value = periodname;
                cmd.Parameters.Add("@OUT_MESSAGE", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    x = Convert.ToInt32(reader[0]);
                }
                out_mess = cmd.Parameters["@OUT_MESSAGE"].Value.ToString();
            }
            catch (Exception ex)
            {
                out_mess = ex.Message;
            }
            return x;
        }
        
        
        /*
          * Lấy kì thi đang được tiến hành từ sql server
        */
        public List<ExamActive> GetExamsActive(out string out_mess)
        {
            List<ExamActive> data = new List<ExamActive>();
            out_mess = "";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GET_EXAMS_IS_ACTIVE", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@OUT_MESS", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    var x = new ExamActive(Convert.ToInt32(reader[0]), reader[1].ToString());
                    data.Add(x);
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
          * Update một kì thi để thi lên sql server
        */
        public void UpdateExamsIsActive(int exam_id, out string out_mess)
        {
            out_mess = "";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE_EXAMS_ACTIVE", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IN_EXAMS_ID", SqlDbType.Int);
                cmd.Parameters["@IN_EXAMS_ID"].Value = exam_id;
                cmd.Parameters.Add("@OUT_MESS", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                out_mess = cmd.Parameters["@OUT_MESS"].Value.ToString();
            }
            catch (Exception ex)
            {
                out_mess = ex.Message;
            }
        }
        
        
        /*
          * Đổi mật khẩu của sinh viên lên sql server
        */
        public void ChangePassWord(int studentid, string oldpass, string newpass, out string out_mess)
        {
            out_mess = "";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("CHANGE_PASSWORD", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IN_OLD_PASSWORD", SqlDbType.NVarChar);
                cmd.Parameters["@IN_OLD_PASSWORD"].Value = oldpass;
                cmd.Parameters.Add("@IN_NEW_PASSWORD", SqlDbType.NVarChar);
                cmd.Parameters["@IN_NEW_PASSWORD"].Value = newpass;
                cmd.Parameters.Add("@IN_STUDENT_ID", SqlDbType.NVarChar);
                cmd.Parameters["@IN_STUDENT_ID"].Value = studentid;
                cmd.Parameters.Add("@OUT_MESS", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                out_mess = cmd.Parameters["@OUT_MESS"].Value.ToString();
            }
            catch (Exception ex)
            {
                out_mess = ex.Message;
            }
        }
    }
    
}

