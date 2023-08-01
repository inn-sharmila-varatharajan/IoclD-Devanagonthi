using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Mail;

namespace IoclDSqlWebApi1.Controllers
{
    //[RoutePrefix("")]
}

public class GetSettingsController : ApiController
{
    public string InntegrateDbConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    [HttpGet]
    [Route("api/GetSettingsController/GetTargetValue/{date}")]
 
    public Array GetTargetValue(string date)
    {
        var path = ConfigurationManager.AppSettings["urlPath"];


        try
        {
            using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "mss_getrph";
                cmd.Parameters.AddWithValue("@date", date);
               

                con.Open();
                DataSet ds = new DataSet();
                DataTable countdt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                da.Dispose();
                cmd.Dispose();
                con.Close();


                var result = ds.Tables[0].AsEnumerable().Select(item => new
                {
                    device = item["p1"],

                    targetcount = item["standard"],

                    

                }).ToArray();



                return result;

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return new Array[] { };
        }
    }

    [HttpGet]
    [Route("api/GetSettingsController/Setvalue/{value}/{deviceid}")]
    public int Setvalue(string value, int deviceid)
    {
        var path = ConfigurationManager.AppSettings["urlPath"];


        try
        {
            using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "mss_setrph";
                cmd.Parameters.AddWithValue("@value", value);
                cmd.Parameters.AddWithValue("@deviceid", deviceid);
                con.Open();

                DataTable countdt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(countdt);
                da.Dispose();
                cmd.Dispose();
                con.Close();


                return int.Parse(countdt.Rows[0][0].ToString());

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return 0;
        }
    }

    [HttpGet]
    [Route("api/GetSettingsController/Settruckvalue/{value}/{deviceid}")]
    public int Settruckvalue(string value, int deviceid)
    {
        var path = ConfigurationManager.AppSettings["urlPath"];


        try
        {
            using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "mss_settingsvalue";
                cmd.Parameters.AddWithValue("@value", value);
                cmd.Parameters.AddWithValue("@device", deviceid);
                con.Open();

                DataTable countdt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(countdt);
                da.Dispose();
                cmd.Dispose();
                con.Close();


                return int.Parse(countdt.Rows[0][0].ToString());

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return 0;
        }
    }

    [HttpGet]
    [Route("api/GetSettingsController/Gettruckcount/")]
    public Array Gettruckcount()
    {
        var path = ConfigurationManager.AppSettings["urlPath"];


        try
        {
            using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "mss_getsettingsvalue";
                

                con.Open();
                DataSet ds = new DataSet();
                DataTable countdt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                da.Dispose();
                cmd.Dispose();
                con.Close();


                var result = ds.Tables[0].AsEnumerable().Select(item => new
                {
                    device = item["_device_id"],

                    targetcount = item["_count"],



                }).ToArray();



                return result;

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return new Array[] { };
        }
    }


}

