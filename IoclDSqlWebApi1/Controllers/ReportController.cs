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
using System.Data.OleDb;
using System.IO;

namespace IoclDSqlWebApi1.Controllers
{
    //[RoutePrefix("")]
}

public class GetReportController : ApiController
{
    public string InntegrateDbConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    [HttpGet]
    [Route("api/GetReportController/GetHourly/{Date}/{deviceid}")]
    public Array GetHourly(string Date,String deviceid)
    {
        var path = ConfigurationManager.AppSettings["urlPath"];


        try
        {
            using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "mss_reports";
                cmd.Parameters.AddWithValue("@fromdate", Date);
                cmd.Parameters.AddWithValue("@todate", Date);
                cmd.Parameters.AddWithValue("@device", deviceid);
                cmd.Parameters.AddWithValue("@type", "hourwise");


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
                    date = item["day"],

                    hour = item["hour"],

                    count = item["maxcount"],
                    

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
	[Route("api/GetReportController/GetHourlySubmit/{Date}/{deviceid}/{from}/{to}")]
	public Array GetHourlySubmit(string Date, String deviceid,string from,string to)
	{
		var path = ConfigurationManager.AppSettings["urlPath"];


		try
		{
			using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
			{
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = con;
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "mss_hourreport";
				cmd.Parameters.AddWithValue("@fromdate", Date);
				cmd.Parameters.AddWithValue("@device", deviceid);
				cmd.Parameters.AddWithValue("@hourfrom", from);
				cmd.Parameters.AddWithValue("@hourto", to);
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
					date = item["day"],

					hour = item["hour"],

					count = item["maxcount"],


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
	[Route("api/GetReportController/GetSensor/{Date}/{Device}")]
	public Array GetSensor(string Date,string Device)
	{
		var path = ConfigurationManager.AppSettings["urlPath"];


		try
		{
			using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
			{
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = con;
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "mss_reports";
				cmd.Parameters.AddWithValue("@fromdate", Date);
				cmd.Parameters.AddWithValue("@todate", Date);
				cmd.Parameters.AddWithValue("@device", Device);
				cmd.Parameters.AddWithValue("@type", "sensor");


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
					cccount = item["cccount"],

					dccount = item["dccount"],

					device = item["device"],

					devicename = item["devicename"],
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
	[Route("api/GetReportController/GetNFC/{Date}")]
	public Array GetNFC(string Date)
	{
		var path = ConfigurationManager.AppSettings["urlPath"];


		try
		{
			using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
			{
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = con;
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "mss_reports";
				cmd.Parameters.AddWithValue("@fromdate", Date);
				cmd.Parameters.AddWithValue("@todate", Date);
				cmd.Parameters.AddWithValue("@device", 0);
				cmd.Parameters.AddWithValue("@type", "nfc");


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
					NFC = item["nfc"],

					
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
    [Route("api/GetReportController/GetMonthly/{Date}")]

    public Array GetMonthly(string Date)
    {
        var path = ConfigurationManager.AppSettings["urlPath"];


        try
        {
            using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "mss_reports";
                cmd.Parameters.AddWithValue("@fromdate", Date);
                cmd.Parameters.AddWithValue("@todate", Date);
                cmd.Parameters.AddWithValue("@device", "0");
                cmd.Parameters.AddWithValue("@type", "monthwise");


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
                    device = item["device"],
                    count = item["maxcount"],


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
    [Route("api/GetReportController/GetDaily/{FromDate}/{ToDate}")]
    public Array GetDaily(string FromDate,string ToDate)
    {
        var path = ConfigurationManager.AppSettings["urlPath"];


        try
        {
            using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "mss_reports";
                cmd.Parameters.AddWithValue("@fromdate", FromDate);
                cmd.Parameters.AddWithValue("@todate", ToDate);
                cmd.Parameters.AddWithValue("@device", "0");
                cmd.Parameters.AddWithValue("@type", "daywise");


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
                    date = item["date"],
                    device = item["carousel"],
                    count = item["maxcount"],
                    cccount = item["cccount"]

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
    [Route("api/GetReportController/GetInterrupt/{device}")]
    public Array GetInterrupt(string device)
    {
        var path = ConfigurationManager.AppSettings["urlPath"];


        try
        {
            using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "mss_interruptreport";
                cmd.Parameters.AddWithValue("@device", device);
            
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
                    devicename = item["_Device_Name"],
                    device = item["_Device_ID"]


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
    [Route("api/GetReportController/GetInterruptdatas/{device}/{date}")]
    public Array GetInterruptdatas(string device,string date)
    {
        var path = ConfigurationManager.AppSettings["urlPath"];


        try
        {
            using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[mss_intrreport]";
                cmd.Parameters.AddWithValue("@device", device);
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
                    intime = item["intime"],
                    outtime= item["outtime"],
                    device = item["_Device_ID"],
                    eventcode =item["_Event_code"],
                    mins= item["mins"]
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
    [Route("api/GetReportController/Gettruckdetails/{date}/{device}")]
    public Array Gettruckdetails(string date, string device)
    {
        var path = ConfigurationManager.AppSettings["urlPath"];


        try
        {
            using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[mss_truckreport]";
                cmd.Parameters.AddWithValue("@device", device);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.CommandTimeout = 600;
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
                    device = item["device"],
                    intime = item["intime"],
                    outtime = item["outtime"],
                    nfc=item["nfc"],
                    count=item["count"],
                    duration=item["duration"],
                    name =item["drivername"],
                    trucknum = item["trucknum"],
                     vendor = item["vendor"],
                      otype = item["otype"],
                       tname = item["tname"],
                    fid = item["fid"]
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




	[HttpPost]
	[Route("api/GetReportController/truckdetailsbydate/{date}")]
	public List<NFCDatas> truckdetailsbydate(string date)
	{
		var path = ConfigurationManager.AppSettings["urlPath"];

		List<NFCDatas> model = new List<NFCDatas>();
		try
		{
			using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
			{
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = con;
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "[mss_truckreport]";
				cmd.Parameters.AddWithValue("@device", "005");
				cmd.Parameters.AddWithValue("@date", date);
				cmd.CommandTimeout = 600;
				con.Open();
				DataSet ds = new DataSet();
				DataTable countdt = new DataTable();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(ds);
				da.Dispose();
				cmd.Dispose();
				con.Close();


				for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
				{
					NFCDatas listOfObjects =
					new NFCDatas
					{
						driverName = ds.Tables[0].Rows[i]["drivername"].ToString(),
						vehno = ds.Tables[0].Rows[i]["trucknum"].ToString(),
						nfc = ds.Tables[0].Rows[i]["nfc"].ToString(),
						outtime = ds.Tables[0].Rows[i]["outtime"].ToString(),
						intime = ds.Tables[0].Rows[i]["intime"].ToString(),
						duration = ds.Tables[0].Rows[i]["duration"].ToString(),
						count = ds.Tables[0].Rows[i]["count"].ToString(),
						vendor = ds.Tables[0].Rows[i]["vendor"].ToString(),
						type = ds.Tables[0].Rows[i]["otype"].ToString(),
						transportername = ds.Tables[0].Rows[i]["tname"].ToString()
					};

					model.Add(listOfObjects);
				}
			}
			return model;

		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.ToString());
			return null;
		}
	}



	[HttpGet]
    [Route("api/GetReportController/GettruckdetailsSearch/{date}/{device}/{vehno}")]
    public Array GettruckdetailsSearch(string date, string device,string vehno)
    {
        var path = ConfigurationManager.AppSettings["urlPath"];


        try
        {
            using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[mss_truckreportsearch]";
                cmd.Parameters.AddWithValue("@device", device);
                cmd.Parameters.AddWithValue("@truck", vehno);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.CommandTimeout = 600;
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
                    device = item["device"],
                    intime = item["intime"],
                    outtime = item["outtime"],
                    nfc = item["nfc"],
                    count = item["count"],
                    duration = item["duration"],
                    name = item["drivername"],
                    trucknum = item["trucknum"],
                    vendor = item["vendor"],
                    otype = item["otype"],
                    tname = item["tname"],
                    fid = item["fid"]
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
    [Route("api/GetReportController/Gettruckcount/{date}/{device}")]
    public Array Gettruckcount(string date, string device)
    {
        var path = ConfigurationManager.AppSettings["urlPath"];


        try
        {
            using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[mss_truckreport]";
                cmd.Parameters.AddWithValue("@device", device);
                cmd.Parameters.AddWithValue("@date", date);
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
                   
                    day = item["day"],
                    hour = item["hour"],
                    count = item["count"],
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
    [Route("api/GetReportController/GetDeviation/{FromDate}/{ToDate}")]
    public Array GetDeviation(string FromDate, string ToDate)
    {
        var path = ConfigurationManager.AppSettings["urlPath"];


        try
        {
            using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[mss_deviationreport]";
                cmd.Parameters.AddWithValue("@fromdate", FromDate);
                cmd.Parameters.AddWithValue("@todate", ToDate);
  

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
                    date = item["date"],
                    device = item["carousel"],
                    deviation = item["deviation"],
                    status= item["status"],

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
    [Route("api/GetReportController/GetInterruptInterval/{device}/{date}")]
    public Array GetInterruptInterval(string device, string date)
    {
        var path = ConfigurationManager.AppSettings["urlPath"];


        try
        {
            using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[mss_intrreport]";
                cmd.Parameters.AddWithValue("@device", device);
                cmd.Parameters.AddWithValue("@date", date);
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
                    in5 = item["in5"],
                    in10 = item["in10"],
                    in15 = item["in15"],
                    in20 = item["in20"],
                    in25 = item["in25"],
                    in30 = item["in30"],
                    intotal = item["intotal"]
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
    [Route("api/GetReportController/Gettruckcountfor3days/{FromDate}/{ToDate}")]
    public Array Gettruckcountfor3days(string FromDate, string ToDate)
    {
        var path = ConfigurationManager.AppSettings["urlPath"];


        try
        {
            using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "mss_Truckcount_for3days";
                cmd.Parameters.AddWithValue("@fromdate", FromDate);
                cmd.Parameters.AddWithValue("@todate", ToDate);


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
                    count1 = item["count1"],
                    count2 = item["count2"],
                    count3 = item["count3"],
                    bayno = item["bayno"],

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
    [Route("api/GetReportController/GetTruckDetails/{vehno}")]
    public Array GetTruckDetails(string vehno)
    {
        var path = ConfigurationManager.AppSettings["urlPath"];

        try
        {
            using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[mss_truckdetails]";
                cmd.Parameters.AddWithValue("@vehno", vehno);



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
                        action = item["Action"],
                        vendor = item["Vendor"],
                        trucknumber = item["Truck Number"],
                        drivername = item["Driver Name"],
                        ownertype = item["Owner Type"],
                        transportername = item["Transporter Name"],
                        tt = item["TT"],
                        fastagid = item["Fastag ID"],
                        fastagenrolmentdt = item["Fastag Enrolment D&T"],
                        nfcid = item["NFC ID"],
                        nfcenrolementdt = item["NFC Enrolment D&T"],
                        isactive = item["Is Active"]




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
    [Route("api/GetReportController/GetTruckDetailsedit/{actionn}/{vendor}/{trucknumber}/{drivername}/{ownertype}/{transportername}/{tt}/{fastagid}/{fastagenrolmentdt}/{nfcid}/{nfcenrolementdt}/{isactive}")]
    public string GetTruckDetailsedit(string actionn,string vendor,string trucknumber,string drivername,string ownertype,string transportername,string tt,string fastagid,string fastagenrolmentdt,string nfcid,string nfcenrolementdt,string isactive)
    {
        var path = ConfigurationManager.AppSettings["urlPath"];

        try
        {
            using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[mss_truckdetailsedit]";
                cmd.Parameters.AddWithValue("@action", actionn);
                cmd.Parameters.AddWithValue("@vendor", vendor);
                cmd.Parameters.AddWithValue("@trucknum", trucknumber);
                cmd.Parameters.AddWithValue("@drivername", drivername.ToString() == "" ? "" : drivername);
                cmd.Parameters.AddWithValue("@ownertype", ownertype.ToString()==""?"":ownertype);
                cmd.Parameters.AddWithValue("@tname", transportername);
                cmd.Parameters.AddWithValue("@tt", tt);
                cmd.Parameters.AddWithValue("@fasttagid", fastagid);
                cmd.Parameters.AddWithValue("@fasttagdt", fastagenrolmentdt);
                cmd.Parameters.AddWithValue("@nfcid", nfcid.ToString()==""?"":nfcid);
                cmd.Parameters.AddWithValue("@nfcenroll", nfcenrolementdt);
                cmd.Parameters.AddWithValue("@isactive", isactive);

                con.Open();
                DataSet ds = new DataSet();
                DataTable countdt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                da.Dispose();
                cmd.Dispose();
                con.Close();
                return "";



            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return "";
        }
    }

	
}



