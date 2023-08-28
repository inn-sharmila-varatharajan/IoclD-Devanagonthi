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

public class GetDashboardController : ApiController
{
    public string InntegrateDbConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

    //public string datetime = DateTime.Now.AddMinutes(-20);
   // public string datetime = "2022-09-22 11:00";
    [HttpGet]
    [Route("api/GetDashboardController/GetLoadingdevicecount/")]
 



    public Array GetLoadingdevicecount()
    {
        var path = ConfigurationManager.AppSettings["urlPath"];


        try
        {
            using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "mss_dashboard";
                cmd.Parameters.AddWithValue("@date", DateTime.Now.AddMinutes(-40));
              
                


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
                    unloading = item["unloadcount"],

                    loading = item["loadcount"],

                 
                    

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
    [Route("api/GetDashboardController/GetunloadTruckcount/")]




    public Array GetunloadTruckcount()
    {
        var path = ConfigurationManager.AppSettings["urlPath"];


        try
        {
            using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "mss_dashboard";
                cmd.Parameters.AddWithValue("@date", DateTime.Now.AddMinutes(-40));

                con.Open();
                DataSet ds = new DataSet();
                DataTable countdt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                da.Dispose();
                cmd.Dispose();
                con.Close();

                var result = ds.Tables[2].AsEnumerable().Select(item => new
                {
                    unloadtruck = item["truckunloading"],

                  
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
    [Route("api/GetDashboardController/GetloadTruckcount/")]




    public Array GetloadTruckcount()
    {
        var path = ConfigurationManager.AppSettings["urlPath"];


        try
        {
            using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "mss_dashboard";
                cmd.Parameters.AddWithValue("@date", DateTime.Now.AddMinutes(-40));

                con.Open();
                DataSet ds = new DataSet();
                DataTable countdt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                da.Dispose();
                cmd.Dispose();
                con.Close();

                var result = ds.Tables[3].AsEnumerable().Select(item => new
                {
                    loadtruck = item["truckloading"],


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
    [Route("api/GetDashboardController/GetloadTrucktarget/")]




    public Array GetloadTrucktarget()
    {
        var path = ConfigurationManager.AppSettings["urlPath"];


        try
        {
            using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "mss_dashboard";
                cmd.Parameters.AddWithValue("@date", DateTime.Now.AddMinutes(-40));

                con.Open();
                DataSet ds = new DataSet();
                DataTable countdt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                da.Dispose();
                cmd.Dispose();
                con.Close();

                var result = ds.Tables[4].AsEnumerable().Select(item => new
                {
                    unloadtarget = item["unloadactual"],
                    loadtarget = item["loadactual"],


                }).ToArray();

                return result;

            }
        }
        catch (Exception)
        {
            return new Array[] { };
        }
    }


    [HttpGet]
    [Route("api/GetDashboardController/GetTruckDeatils/")]




    public Array GetTruckDeatils()
    {
        var path = ConfigurationManager.AppSettings["urlPath"];


        try
        {
            using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "mss_dashboard";
                cmd.Parameters.AddWithValue("@date", DateTime.Now.AddMinutes(-40));

                con.Open();
                DataSet ds = new DataSet();
                DataTable countdt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                da.Dispose();
                cmd.Dispose();
                con.Close();

                var result = ds.Tables[1].AsEnumerable().Select(item => new
                {
                    device = item["_Device_Id"],
                    status =item["status"],
                    vehnum= item["vehnum"]

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

