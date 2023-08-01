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
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;

namespace IoclDSqlWebApi1.Controllers
{
    //[RoutePrefix("")]
}

public class GetCardController : ApiController
{
    public string InntegrateDbConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    public static MqttClient _mqttClient = null;
    [HttpGet]
    [Route("api/GetCardController/GetDeviceid1/{Date}", Name = "Device1")]
 



    public Array GetDeviceid1(string Date)
    {
        var path = ConfigurationManager.AppSettings["urlPath"];


        try
        {
            using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "mss_getcarddatas";
                cmd.Parameters.AddWithValue("@date", Date);
                
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
                    DeviceName = item["Device"],

                    DeviceId = item["Deviceid"],

                    eventcode40 = item["count40"],
                    eventcode41 = item["count41"],
                   // status =item["Status"]
                    //Start_Time = item["Start Time"],
                    //End_Time = item["End Time"],
                    //Rate = item["Rate"]


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
    [Route("api/GetCardController/Getproductioncount/{Date}")]




    public Array Getproductioncount(string Date)
    {
        var path = ConfigurationManager.AppSettings["urlPath"];


        try
        {
            using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[mss_getprdcount]";
                cmd.Parameters.AddWithValue("@date", Date);

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
                    Total = item["total"],
                    rph = item["rph"],
                    percentage =item["actual"],


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
    [Route("api/GetCardController/GetAverage/{Date}/{deviceid}")]
    public Array GetAverage(string Date,int deviceid)
    {
        var path = ConfigurationManager.AppSettings["urlPath"];
        int cc, dc;

        try
        {
            using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "mss_RPH";
                cmd.Parameters.AddWithValue("@date", Date);
                cmd.Parameters.AddWithValue("@deviceid", deviceid);


                con.Open();
                DataSet ds = new DataSet();
                DataTable countdt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                da.Dispose();
                cmd.Dispose();
                con.Close();
                if (deviceid == 31)
                {
                     cc = 0;
                     dc = 0;
                }
                else { 
                 cc = int.Parse(ds.Tables[1].Rows[0][1].ToString());
                 dc = int.Parse(ds.Tables[1].Rows[0][0].ToString());
            }
                var result = ds.Tables[0].AsEnumerable().Select(item => new
                {
                    rph = item["rph"],

                    standard = item["standard"],

                    deviation = item["deviation"],
                    count = item["count"],
                    dc = dc,
                    cc=cc


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
    [Route("api/GetCardController/Setvalue/{value}/{deviceid}")]
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
    [Route("api/GetCardController/GetRPHValues/{Date}")]
    public Array GetRPHValues(string Date)
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
                cmd.Parameters.AddWithValue("@date", Date);
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
                    rph = item["rph"],

                  

                    status = item["deviation"],

                    carousel =item["p1"],
                    livecount= item["p4"],
                    actual=item["actual"]
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
    [Route("api/GetCardController/Sendmail/")]
    public void Sendmail()
    {
        try
        {
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress("sharmilav25@gmail.com");
            msg.To.Add("iyarkaiyoduvalvom@gmail.com");
            msg.Subject = "test";
            msg.Body = "Test Content";
            //msg.Priority = MailPriority.High;


            using (SmtpClient client = new SmtpClient())
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = true;
                client.Credentials = new NetworkCredential("sharmilav25@gmail.com", "sharmila25pra");
                
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                client.Send(msg);
            }
        }
        catch(Exception ex)
        {
           Console.WriteLine(ex.ToString());
        }
    }



    [HttpGet]
    [Route("api/GetCardController/Getcylineralert")]
    public Array Getcylineralert()
    {
        var path = ConfigurationManager.AppSettings["urlPath"];


        try
        {
            using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "mss_cylidertraffic";
                cmd.Parameters.AddWithValue("@date", DateTime.Now.AddSeconds(-100));
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
                    deviceid = item["deviceid"],
                    traffic= item["traffic"]


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



    private static void ConnectToMqtt()
    {
        try
        {
            if (_mqttClient == null || _mqttClient.IsConnected == false)
            {
                var clientId = Guid.NewGuid().ToString();
                _mqttClient = new MqttClient("192.168.1.222", 1883, false, null, null, MqttSslProtocols.None);
                _mqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;
                _mqttClient.Connect(clientId, "ain-iocl-dev-usr", "@parinnosys123");
            }
        }
        catch (Exception)
        {
        }
    }
    private static void DisconnectMQTT()
    {
        if (_mqttClient != null)
        {
            if (_mqttClient.IsConnected)
                _mqttClient.Disconnect();
            _mqttClient = null;
            System.Threading.Thread.Sleep(1000);
        }
    }


    private static void MqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        Debug.WriteLine("Received = " + Encoding.UTF8.GetString(e.Message) + " on topic " + e.Topic);
       
    }



    [HttpGet]
    [Route("api/GetCardController/GetShopFloorDatas/{Date}")]
    public Array GetShopFloorDatas(String Date)
    {
        var path = ConfigurationManager.AppSettings["urlPath"];


        try
        {
            using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "mss_shopfloor";
                cmd.Parameters.AddWithValue("@date", Date);
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
                    deviceid = item["deviceid"],
                    rpm = item["count"],
                    time =item["time"],
                    status=item["status"],
                    offline=item["timediff"],
                    direction=item["direction"],
                    livecount = item["livecount"]


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
	[Route("api/GetCardController/CheckStatusfromDB/{deviceid}")]
	public  string CheckStatusfromDB(int deviceid)
	{
		int annunciatorid = 0;
		try
		{
			using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
			{
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = con;
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "Checktrafficstarvation";
				cmd.Parameters.AddWithValue("@device", deviceid);
				con.Open();
				DataSet ds = new DataSet();
				DataTable countdt = new DataTable();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(ds);
				da.Dispose();
				cmd.Dispose();
				con.Close();

				//assigning deviceid for annunciator.
				


				 if ( deviceid == 114 || deviceid == 17 || deviceid == 51 ||
						deviceid == 20 || deviceid == 28 || deviceid == 33)
				{
					annunciatorid = 121;
				}
				else if (deviceid == 109 || deviceid == 36 || deviceid == 107 )
				{
					annunciatorid = 122;
				}
				else if ( deviceid == 106 || deviceid == 108
					|| deviceid == 22 || deviceid == 43 || deviceid == 48 || deviceid == 16 || deviceid == 14)
				{
					annunciatorid = 123;
				}
				else if (deviceid == 112 || deviceid == 111 || deviceid == 32 || deviceid == 115
					)
				{
					annunciatorid = 124;
				}
				else if ( deviceid == 119 || deviceid == 18 || deviceid == 21
					|| deviceid == 26 || deviceid == 25)
				{
					annunciatorid = 125;
				}
				else if (deviceid == 37 || deviceid == 101 || deviceid == 117 )
				{
					annunciatorid = 126;
				}
				else if (deviceid == 57 || deviceid == 65 || deviceid == 12)
				{
					annunciatorid = 127;
				}
				else if (deviceid == 116 )
				{
					annunciatorid = 128;
				}
				else if (deviceid == 53 || deviceid == 63 || deviceid == 44)
				{
					annunciatorid = 129;
				}
				else if (deviceid == 105 )
				{
					annunciatorid = 130;
				}

				if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
				{
                    ConnectToMqtt();
					if (_mqttClient.IsConnected)
					{
						_mqttClient.Subscribe(new[] { "AI/IIOT/IOCLD/CCM/ANNUNCIATOR/" + annunciatorid + "/CLEAR" }, new[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });

					}

					if (_mqttClient != null && _mqttClient.IsConnected)
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/ANNUNCIATOR/" + annunciatorid + "/EVENT", Encoding.UTF8.GetBytes("CLEAR"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
					//_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/ANNUNCIATOR/"+annunciatorid+"/", Encoding.UTF8.GetBytes("CLEAR"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);

				}
				else if (ds.Tables[0].Rows.Count != 0 || ds.Tables[1].Rows.Count != 0)
				{
					ConnectToMqtt();
					if (_mqttClient.IsConnected)
					{
						_mqttClient.Subscribe(new[] { "AI/IIOT/IOCLD/CCM/ANNUNCIATOR/" + annunciatorid + "/EVENT" }, new[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });

					}

					if (_mqttClient != null && _mqttClient.IsConnected)
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/ANNUNCIATOR/" + annunciatorid + "/EVENT", Encoding.UTF8.GetBytes("EVENT"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
					//	_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/ANNUNCIATOR/"+annunciatorid+"/", Encoding.UTF8.GetBytes("EVENT"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);

				}
				return ds.Tables[0].Rows.Count.ToString();

			}
		}
		catch (Exception e)
		{
            return e.StackTrace;
		}

	}



	[HttpGet]
	[Route("api/GetCardController/CheckNfcStatus/{deviceid}/{nfcid}")]
	public List<NFCDatas> CheckNfcStatus(int deviceid,String nfcid)
	{

        List<NFCDatas> model= new List<NFCDatas>();
     	try
        {
            using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "mss_truckjsondata";
                cmd.Parameters.AddWithValue("@device", deviceid);
				cmd.Parameters.AddWithValue("@nfc", nfcid);
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
        catch (Exception e)
        {
            return null;
        }

	}



	[HttpGet]
    [Route("api/GetCardController/GetDiverterdirection/{device}")]
    public Array GetDiverterdirection(String device)
    {

        device = device.Replace("gbtn", "");
        device = device.Replace("g", "");
        try
        {
            ConnectToMqtt();
            if (_mqttClient.IsConnected)
            {
                _mqttClient.Subscribe(new[] { "AI/IIOT/IOCLD/CCM/DCONTROL/"+device }, new[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
               
            }

            if (_mqttClient != null && _mqttClient.IsConnected)
                _mqttClient.Publish($"AI/IIOT/IOCLD/CCM/DCONTROL/" + device, Encoding.UTF8.GetBytes( "{\"Div\":2}" ), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);


        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return new Array[] { };
      
    }



}

public class NFCDatas
{

	public string driverName { get; set; }

	
	
	public string vehno { get; set; }
	public string nfc { get; set; }
    public string intime { get; set; }
	public string outtime { get; set; }
	public string duration { get; set; }
	public string count { get; set; }
	public string vendor { get; set; }
	public string type { get; set; }
	public string transportername { get; set; }

}

