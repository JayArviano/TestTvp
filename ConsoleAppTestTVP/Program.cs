using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ConsoleAppTestTVP
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			IEnumerable<long> ListId = new List<long>
			{
				4,
				3,
				2,
				1
			};

			ReleaseProjectHold(ListId);

			Console.ReadKey();

		}

		public static void ReleaseProjectHold(IEnumerable<long> ListId)
		{
			//Checking sql
			string conString = "Data Source=DESKTOP-BTC8QL7;initial catalog=Jajal;Trusted_Connection=True;";
			try
			{
				using (SqlConnection con = new SqlConnection(conString))
				{
					con.Open();
					using (SqlCommand cmd = new SqlCommand("[dbo].[sp_jajal_TVP]", con))
					{
						cmd.CommandType = CommandType.StoredProcedure;
						SqlParameter tvpParam = cmd.Parameters.AddWithValue("@InsertVar_TVP", CreateDataTable(ListId));
						tvpParam.SqlDbType = SqlDbType.Structured;
						tvpParam.TypeName = "dbo.Var_TVP";

						using (SqlDataReader dr = cmd.ExecuteReader())
						{
							if (dr.HasRows)
							{
								while (dr.Read())
								{
									Console.WriteLine(dr["ID"].ToString() + " " + dr["StringAdd"].ToString());
								}
							}
						}

					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.StackTrace);
			}

		}

		private static DataTable CreateDataTable(IEnumerable<long> ids)
		{
			DataTable table = new DataTable();
			table.Columns.Add("ID", typeof(long));
			foreach (long id in ids)
			{
				table.Rows.Add(id);
			}
			return table;
		}
	}
}
