using ExamReg_0._0.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ExamReg_0._0.DataRepository
{
    public class TestExamRepository
    {

        public SqlConnection conn = new SqlConnection("Data Source=DESKTOP-K4B953D;Initial Catalog=ExamManagerApplication;Integrated Security=True");
        
        
        /*
          * Lấy tất cả ca thi của môn trong một kì thi từ sql server
        */
        public List<TestExam> GetTestExam(string subjects,int examid, out string out_mess)
        {
            out_mess = "";
            List<TestExam> data = new List<TestExam>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SHOW_ALL_TEST_EXAM_OF_SUBJECT", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@INT_SUBJECT_ID", SqlDbType.Int);
                cmd.Parameters["@INT_SUBJECT_ID"].Value = int.Parse(subjects);
                cmd.Parameters.Add("@INT_EXAM_ID", SqlDbType.Int);
                cmd.Parameters["@INT_EXAM_ID"].Value = examid;
                cmd.Parameters.Add("@OUT_MESSAGE", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var order = Convert.ToInt32(reader[10]);
                    var hour = "7h-9h";
                    if (order == 1)
                    {
                        hour = "7h-9h";
                    }
                    if (order == 2)
                    {
                        hour = "9h-11h";
                    }
                    if (order == 1)
                    {
                        hour = "11h-13h";
                    }
                    if (order == 1)
                    {
                        hour = "13h-15h";
                    }
                    if (order == 1)
                    {
                        hour = "15h-17h";
                    }
                    if (order == 1)
                    {
                        hour = "17h-19h";
                    }
                    string x = reader[1].ToString();
                    x = x.ToString();
                    var erb = new TestExam(
                        Convert.ToInt32(reader[0]),
                        Convert.ToInt32(reader[1]), 
                        Convert.ToDateTime(reader[2]).ToString("dd-MM-yyyy"), 
                        Convert.ToBoolean(reader[3]), 
                        Convert.ToInt32(reader[4]), 
                        Convert.ToInt32(reader[5]), 
                        reader[6].ToString(),
                        Convert.ToInt32(reader[7]), 
                        reader[8].ToString(),
                        Convert.ToInt32(reader[9]),
                        hour
                        );
                    data.Add(erb);
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
          * Thêm một ca thi cho một kì thi
        */
        public void AddTestExam(int roomid, int subjectid, int examorder, DateTime datetime, bool istest, out string out_mess)
        {
            out_mess = "";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("ADD_TEST_EXAM", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IN_SUBJECT_ID", SqlDbType.Int, 50);
                cmd.Parameters["@IN_SUBJECT_ID"].Value = subjectid;
                cmd.Parameters.Add("@IN_EXAM_ODER", SqlDbType.Int);
                cmd.Parameters["@IN_EXAM_ODER"].Value = examorder;
                cmd.Parameters.Add("@IN_ROOM_ID", SqlDbType.Int);
                cmd.Parameters["@IN_ROOM_ID"].Value = roomid;
                cmd.Parameters.Add("@IN_EXAM_DATE", SqlDbType.DateTime);
                cmd.Parameters["@IN_EXAM_DATE"].Value = datetime;
                cmd.Parameters.Add("@IN_IS_TEST", SqlDbType.Bit);
                cmd.Parameters["@IN_IS_TEST"].Value = 0;
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
          * Lấy tất cả các ca thi có thể đăng kí được cho sinh viên từ sql server
        */
        public List<AllListTest> GetAllTestExamStudentSubject(int student_id, int examnowactive, out string out_mess) 
        {
            out_mess = "";
            
            List<AllListTest> allListTests = new List<AllListTest>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SHOW_ALL_TEST_EXAM_OF_STUDENT", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IN_STUDENT_ID ", SqlDbType.Int);
                cmd.Parameters["@IN_STUDENT_ID "].Value = student_id;
                cmd.Parameters.Add("@IN_EXAM_IS_ACTIVE", SqlDbType.Int);
                cmd.Parameters["@IN_EXAM_IS_ACTIVE"].Value = examnowactive;
                cmd.Parameters.Add("@OUT_MESS", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                   int x;
                    var y = reader[0];
                    if(reader[1] is  DBNull)
                    {
                        x = 0;
                    }
                    else
                    {
                        x = Convert.ToInt32(reader[1]);
                    }
                    var alt = new AllListTest(
                            reader[0].ToString(),
                            x,
                            Convert.ToBoolean(reader[2]),
                            Convert.ToInt32(reader[3]),
                            Convert.ToInt32(reader[4]),
                            Convert.ToInt32(reader[5]),
                            Convert.ToInt32(reader[6]),
                            Convert.ToDateTime(reader[7]),
                            Convert.ToInt32(reader[8]),
                            Convert.ToInt32(reader[9]),
                            reader[10].ToString(),
                            Convert.ToInt32(reader[11])
                            );
                    allListTests.Add(alt);
                }
            }
            catch (Exception ex)
            {
                out_mess = ex.Message;
            }
                return allListTests;
        }


        /*
          * Ghi nhận một ca thi cho sinh viên lên sql server
        */
        public void JoinInTestExam(int student_id, int subject_id, int casetest_before, int casetest, out string out_mess)
        {
            out_mess = "";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SAVE_TEST_EXAM", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IN_SUBJECT_ID", SqlDbType.Int, 50);
                cmd.Parameters["@IN_SUBJECT_ID"].Value = subject_id;
                cmd.Parameters.Add("@IN_STUDENT_ID", SqlDbType.Int, 50);
                cmd.Parameters["@IN_STUDENT_ID"].Value = student_id;
                cmd.Parameters.Add("@IN_TESTCASE_ID", SqlDbType.Int, 50);
                cmd.Parameters["@IN_TESTCASE_ID"].Value = casetest;
                cmd.Parameters.Add("@IN_TESTCASE_BEFORE_ID", SqlDbType.Int, 50);
                cmd.Parameters["@IN_TESTCASE_BEFORE_ID"].Value = casetest_before;
                cmd.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                out_mess = ex.Message;
            }
        }


        /*
          * Lấy danh sách học sinh của ca thi
        */
        public List<Students> GetALlStudentOfTestExam(int testcaseid, out string out_mess)
        {
            out_mess = "";

            List<Students> student = new List<Students>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GET_ALL_LIST_STUDENT_IN_TEST_EXAM", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IN_CASE_TEST_ID", SqlDbType.Int);
                cmd.Parameters["@IN_CASE_TEST_ID"].Value = testcaseid;
                cmd.Parameters.Add("@OUT_MESS", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string sex = "";
                    if (Convert.ToBoolean(reader[2]) == true)
                    {
                        sex = "Nam";
                    }
                    else
                    {
                        sex = "Nữ";
                    }
                    var sts = new Students(
                            Convert.ToInt32(reader[0]),
                            reader[1].ToString(),
                            sex,
                            reader[3].ToString(),
                            reader[4].ToString(),
                            reader[5].ToString()
                            ) ;
                    student.Add(sts);
                }
            }
            catch (Exception ex)
            {
                out_mess = ex.Message;
            }
            return student;
        }


        /*
          * Lây thông tin ca thi từ sql server
        */
        public TestExamInfo GetTestInfo(int casetestid)
        {
            try { 
            conn.Open();
            SqlCommand cmd = new SqlCommand("GET_TEST_EXAMS", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@INT_EXAM_ID", SqlDbType.Int);
            cmd.Parameters["@INT_EXAM_ID"].Value = casetestid;
            
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string x = reader[1].ToString();
                x = x.ToString();
                var tei = new TestExamInfo(
                    Convert.ToInt32(reader[0]),
                    Convert.ToDateTime(reader[1]),
                    Convert.ToInt32(reader[2]),
                    Convert.ToInt32(reader[3]),
                    Convert.ToInt32(reader[4]),
                    (reader[5]).ToString(),
                    reader[6].ToString()
                    );
                    return tei;
                }
               
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
}

        /*
          * Lấy thông tin học sinh sql server
        */
        public Students GetStudentInfo(int studentid)
        {
            List<Students> student = new List<Students>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GET_STUDENT_INFO", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IN_STUDENT_ID", SqlDbType.Int);
                cmd.Parameters["@IN_STUDENT_ID"].Value = studentid;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string sex = "";
                    if (Convert.ToBoolean(reader[3]) == true)
                    {
                        sex = "Nam";
                    }
                    else
                    {
                        sex = "Nữ";
                    }
                    var sts = new Students(
                            Convert.ToInt32(reader[0]),
                            reader[1].ToString(),
                            sex,
                            reader[2].ToString(),
                            Convert.ToDateTime(reader[4]).ToString("dd-MM-yyyy"),
                            reader[5].ToString()
                            );
                    return sts;
                }
            }
            catch (Exception ex)
            {
                
            }
            return null;
        }
    }
    

}
