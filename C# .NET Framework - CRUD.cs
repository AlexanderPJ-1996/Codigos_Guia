using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Librerías
using System.Data;           // Usar Clase DataTable
using System.IO;             // Usar Clase MemoryStream
using System.Windows.Forms;  // Usar opciones/componentes de WinForms
using System.Drawing;        // Usar opciones/componentes de WinForms (Colores, PictureBox, etc...)
using System.Data.SqlClient; // SQL Server
using System.Data.OleDb;     // Access (2003/2007-2013)
using System.Data.SQLite;    // SQLite
using MySqlConnector;        // MySQL

namespace [Proyecto]
{
    public class Conn //Clase para procesos CRUD
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
		Server=[ServerAddress]; Database=[Database]; Uid=[User]; Pwd=[Password];
		Server=[ServerAddress]; Port=[#Port000]; Database=[Database]; Uid=[User]; Pwd=[Password];
		*/
		
		/*
		Iniciar la conexión y almacenar la cadena de conexión (ConnectionString)
		C# no admite la opción de usar variables para almacenar ConnectionString con el metodo actual
		*/
		private readonly SqlConnection Conectar = new SqlConnection(@"[ConnectionString]");
        // Aprobar o denegar acciones en función del exito con la conexión
        public bool EConn, ERead, Chang;
		
		/*
		Se puede utilizar este método para solo declarar la cadena/connectionStrings, pero en tal caso,
		deberá de declararse: var Conectar = new SqlConnection(Cadena); al principio de cada método que 
		utlice la variable [Conectar]
		*/
		private readonly string Cadena = @"[ConnectionString]";
		
		public void VarConectar()
		{
			var Conectar = new SqlConnection(Cadena);
		}
		
        // Iniciar y probar conexión con base de datos
        public void Conexion()
        {
            EConn = false;
            while (EConn == false)
            {
                try
                {
                    Conectar.Open();
                    EConn = true;
                    Conectar.Close();
                }
                catch (Exception ex)
                {
                    EConn = false;
                    if (MessageBox.Show(ex.Message + Environment.NewLine + String.Empty + Environment.NewLine + "¿Desea reintentar la conexión?", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Cancel)
                    {
                        Environment.Exit(0);
                    }
                }
            }
        }
		
        /*
        Instrucciones SQL: SELECT
        Utilizable para hacer login con usuario y contraseña almacenada en base de datos 
        */
        public void Leer(string SQL)
        {
            Conectar.Open();
            try
            {
                SqlCommand CMD = new SqlCommand(SQL, Conectar);
                SqlDataReader DR = CMD.ExecuteReader();
                if (DR.HasRows)
                {
                    ERead = true;
                }
                else
                {
                    ERead = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            Conectar.Close();
        }
		
        /*
        Instrucciones SQL: SELECT
        Mostrar datos en un DataGridView
        */
        public void Consulta(DataGridView Tabla, string SQL)
        {
            try
            {
                SqlDataAdapter DA = new SqlDataAdapter(SQL, Conectar);
                DataTable DT = new DataTable();
                DA.Fill(DT);
                Tabla.DataSource = DT;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
		
		/*
		Instrucciones SQL: SELECT (COUNT)
		Mostrar Datos en un TextBox 
		*/
		public void TBxText(TextBox TBx, string SQL)
        {
            Conectar.Open();
            try
            {
                SqlCommand CMD = new SqlCommand(SQL, Conectar);
                var Count = CMD.ExecuteScalar();
                if (Count != null)
                {
                    TBx.Text = Count.ToString();
                }
            }
            catch (Exception)
            {

            }
            Conectar.Close();
        }
		
        /*
        Instrucciones SQL: SELECT
        Rellenar un ComboBox
        */
        public DataTable Rellenar(string SQL)
        {
            DataTable DT = new DataTable();
            try
            {
                SqlDataAdapter DA = new SqlDataAdapter(SQL, Conectar);
                DA.Fill(DT);
            }
            catch (Exception ex)
            {
                Conectar.Close();
                MessageBox.Show(ex.Message, String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return DT;
        }
		
        // Instrucciones SQL: INSERT, UPDATE, DELETE, DROP y TRUNCATE TABLE. DataGridView
        public void Operaciones(string SQL)
        {
            Conectar.Open();
            try
            {
                SqlCommand CMD = new SqlCommand(SQL, Conectar);
                CMD.ExecuteNonQuery();
				Chang = true;
            }
            catch (Exception ex)
            {
                Chang = false;
				MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            Conectar.Close();
        }
		
        // Instrucciones SQL: INSERT, UPDATE y DELETE. PictureBox
        public void WorkImgs(PictureBox Imagen, string SQL)
        {
            /*
            Tomar imagen de PictureBox, convertirlo en MemoryStream,
            y después en arreglo de Bytes para ser almacenado en base de datos
            */
            MemoryStream MS = new MemoryStream();
            Imagen.Image.Save(MS, Imagen.Image.RawFormat);
            byte[] Imagenes = MS.GetBuffer();
            // Agregar parámetro para guardar el arreglo de bytes
            SqlCommand CMD = new SqlCommand(SQL, Conectar);
            CMD.Parameters.AddWithValue("@Imagen", Imagenes);
            try
            {
                Conectar.Open();
                CMD.ExecuteNonQuery();
                Conectar.Close();
				Chang = true;
            }
            catch (Exception ex)
            {
                Chang = false;
				MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Conectar.Close();
            }
        }
		
		// Instrucciones SQL: INSERT, UPDATE y DELETE con parámetros incluidos 
		void InUpDe(string [Columna 1], int [Columna 2], byte[] [Columna3])
        {
            using (var Conectar = new SqlConnection(cn.Cadena))
            {
                string SQL = "INSERT INTO [Tabla] ([Columna 1], [Columna 3]) VALUES (@[Columna 1], @[Columna 3])";
				string SQL = "UPDATE [Tabla] SET [Columna 1] = @[Columna 1], [Columna 3] = @[Columna 3] WHERE ([Columna 2] = @[Columna 2])";
				string SQL = "DELETE FROM [Tabla] WHERE ([Columna 2] = @[Columna 2])";
                SqlCommand CMD = new SqlCommand(SQL, Conectar);
                CMD.Parameters.AddWithValue("@[Columna]", [Columna]);
                CMD.Parameters.AddWithValue("@[Columna]", [Columna]);
                CMD.Parameters.AddWithValue("@[Columna]", [Columna]);
                try
                {
                    Conectar.Open();
                    CMD.ExecuteNonQuery();
                    Conectar.Close();
                    Chang = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Conectar.Close();
                    Chang = false;
                }
            }
        }
    }
}

// Librerías estandar de un Form
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Si la clase CRUD, está en otro proyecto, se importa la ibrería de dicho proyecto
using [Proyecto]

namespace [Proyecto]
{
	public partial class [Form] : Form
	{
		// Variable para acceder a procesos y variables de la clase CRUD
		private readonly Conn cn = new Conn();
		/*
		.Net 5.0 en adelante
		private readonly Conn cn = = new();
		*/
		string SQL;
		
		public [Form]()
        {
            InitializeComponent();
        }
		
		// Probar conexión
		void Probar()
		{
			cn.Conexion();
		}
		
		// Consulta SQL: CREATE TABLE para tablas en caso de que estas no existan
		void CreTable()
		{
			SQL = "CREATE TABLE IF NOT EXISTS [Tabla] (([Columna 1] [TipoDato](Longitud) [NULL/NOT NULL], [Columna N] [TipoDato](Longitud) [NULL/NOT NULL], ...);";
			cn.Operaciones(SQL);
		}
		
		// Consulta SQL: SELECT en sin cargar datos en DataGridView
		void ReadRegs()
        {
            SQL = "SELECT * FROM [Tabla]";
			cn.Leer(SQL);
        }
		
		// Consulta SQL: SELECT para cargar datos DataGridView
		void LoadRegs()
		{
			SQL = "SELECT * FROM [Tabla]";
			cn.Consulta([DataGridView], SQL);
		}
		
		/*
		Consultas SQL: SELECT
		Usar CAST([Columna] AS VARCHAR([Longitud])) para busquedas =/LIKE con valores numéricos
		*                | SELECT * FROM [Tabla]
		[Columna]        | SELECT [Columna] FROM [Tabla]
		=                | SELECT * FROM [Tabla] WHERE ([Columna] = '" & [Dato] & "')"
		LIKE             | SELECT * FROM [Tabla] WHERE ([Columna] LIKE '%" & [Dato] & "%')"
		ASC/DESC         | SELECT * FROM [Tabla] ORDER BY [Columna] ASC/DESC
		COUNT()          | SELECT COUNT(*) FROM [Tabla]
		SUM()          | SELECT SUM([Columna]) FROM [Tabla]
		MAX()/MIN()      | SELECT MAX([Columna]) FROM [Tabla]
		*/
		
		// Rellenar ComboBox
		void RelleCBx()
		{
			SQL = "SELECT [Columna] FROM [Tabla] ORDER BY [Columna] ASC/DESC";
			try
            {
                DataTable DT = new DataTable();
				// DataTable DT = new();
                DT = cn.Rellenar(SQL);
                [ComboBox].DataSource = DT;
                [ComboBox].DisplayMember = "[Columna]";
                [ComboBox].SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
		}
		
		// Consultas SQL: INSERT, UPDATE, DELETE, TRUNCATE TABLE
		void InUpDeTru()
		{
			// DataGridView
			// Guardar 
			SQL = "INSERT INTO [Tabla] ([Columna 1], [Columna 2],... [Columna N]) SELECT " + [Dato 1] + ", " + [Dato 2] + ",... " + [Dato N];
			// Actualizar
			SQL = "UPDATE [Tabla] SET [Columna 1] = " + [Dato 1] + ", [Columna 2] = " + [Dato 2] + ",... Columna N = " + [Dato N] + " WHERE ([Criterio] = " + [Criterio] + ")";
			// Eliminar
			SQL = "DELETE FROM [Tabla] WHERE ([Criterio]=" + [Criterio] + ")";
			// Vaciar tablas
			SQL = "TRUNCATE TABLE [Tabla]";
			cn.Operaciones([DataGridView], SQL);
			
			// PictureBox
			// Guardar
			SQL = "INSERT INTO [Tabla] ([Columna 1], [Columna 2],... [Columna N]) VALUES (" + [Dato 1] + ", " + [Dato 2] + ",... " + [Dato N] + ", @Imagen)";
			// Actualizar
			SQL = "UPDATE [Tabla] SET [Columna 1] = " + [Dato 1] + ", [Columna 2] = " + [Dato 2] + ",... Columna N = " + [Dato N] + " [Columna] = @Imagen WHERE ([Criterio] = " + [Criterio] + ")";
			// Eliminar
			SQL = "DELETE FROM [Tabla] WHERE ([Criterio]=" + [Criterio] + ")";
			cn.WorkImgs([PictureBox], SQL);
		}
		
		// Consulta SQL: TRUNCATE TABLE en SQLite
		void TruncarSQLite()
		{
			SQL = "DROP TABLE [Tabla]";
			cn.Operaciones([DataGridView], SQL);
			SQL = "CREATE TABLE [Tabla] ([Columna 1] [TipoDato](Longitud) [NULL/NOT NULL], [Columna N] [TipoDato](Longitud) [NULL/NOT NULL], ...);";
			cn.Operaciones(SQL);
		}
	}
}