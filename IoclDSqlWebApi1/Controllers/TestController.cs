using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;


namespace IoclDSqlWebApi1.Controllers
{
}
    public class TestController : ApiController
    {
	public string InntegrateDbConnectionString = ConfigurationManager.ConnectionStrings["WindMill"].ConnectionString;

	public static string Path = ConfigurationManager.AppSettings["Path"].ToString();

    [HttpGet]
        [Route("api/TestController/GetDetails/{content}")]
        public string GetDetails(string content)
        {
        try
        {

            using (var stream = new FileStream(
           Path, FileMode.Append, FileAccess.Write, FileShare.Write, 4096))
            {
                var bytes = Encoding.UTF8.GetBytes(Environment.NewLine + content);
                stream.Write(bytes, 0, bytes.Length);
            }
            return "ok";
        }
        catch(Exception ex)
        {
            ex.ToString();
            return ex.ToString();
        }
      
             
        }


    [HttpPost]
    [Route("api/TestController/GetDetailspost/{content}")]
    public string GetDetailspost(string content)
    {
        try
        {

            using (var stream = new FileStream(
           Path, FileMode.Append, FileAccess.Write, FileShare.Write, 4096))
            {
                var bytes = Encoding.UTF8.GetBytes(Environment.NewLine + content);
                stream.Write(bytes, 0, bytes.Length);
            }

            var str = content.Split(']');
            var deviceid = str[0]+"]" ;

            var gsmno = deviceid.Split('L');

            var devicestatus = gsmno[1].Split('[');


            var time = str[1].Split('T');
            time[1] = time[1].Replace("-", ":");

            var remotetime = time[0].Substring(1, time[0].Length-1) + " "+time[1];

            var datetime = gsmno[0]+"," + devicestatus[0]+ "," + devicestatus[1].Substring(0, devicestatus[1].Length-1) ;



            try
            {
                using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "mss_rawdata";
                    cmd.Parameters.AddWithValue("@device", deviceid);
                    cmd.Parameters.AddWithValue("@remote_time", remotetime);
					cmd.Parameters.AddWithValue("@gsmno", gsmno[0]);
				
					cmd.Parameters.AddWithValue("@deviceid", devicestatus[0]);
					cmd.Parameters.AddWithValue("@devicestatus", devicestatus.Length ==0? "": devicestatus[1].Substring(0, devicestatus[1].Length - 1));


					con.Open();
                    DataSet ds = new DataSet();
                    DataTable countdt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    da.Dispose();
                    cmd.Dispose();
                    con.Close();



                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }


            return remotetime;
        }
        catch (Exception ex)
        {
            ex.ToString();
            return ex.ToString();
        }


    }
}
