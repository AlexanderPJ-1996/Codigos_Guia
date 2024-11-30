/*
Este es el paso a paso para captar datos de una tabla para alimentar un componente BindingSource
para cargar un ReportViewer
*/

// Clase para representar las columnas de una tabla en una base de datos
using System;

namespace [Proyecto]
{
	public class ClaseTabla
	{
		public int/long [INTEGER/BIGINT] { get; set; }
		public string [VARCHAR] { get; set; }
		public DateTime [DateTime] { get; set; }
		public double [DOUBLE/DECIMAL] { get; set; }
		public bool [BOOLEAN/BIT] { get; set; }
		public byte[] [BYTE] { get; set; }
	}
}

// Clase para crear y cargar una lista con los datos alamcenados en una tabla
using System.Data;           // Usar Clase DataTable
using System.Data.SqlClient; // SQL Server
using System.Data.OleDb;     // Access (2003/2007-2013)
using System.Data.SQLite;    // SQLite
using MySqlConnector;        // MySQL

namespace [Proyecto]
{
	public class Datos_Tabla
	{
		public List<ClaseTabla> Listar()
		{
			List<ClaseTabla> Lista = new List<ClaseTabla>();
			using (SqlConnection Conectar = new SqlConnection(@"[ConnectionString]"))
			{
				try
				{
					string SQL = "[Consulta SQL SELECT]";
					SqlCommand CMD = new SqlCommand(SQL, Conectar);
					CMD.CommandType = CommandType.Text;
					Conectar.Open();
					using (SqlDataReader DR = CMD.ExecuteReader())
					{
						while (DR.Read())
						{
							Lista.Add(new ClaseTabla()
							{
								[INTEGER/BIGINT] = Convert.ToInt32(DR["[INTEGER/BIGINT]"]),
								[VARCHAR] = DR["[VARCHAR]"].ToString(),
								[DOUBLE/DECIMAL] = Convert.ToDouble/ToDecimal(DR["[DOUBLE/DECIMAL]"]),
								[DateTime] = Convert.ToDateTime(DR["[DateTime]"]),
								[BOOLEAN/BIT] = Convert.ToBoolean(DR["[BOOLEAN/BIT]"]),
								[BYTE] = (byte[])DR["BYTE"]
							});
						}
					}
				}
				catch (Exception)
				{
					Lista = new List<ClaseTabla>();
				}
			}
			return Lista;
		}
	}
}

// Configurar Form para cargar datos
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace [Proyecto]
{
	public partial class [Form] : Form
	{
		private readonly Datos_Tabla DT = new Datos_Tabla();
		
		void Cargar()
		{
			[BindingSource].DataSource = DT.Listar();
			[ReportViewer].RefreshReport();
		}
	}
}