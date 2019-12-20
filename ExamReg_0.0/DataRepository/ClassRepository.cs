using ExamReg_0._0.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ExamReg_0._0.DataRepository
{
    public class ClassRepository
    {
      
            private SqlConnection conn = new SqlConnection("Data Source=DESKTOP-K4B953D;Initial Catalog=ExamManagerApplication;Integrated Security=True");
            
        
            /*
             * Lấy danh sách các lớp từ sql server 
            */
            public List<string> GetAllClass(out string out_mess)
            {
                List<string> data = new List<string>();
                out_mess = "";
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("GET_ALL_CLASS", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string cls = reader[0].ToString();
                        data.Add(cls);
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
             * Lấy tất cả danh sách học sinh trong một lớp được chọn từ sql server 
            */
            public List<StudentClass> GetStudentClass(string class_name, out string out_mess)
            {
            List<StudentClass> data = new List<StudentClass>();
            out_mess = "";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SHOW_ALL_STUDENT_IN_CLASS", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CLASS_NAME", SqlDbType.NVarChar);
                cmd.Parameters["@CLASS_NAME"].Value = class_name;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string password="";
                    string Status="";
                    if(reader[2].ToString() == "")
                    {
                        password = "Chưa có mật khẩu";
                    }
                    else
                    {
                        password = reader[2].ToString();
                    }
                    if (Convert.ToInt32(reader[3]) == 0)
                    {
                        Status = "Chưa cài mật khẩu";
                    }
                    if(Convert.ToInt32(reader[3]) == 1)
                    {
                        Status = "Chưa kích hoạt";
                    }
                    if (Convert.ToInt32(reader[3]) == 2)
                    {
                        Status = "Kích hoạt";
                    }
                    var e = new StudentClass
                        (
                            Convert.ToInt32(reader[0]),
                            reader[1].ToString(),
                            password,
                            Status
                        );
                    data.Add(e);
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
            * Thêm Thông tin học sinh vào một lớp
            */
            public void AddStudentInClass(int studentId, string studentName, string studentClass, bool sex, string born_place,string password, DateTime birthday, out string out_mess )
        {
            int bit;
            if (sex == true)
            {
                bit = 1;
            }
            else
            {
                bit = 0;
            }
            out_mess = "";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("ADD_STUDENT_IN_CLASS", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IN_STUDENT_CLASS", SqlDbType.NVarChar, 50);
                cmd.Parameters["@IN_STUDENT_CLASS"].Value = studentClass;
                cmd.Parameters.Add("@IN_STUDENT_ID", SqlDbType.Int);
                cmd.Parameters["@IN_STUDENT_ID"].Value = studentId;
                cmd.Parameters.Add("@IN_STUDENT_NAME", SqlDbType.NVarChar);
                cmd.Parameters["@IN_STUDENT_NAME"].Value = studentName;
                cmd.Parameters.Add("@IN_BORN_PLACE", SqlDbType.NVarChar);
                cmd.Parameters["@IN_BORN_PLACE"].Value = born_place;
                cmd.Parameters.Add("@IN_SEX", SqlDbType.Bit);
                cmd.Parameters["@IN_SEX"].Value = bit;
                cmd.Parameters.Add("@IN_BRITHDAY", SqlDbType.DateTime);
                cmd.Parameters["@IN_BRITHDAY"].Value = birthday;
                cmd.Parameters.Add("@IN_PASSWORD", SqlDbType.NVarChar);
                cmd.Parameters["@IN_PASSWORD"].Value = password;
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
