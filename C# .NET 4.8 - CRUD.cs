using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Librerías
using System.Data;           // Usar Clase DataTable
using System.Data.SqlClient; // Microsoft SQL Server
using System.Data.OleDb;     // Access (2003/2007-2013)
using System.Data.SQLite;    // SQLite
using MySqlConnector;        // MySQL
using System.Windows.Forms;  // Usar opciones/componentes de WinForms
using System.IO;             // Usar Clase MemoryStream
using System.Drawing;        // Usar opciones/componentes de WinForms (Colores, PictureBox, etc...)

namespace [Proyecto]
{
    public class Conn // Clase para procesos CRUD
    {
		/*
		ConnectionString: (https://www.connectionStrings.com/)
		la cadena de conexión o ConnectionString deberá estar entre las comillas
		(Directorio) será cambiada por |DataDirectory|\ si es una base de datos local
		
		SQL Server LocalDB:
		Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\[Database].mdf; Integrated Security=True
		
		Access
		2003
		Provider=Microsoft.Jet.OLEDB.4.0; Data Source=(Ubicación)\[Database].mdb
		Provider=Microsoft.Jet.OLEDB.4.0; Data Source=(Ubicación)\[Database].mdb; Jet OLEDB:Database Password=[Password]
		2007-2013
		Provider=Microsoft.ACE.OLEDB.12.0; Data Source=(Ubicación)\[Database].acCRUD
		Provider=Microsoft.ACE.OLEDB.12.0; Data Source=(Ubicación)\[Database].acCRUD; Jet OLEDB:Database Password=[Password]
		
		SQLite
		Data Source=(Ubicación)\[Database].db; Version=3;
		
		MySQL
		Server=[ServerAddress]; Database=[Database]; Uid=[User]; Pwd=[Password]; -- Estandar
		Server=[ServerAddress]; Port=[#Port000]; Database=[Database]; Uid=[User]; Pwd=[Password]; -- Especificar n° puerto TCP
		Server=[ServerAddress], [ServerAddress], [ServerAddress]; Database=[Database]; Uid=[User]; Pwd=[Password; -- Servidores múltiples
		*/
		
		/*
		Iniciar la conexión y almacenar la cadena de conexión (ConnectionString)
		C# no admite la opción de usar variables para almacenar ConnectionString con el metodo actual
		*/
		private readonly SqlConnection Conexion = new SqlConnection(@"[ConnectionString]");
		/*
		Se puede utilizar este método para solo declarar la cadena/connectionStrings, pero en tal caso,
		deberá de declararse: var Conexion = new SqlConnection(Cadena); al principio de cada método que 
		utlice la variable [Conexion]
		*/
		private readonly string CoString = @"[ConnectionString]";
		
