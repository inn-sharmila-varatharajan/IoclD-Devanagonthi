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
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Text;
using System.Diagnostics;
using System.Drawing;
using System.Xml.Linq;
using Microsoft.Ajax.Utilities;
using System.Web;
using System.Net.NetworkInformation;

namespace IoclDSqlWebApi1.Controllers
{
    //[RoutePrefix("")]
}

public class GetConfigController : ApiController
{
	public static MqttClient _mqttClient = null;
	public static string vcrelay ="";
	public string InntegrateDbConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
	[HttpGet]
	[Route("api/GetConfigController/Getconfig/{Carousel}/{devicetype}")]


	public Array Getconfig(string Carousel, string devicetype)
	{
		var path = ConfigurationManager.AppSettings["urlPath"];



		try
		{
			using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
			{
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = con;
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "mss_GetEventData";
				cmd.Parameters.AddWithValue("@carousel", Carousel);
				cmd.Parameters.AddWithValue("@devicetype", devicetype);


				con.Open();
				DataTable countdt = new DataTable();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(countdt);
				da.Dispose();
				cmd.Dispose();
				con.Close();


				var result = countdt.AsEnumerable().Select(item => new
				{

					DeviceId = item["_Device_Id"],



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
	[Route("api/GetConfigController/getjsonconfig/{Carousel}/{devicetype}/{deviceid}")]


	public Array getjsonconfig(string Carousel, string devicetype, string deviceid)
	{
		var jsonstring = "";
		if (devicetype == "Counter")
		{
			jsonstring = "{\"Get_Config\":{\"Ring\":\"\",\"Nozzle\":\"\",\"Shoulder\":\"\",\"EdgeValue\":\"\", \"Com_Shoulder\":\"\",\"Com_Ring\":\"\",\"Com_Nozzle\":\"\",\"Traffic_time\":\"\", \"Conveyor_traffic\": \"\",\"Conveyor_level\": \"\"},\"Device_Definition\":{\"Device_Type\":\"Counter\"}}";
		}

		else if (devicetype == "VCounter")
		{
			jsonstring = "{\"Get_Config\":{\"Ring\":\"\",\"Nozzle\":\"\",\"Shoulder\":\"\",\"EdgeValue\":\"\", \"Com_Shoulder\":\"\",\"Com_Ring\":\"\",\"Com_Nozzle\":\"\",\"Traffic_time\":\"\", \"Conveyor_traffic\": \"\",\"Conveyor_level\": \"\",\"Relay_IP\":\"\",\"Vcounter\": \"\"},\"Device_Definition\":{\"Device_Type\":\"VCounter\"}}";

		}
		else if (devicetype == "DCounter")
		{
			jsonstring = "{\"Get_Config\":{\"Ring\":\"\",\"Nozzle\":\"\",\"Shoulder\":\"\",\"EdgeValue\":\"\", \"Com_Shoulder\":\"\",\"Com_Ring\":\"\",\"Com_Nozzle\":\"\",\"Traffic_time\":\"\", \"Conveyor_traffic\": \"\",\"Conveyor_level\": \"\",    \"Time_Format\": {      \"Stopper_time\": \"\",      \"Lever_time\": \"\"    }},\"Device_Definition\":{\"Device_Type\":\"DCounter\"}}";

		}
		try
		{
			ConnectToMqtt();
			if (_mqttClient.IsConnected)
			{
				_mqttClient.Subscribe(new[] { "AI/IIOT/IOCLD/CCM/" + devicetype.ToUpper() + "/" + deviceid + "/" }, new[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });

			}

			if (_mqttClient != null && _mqttClient.IsConnected)
			{
				_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/" + devicetype.ToUpper() + "/" + deviceid + "/", Encoding.UTF8.GetBytes(jsonstring), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);

			}
			//	_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/ANNUNCIATOR/"+annunciatorid+"/", Encoding.UTF8.GetBytes("EVENT"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);


			System.Threading.Thread.Sleep(1000);

			using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
			{
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = con;
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "[EventCount40]";
				cmd.Parameters.AddWithValue("@deviceid", deviceid);
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
					ring = item["_Ring"],
					nozzle = item["_Nozzle"],
					shoulder = item["_Shoulder"],
					edge = item["_EdgeValue"],
					comring = item["_Com_Ring"],
					comshoulder = item["_Com_Shoulder"],
					comnozzle = item["_Com_Nozzle"],

					ttime = item["_Traffic_time"],
					ctraffic = item["_Conveyor_traffic"],
					clevel = item["_Conveyor_level"],
					relayip = item["_Relay_IP"],
					vcounter = item["_Vcounter"],
					stoptime = item["_Stop_Time"],
					ltime = item["_Lever_Time"]
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

	public static string SetSession(string relay)
	{
		HttpContext.Current.Session["Relay"] = relay;
		return "Hello " + HttpContext.Current.Session["Name"] + Environment.NewLine + "The Current Time is: " + DateTime.Now.ToString();
	}


	[HttpGet]
	[Route("api/GetConfigController/changejsonconfig/{Carousel}/{devicetype}/{deviceid}/{json}")]


	public Array changejsonconfig(string Carousel, string devicetype, string deviceid, string json)
	{
		var jsonstring = "";
		var values = json.Split('!');

		var ips = '"'+  vcrelay.Replace(",",".").ToString()+'"';
		ips = "["+ips+"]";

		

		if (devicetype == "Counter")
		{
			jsonstring = "{\"Change_Config\":{\"Ring\":/" + values[0] + "/,\"Nozzle\":/" + values[1] + "/,\"Shoulder\":/" + values[2] + "/,\"EdgeValue\":/" + values[3] + "/, \"Com_Shoulder\":/" + values[4] + "/,\"Com_Ring\":/" + values[5] + "/,\"Com_Nozzle\":/" + values[6] + "/,\"Traffic_time\":/" + values[7] + "/, \"Conveyor_traffic\": /" + values[8] + "/,\"Conveyor_level\": /" + values[9] + "/},\"Device_Definition\":{\"Device_Type\":\"Counter\"}}";
		}
		else if (devicetype == "VCounter")
		{
			jsonstring = "{\"Change_Config\":{\"Ring\":/" + values[0] + "/,\"Nozzle\":/" + values[1] + "/,\"Shoulder\":/" + values[2] + "/,\"EdgeValue\":/" + values[3] + "/, \"Com_Shoulder\":/" + values[4] + "/,\"Com_Ring\":/" + values[5] + "/,\"Com_Nozzle\":/" + values[6] + "/,\"Traffic_time\":/" + values[7] + "/, \"Conveyor_traffic\": /" + values[8] + "/,\"Conveyor_level\": /" + values[9] + "/,\"Relay_IP\":/" + ips.ToString() + "/,\"Vcounter\": /\"" + values[11].ToString() + "\"/},\"Device_Definition\":{\"Device_Type\":\"VCounter\"}}";
		}
		else if (devicetype == "DCounter")
		{
			jsonstring = "{\"Change_Config\":{\"Ring\":/" + values[0] + "/,\"Nozzle\":/" + values[1] + "/,\"Shoulder\":/" + values[2] + "/,\"EdgeValue\":/" + values[3] + "/, \"Com_Shoulder\":/" + values[4] + "/,\"Com_Ring\":/" + values[5] + "/,\"Com_Nozzle\":/" + values[6] + "/,\"Traffic_time\":/" + values[7] + "/, \"Conveyor_traffic\": /" + values[8] + "/,\"Conveyor_level\": /" + values[9] + "/,\"Time_Format\": {\"Stopper_time\": /" + values[10] + "/,\"Lever_time\": /" + values[11] + "/}},\"Device_Definition\":{\"Device_Type\":\"DCounter\"}}";
		}
		jsonstring = jsonstring.Replace("/", "");
		Debug.WriteLine(jsonstring);
		try
		{
			ConnectToMqtt();
			if (_mqttClient.IsConnected)
			{
				_mqttClient.Subscribe(new[] { "AI/IIOT/IOCLD/CCM/" + devicetype.ToUpper() + "/" + deviceid + "/" }, new[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
			}
			if (_mqttClient != null && _mqttClient.IsConnected)
			{
				_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/" + devicetype.ToUpper() + "/" + deviceid + "/", Encoding.UTF8.GetBytes(jsonstring), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
			}
			//	_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/ANNUNCIATOR/"+annunciatorid+"/", Encoding.UTF8.GetBytes("EVENT"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);


			System.Threading.Thread.Sleep(1000);
			using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
			{
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = con;
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "[EventCount40]";
				cmd.Parameters.AddWithValue("@deviceid", deviceid);
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
					ring = item["_Ring"],
					nozzle = item["_Nozzle"],
					shoulder = item["_Shoulder"],
					edge = item["_EdgeValue"],
					comring = item["_Com_Ring"],
					comshoulder = item["_Com_Shoulder"],
					comnozzle = item["_Com_Nozzle"],
					ttime = item["_Traffic_time"],
					ctraffic = item["_Conveyor_traffic"],
					clevel = item["_Conveyor_level"],
					relayip = item["_Relay_IP"],
					vcounter = item["_Vcounter"],
					stoptime = item["_Stop_Time"],
					ltime = item["_Lever_Time"]
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
	[Route("api/GetConfigController/storeip/{ip}")]


	public string storeip(string ip)
	{
		try
		{
			vcrelay = ip.ToString();



			return ip.ToString();
		}
		catch (Exception ex)
		{
			ex.ToString();
		}
		return "";
	}
}

