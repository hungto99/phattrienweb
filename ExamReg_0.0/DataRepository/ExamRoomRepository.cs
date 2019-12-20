using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

using ExamReg_0._0.Models;
using Microsoft.Data.SqlClient;

namespace ExamReg_0._0.DataRepository
{
    public class ExamRoomRepository
    {
        private SqlConnection conn = new SqlConnection("Data Source=DESKTOP-K4B953D;Initial Catalog=ExamManagerApplication;Integrated Security=True");
        
        
        /*
          * Lấy danh sách phòng thi từ sql server
        */
        public List<ExamRoom> GetExamRooms(string examlocation, int examorder, out string out_mess)
        {
            out_mess = "";
            List<ExamRoom> data = new List<ExamRoom>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SHOW_EXAM_ROOM", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IN_EXAM_LOCATION", SqlDbType.NVarChar, 50);
                cmd.Parameters["@IN_EXAM_LOCATION"].Value = examlocation;
                cmd.Parameters.Add("@IN_EXAM_ORDER", SqlDbType.Int);
                cmd.Parameters["@IN_EXAM_ORDER"].Value = examorder;
                cmd.Parameters.Add("@OUT_MESSAGE", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string x = reader[1].ToString();
                    x = x.ToString();
                    var erb = new ExamRoom(Convert.ToInt32(reader[0]), x, Convert.ToInt32(reader[2]), Convert.ToInt32(reader[3]));
                    data.Add(erb);
                }
            }
            catch(Exception ex)
            {
                out_mess = ex.Message;
                return null;
            }
            return data;
        }


        private static DataTable CreateDataTable(List<int> ids)
        {
            DataTable table = new DataTable();
            table.Columns.Add("IN_ROOM_ID", typeof(int));
            foreach (int id in ids)
            {
                table.Rows.Add(id);
            }
            return table;
        }
        
        
        /*
          * Xóa phòng thi của kì thi từ sql server
        */
        public void DeleteRooms(List<int> list_id)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE_EXAM_ROOM", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IN_LIST_ROOM_ID", CreateDataTable(list_id));
            cmd.Parameters["@IN_LIST_ROOM_ID"].SqlDbType = SqlDbType.Structured;
            cmd.Parameters["@IN_LIST_ROOM_ID"].TypeName = "LIST_ROOM_ID";
            cmd.ExecuteNonQuery();
        }
        
        
        /*
          * Thêm phòng thi của kì thi từ sql server
        */
        public void AddRoom(int room_order, int computer_number, string room_location, out string out_mess)
        {

            out_mess = "";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("ADD_EXAM_ROOM", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IN_ROOM_LOCATION", SqlDbType.NVarChar, 50);
                cmd.Parameters["@IN_ROOM_LOCATION"].Value = room_location;
                cmd.Parameters.Add("@IN_ROOM_ORDER", SqlDbType.Int);
                cmd.Parameters["@IN_ROOM_ORDER"].Value = room_order;            
                cmd.Parameters.Add("@IN_COMPUTER_NUMBER", SqlDbType.Int);
                cmd.Parameters["@IN_COMPUTER_NUMBER"].Value = computer_number;
                cmd.Parameters.Add("@OUT_MESSAGE", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                out_mess = cmd.Parameters["@OUT_MESSAGE"].Value.ToString();
            }
            catch(Exception ex)
            {
                out_mess = ex.Message;
            }
        }
        
        
        /*
          * Lấy địa điểm thi của kì thi từ sql server
        */
        public AjaxResult GetRoomLocation()
        {
            AjaxResult ajaxResult = new AjaxResult();
            List<string> listroom = new List<string>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SHOW_ROOM_LOCATION", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string x = reader[0].ToString();
                    x = x.ToString();
                    listroom.Add(x);
                }
            }
            catch(Exception ex)
            {
                ajaxResult.Succeed = false;
                ajaxResult.Message = ex.Message;
            }
            ajaxResult.Data = listroom;
            return ajaxResult;
        }
    }
}
