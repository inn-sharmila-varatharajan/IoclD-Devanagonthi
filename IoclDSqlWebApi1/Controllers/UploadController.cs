using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;

namespace IoclDSqlWebApi1.Controllers
{

	public class UploadController : Controller
	{
		// GET: Home
		public ActionResult Upload()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Upload(HttpPostedFileBase postedFile)
		{
			string filePath = string.Empty;
			if (postedFile != null)
			{
				string path = Server.MapPath("~/Uploads/");
				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);
				}

				filePath = path + Path.GetFileName(postedFile.FileName);
				string extension = Path.GetExtension(postedFile.FileName);
				postedFile.SaveAs(filePath);

				//Create a DataTable.
				DataTable dt = new DataTable();
				dt.Columns.AddRange(new DataColumn[6] { new DataColumn("TruckNumber", typeof(string)),
					new DataColumn("DriverName", typeof(string)),
					new DataColumn("DriverType", typeof(string)),
					new DataColumn("TransporterName", typeof(string)),
								new DataColumn("FasttagID", typeof(string)),
								new DataColumn("NFCID",typeof(string)) });


				//Read the contents of CSV file.
				string csvData = System.IO.File.ReadAllText(filePath);

				//Execute a loop over the rows.
				foreach (string row in csvData.Split('\n'))
				{
					if (!string.IsNullOrEmpty(row))
					{
						dt.Rows.Add();
						int i = 0;

						//Execute a loop over the columns.
						foreach (string cell in row.Split(','))
						{
							// Debug.WriteLine(cell);
							dt.Rows[dt.Rows.Count - 1][i] = cell;
							i++;
						}
					}
				}

				string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
				try
				{
					using (SqlConnection con = new SqlConnection(conString))
					{
						using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
						{
							sqlBulkCopy.DestinationTableName = "dbo.trucks";
							sqlBulkCopy.ColumnMappings.Add("TruckNumber", "Truck Number");
							sqlBulkCopy.ColumnMappings.Add("DriverName", "Driver Name");
							sqlBulkCopy.ColumnMappings.Add("DriverType", "Owner Type");
							sqlBulkCopy.ColumnMappings.Add("TransporterName", "Transporter Name");
							sqlBulkCopy.ColumnMappings.Add("FasttagID", "Fastag ID");
							sqlBulkCopy.ColumnMappings.Add("NFCID", "NFC ID");
							con.Open();
							sqlBulkCopy.WriteToServer(dt);
							con.Close();
						}
					}
				}
				catch(Exception ex)
				{
					Debug.WriteLine( ex.ToString());
				}
			}

			return RedirectToAction("TruckPage", "Home");
		}

	}



}