        // Iniciar y probar conexión con base de datos
		bool EConn;
        public void TryCon()
        {
            EConn = false;
            while (EConn == false)
            {
                using (var Conexion = new SqlConnection(CoString))
				{
					try
					{
						Conexion.Open();
						EConn = true;
						Conexion.Close();
					}
					catch (Exception ex)
					{
						EConn = false;
						if (MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
						{
							Environment.Exit(0);
						}
					}
				}
            }
        }
		// Instrucciones SQL: SELECT para mostrar datos en un DataGridView, cargar BindingSource para ReportViewer
		public List<ClaseTabla> Cargar()
        {
            List<ClaseTabla> Lista = new List<ClaseTabla>();
            using (var Conexion = new SqlConnection(CoString))
            {
                Conexion.Open();
                try
                {
                    string QSel = "SELECT * FROM [Tabla]";
                    using (SqlCommand CMD = new SqlCommand(QSel, Conexion))
					{
						CMD.CommandType = CommandType.Text;
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
                }
                catch (Exception ex)
                {
                    Lista = new List<ClaseTabla>();
					MessageBox.Show(ex.Message);
                }
                Conexion.Close();
            }
            return Lista;
        }
        /*
        Instrucciones SQL: SELECT utilizable para:
		- Verificar existencia de un registro
		- Login con usuario y/o contraseña
        */
		public bool RegEx(string/long/bool [Columna])
        {
			bool Read = new bool();
			using (var Conexion = new SqlConnection(CoString))
            {
                Conexion.Open();
                try
                {
                    string QSel = "SELECT * FROM [Tabla] WHERE ([Columna] = @[Columna])";
                    using (SqlCommand CMD = new SqlCommand(QSel, Conexion))
					{
						CMD.Parameters.AddWithValue("@[Columna]", [Columna]);
						using (SqlDataReader DR = CMD.ExecuteReader())
						{
							if (DR.HasRows)
							{
								Read = true;
							}
							else
							{
								Read = false;
							}
						}
					}
                }
                catch (Exception ex)
                {
                    Read = false;
					MessageBox.Show(ex.Message);
                }
                Conexion.Close();
            }
			return Read;
        }
		// Instrucciones SQL: SELECT (COUNT, MAX, MIN) para mostrar datos en un TextBox
		public string TBxText(TextBox TBx)
        {
            string Output = string.Empty;
			using (var Conexion = new SqlConnection(CoString))
            {
                Conexion.Open();
                try
                {
                    string QSel = "SELECT COUNT(*) FROM [Tabla]";
                    using (SqlCommand CMD = new SqlCommand(QSel, Conexion))
					{
						var Result = CMD.ExecuteScalar();
						if (Result != null)
						{
							Output = Result.ToString();
						}
					}
                }
                catch (Exception ex)
                {
                    string Output = string.Empty;
					MessageBox.Show(ex.Message);
                }
                Conexion.Close();
            }
			return Output;
        }
        // Instrucciones SQL: SELECT para rellenar un ComboBox
		public void RelleCBx(ComboBox CBx)
        {
            List<string> Lista = new List<string>();
            using (var Conexion = new SqlConnection(CoString))
            {
                Conexion.Open();
                try
                {
                    string QSel = "SELECT [Columna] FROM [Tabla] ORDER BY [Columna] ASC";
                    using (SqlCommand CMD = new SqlCommand(QSel, Conexion))
					{
						CMD.CommandType = CommandType.Text;
						using (SqlDataReader DR = CMD.ExecuteReader())
						{
							while (DR.Read())
							{
								Lista.Add(DR["[Columna]"].ToString());
							}
						}
					}
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Lista = new List<string>();
                }
                Conexion.Close();
            }
            CBx.DataSource = Lista;
        }
		public bool Done;
		// Instrucciones SQL: INSERT, UPDATE y DELETE, DROP y TRUNCATE TABLE con parámetros incluidos
		public void InUpDe(string [Columna 1], int [Columna 2], byte[] [Columna3])
        {
            using (var Conexion = new SqlConnection(CoString))
            {
                Conexion.Open();
				try
				{
					string QIns = "INSERT INTO [Tabla] ([Columna 1], [Columna 3]) VALUES (@[Columna 1], @[Columna 3])";
					string QUpd = "UPDATE [Tabla] SET [Columna 1] = @[Columna 1], [Columna 3] = @[Columna 3] WHERE ([Columna 2] = @[Columna 2])";
					string QDel = "DELETE FROM [Tabla] WHERE ([Columna 2] = @[Columna 2])";
					using (SqlCommand CMD = new SqlCommand([Variable], Conexion))
					{
						CMD.Parameters.AddWithValue("@[Columna]", [Columna]);
						CMD.Parameters.AddWithValue("@[Columna]", [Columna]);
						CMD.Parameters.AddWithValue("@[Columna]", [Columna]);
						CMD.ExecuteNonQuery();
						Done = true;
					}
				}
				catch (Exception ex)
				{
					Done = false;
					MessageBox.Show(ex.Message);
				}
				Conexion.Close();
            }
        }
		
		// Ejecutar una instrucción sql guardada en un arhivo SQL (*.sql)
		public void ImportData()
		{
			using (var Conexion = new SqlConnection(CoString))
			{
				Conexion.Open();
				try
				{
					string QIns = File.ReadAllText("[Archio].sql");
					using (SqlCommand CMD = new SqlCommand(QIns, Conexion))
					{
						long rowsAffected = CMD.ExecuteNonQuery();
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
				Conexion.Close();
			}
		}
    }
}
