using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Data;

public class LogErrors
{
    private static string _connstr = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;

    public void InsertErrors(string ErMsg, string ErSrc, string StackTrace, string MethodName)
    {
        // connect to database and record the error
        using (SqlConnection conn = new SqlConnection(_connstr))
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "InsertIntoAppErrors";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@ErrorSrc", SqlDbType.VarChar).Value = ErMsg;
                cmd.Parameters.Add("@ErrorMsg", SqlDbType.VarChar).Value = ErSrc;
                cmd.Parameters.Add("@MethodName", SqlDbType.VarChar).Value = MethodName;
                cmd.Parameters.Add("@StackTrace", SqlDbType.VarChar).Value = StackTrace;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                // if the connection to the database fails, write it to text file. 
                using (StreamWriter sw = File.AppendText("Error_Log.txt"))
                {
                    sw.WriteLine(DateTime.Now + "\t" + e.Message);
                }
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
