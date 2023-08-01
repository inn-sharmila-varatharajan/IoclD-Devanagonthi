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
using System.Web.Http.Results;
using System.Threading.Tasks;
using System.Threading;

namespace IoclDSqlWebApi1.Controllers
{
    //[RoutePrefix("")]
}

public class GetDeviceController : ApiController
{
	public static MqttClient _mqttClient = null;
	
	public string InntegrateDbConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
	[HttpGet]
	[Route("api/GetDeviceController/Getstatus/{Carousel}/{devicetype}/{status}")]


	public Array Getstatus(string Carousel, string devicetype, string status)
	{
		var path = ConfigurationManager.AppSettings["urlPath"];
		Array result1= new Array[] { };
		var json= status.ToUpper();
		try
		{
			ConnectToMqtt();


			if (_mqttClient != null && _mqttClient.IsConnected)
			{
				
				if (Carousel == "1")
				{
					if (devicetype == "Counter" || devicetype == "ALL")
					{
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/VCOUNTER/095/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/VCOUNTER/096/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/DCOUNTER/114/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/COUNTER/017/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/COUNTER/051/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/COUNTER/020/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/COUNTER/028/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/COUNTER/033/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/DCOUNTER/109/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/COUNTER/036/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/DCOUNTER/107/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/VCOUNTER/092/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);

					}
					if (devicetype == "Annunciator" || devicetype == "ALL")
					{
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/ANNUNCIATOR/121/EVENT/", Encoding.UTF8.GetBytes("{\"ENABLE\"}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/ANNUNCIATOR/122/EVENT/", Encoding.UTF8.GetBytes("{\"ENABLE\"}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);

					}
				}

				else if (Carousel == "2")
				{
					if (devicetype == "Counter" || devicetype == "ALL")
					{
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/VCOUNTER/093/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/VCOUNTER/089/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/DCOUNTER/106/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/DCOUNTER/108/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/COUNTER/022/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/COUNTER/043/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/COUNTER/048/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/COUNTER/016/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/COUNTER/014/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);

						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/DCOUNTER/112/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/DCOUNTER/111/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/COUNTER/032/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/DCOUNTER/115/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/VCOUNTER/082/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/VCOUNTER/086/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/VCOUNTER/085/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);

					}
					if (devicetype == "Annunciator" || devicetype == "ALL")
					{
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/ANNUNCIATOR/123/EVENT/", Encoding.UTF8.GetBytes("{\"ENABLE\"}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/ANNUNCIATOR/124/EVENT/", Encoding.UTF8.GetBytes("{\"ENABLE\"}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);

					}
				}

				else if (Carousel == "3")
				{
					if (devicetype == "Counter" || devicetype == "ALL")
					{
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/VCOUNTER/088/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/VCOUNTER/091/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/DCOUNTER/119/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/COUNTER/018/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/COUNTER/021/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/COUNTER/025/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/COUNTER/037/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/DCOUNTER/101/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);

						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/DCOUNTER/117/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/VCOUNTER/081/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/VCOUNTER/084/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);

					}
					if (devicetype == "Annunciator" || devicetype == "ALL")
					{
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/ANNUNCIATOR/125/EVENT/", Encoding.UTF8.GetBytes("{\"ENABLE\"}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/ANNUNCIATOR/126/EVENT/", Encoding.UTF8.GetBytes("{\"ENABLE\"}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);

					}
				}
				else
				{
					if (devicetype == "Counter" || devicetype == "ALL")
					{
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/VCOUNTER/090/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/VCOUNTER/098/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/DCOUNTER/104/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/DCOUNTER/103/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/COUNTER/040/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/COUNTER/026/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/COUNTER/031/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/COUNTER/057/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);

						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/COUNTER/065/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/COUNTER/012/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/DCOUNTER/116/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/COUNTER/053/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/COUNTER/063/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/COUNTER/044/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/DCOUNTER/105/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/VCOUNTER/097/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/VCOUNTER/099/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/VCOUNTER/094/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/VCOUNTER/131/", Encoding.UTF8.GetBytes("{\"" + json + "\":2}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);

					}
					if (devicetype == "Annunciator" || devicetype == "ALL")
					{
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/ANNUNCIATOR/127/EVENT/", Encoding.UTF8.GetBytes("{\"ENABLE\"}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/ANNUNCIATOR/128/EVENT/", Encoding.UTF8.GetBytes("{\"ENABLE\"}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/ANNUNCIATOR/129/EVENT/", Encoding.UTF8.GetBytes("{\"ENABLE\"}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
						_mqttClient.Publish($"AI/IIOT/IOCLD/CCM/ANNUNCIATOR/130/EVENT/", Encoding.UTF8.GetBytes("{\"ENABLE\"}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);

					}
				}
			}

			if (status == "Enable")
			{
				Thread.Sleep(35000);
			}
			else
			{
				Thread.Sleep(5000);
			}

			

			using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
			{
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = con;
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "mss_devicestatus";
				cmd.Parameters.AddWithValue("@carousel", "1");
					cmd.CommandTimeout = 600;
				con.Open();
				DataTable countdt = new DataTable();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(countdt);
				da.Dispose();
				cmd.Dispose();
				con.Close();


				var result = countdt.AsEnumerable().Select(item => new
				{

					device = item["device"],
					counterstatus = item["counterstatus"],
					annunstatus = item["Annun"]



				}).ToArray();


					result1 = result;


					//return result;
				}
			

			
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.ToString());
			return new Array[] { };
		}

		return result1;
	}
		
	private static void ConnectToMqtt()
	{
		try
		{
			if (_mqttClient == null || _mqttClient.IsConnected == false)
			{
				var clientId = Guid.NewGuid().ToString();
				_mqttClient = new MqttClient("13.235.99.227", 1883, false, null, null, MqttSslProtocols.None);
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


	
}

