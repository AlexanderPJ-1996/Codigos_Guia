// Librerías estandar de un Windows Form
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace [Proyecto]
{
	public partial class [Form] : Form
	{
		// [Comentario]
		// Comentarios multi-linea
		/*
		
		*/
		
		string [Variable]; // Declarar variable, en este caso una de tipo string
		string? [Variable]; // Aceptar valores null
		required string [Variable]; // No aceptar valores null
		public string [Variable]; // Declarar variable de acceso público
		readonly string [Variable]; // Declarar variable de solo lectura
		private readonly [Clase] [Variable] = new [Clase](); // Variable para acceder a procesos y variables de otra
		
		// Obtener información de ensamblado de aplicación/Proyecto
		void AppInfo()
        {
            string AppTit = Application.ProductName;
            string AppDev = Application.CompanyName;
            string AppVer = Application.ProductVersion;
        }
		
		// Personalizar control ComboBox desde código
		void ComboBoxItems()
        {
            // Impedir editar texto de los elementos dentros de un ComboBox
			[ComboBox].DropDownStyle = ComboBoxStyle.DropDownList;
            [ComboBox].FlatStyle = FlatStyle.Popup;
			
			// Agregar elementos desde una lista de elementos
			List<string> [Variable] = new List<string>
            {
                [Elemento 1], 
				[Elemento 2], 
				..., 
				[Elemento N]
            };
            [ComboBox].Items.AddRange([Variable].ToArray());
			
			// Borrar los items de un ComboBox cuando no están rellenados por medio de un DataSet
			[ComboBox].Items.Clear();
			[ComboBox].ResetText();
        }
		
		// Personalizar control TextBox desde código
		void TextBoxProps()
		{
			//CharacterCasing
			[TextBox].CharacterCasing = CharacterCasing.[Upper/Normal/Lower];
			// MaxLength
			[TextBox].MaxLength = [Valor];
			// TextAlign
			[TextBox].TextAlign = HorizontalAlignment.[Right/Center/Left];
			// PasswordChar
			[TextBox].UseSystemPasswordChar = [true/false];
			// Hacer que no pueda modificarse el texto de un TextBox/RichTextBox
			[TextBox].ReadOnly = true/false;
			[RichTextBox].ReadOnly = true/false;
		}
		
		void PictureBoxProps()
		{
			// Validar si un PictureBox tiene o no una imagen
			if ([PictureBox].Image == null)
			{
				
			}
			// Convertir imagen de un PictureBox en un arreglo de bytes[] con librería System.IO
			MemoryStream MS = new MemoryStream();
			[PictureBox].Image.Save(MS, [PictureBox].Image.RawFormat);
			byte[] Bytes = MS.GetBuffer();
			// Cargar imagen en PictureBox
			string PathFile = @"[Ruta]"; // Ruta de archivo con nombre y extensión incluido
			[PictureBox].Image = Image.FromFile(PathFile);
			// Cargar archivo de imagen en memoria para desbloquear archivo
			using (FileStream FS = new FileStream(PathFile, FileMode.Open, FileAccess.Read))
			{
				using (MemoryStream MS = new MemoryStream())
				{
					FS.CopyTo(MS);
					MS.Position = 0;
					[PictureBox].Image = Image.FromStream(MS);
				}
			}
			// Guardar imagenes cargadas en PictureBox sin un SaveFileDialog
			[PictureBox].Image.Save(PathFile);
		}
		
		// Solo recibir numeros sin espacios vacios en un TextBox (int 0), mediante evento KeyPress
		public void OnlyInts(KeyPressEventArgs e)
		{
			e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
		}
		
		// Solo recibir letras sin espacios vacios en un TextBox (string), mediante evento KeyPress
		public void OnlyText(KeyPressEventArgs e)
		{
			e.Handled = !(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back);
		}
		
		/*
        Asegura que el TextBox solo acepte números, un solo punto decimal 
        y hasta 2 dígitos después del punto
        */
        void DecimText(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números, un punto decimal y teclas de control
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // Permitir solo un punto decimal
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
            // Permitir solo 2 dígitos después del punto decimal
            if ((sender as TextBox).Text.Contains("."))
            {
                string[] parts = (sender as TextBox).Text.Split('.');
                if (parts.Length > 1 && parts[1].Length >= 2 && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
		
		// Realizar cálculos básicos con TextBox/Variable
		void Calcular()
		{
			string Operacion;
			int/decimal [Variable 1], [Variable 2], [Variable 3];
			/*
			Int32 = Números enteros de -2.147.483.648 a 2.147.483.647 (9 cifras 000.000.000)
			Int64 = Números enteros de -9.223.372.036.854.775.808 a 9.223.372.036.854.775.807 (18 cifras 000.000.000.000.000.000)
			*/
			[Variable 1] = Convert.ToInt64/ToDecimal([TextBox1]);
			[Variable 2] = Convert.ToInt64/ToDecimal([TextBox2]);
			// Suma
			[Variable 3] = [Variable 1] + [Variable 2];
			// Resta
			[Variable 3] = [Variable 1] - [Variable 2];
			// Multiplicación
			[Variable 3] = [Variable 1] * [Variable 2];
			// División
			[Variable 3] = [Variable 1] / [Variable 2];
			// Mostrar resultado
			[TextBox3] = [Variable 3].ToString();
		}
		
		// Personalizar control TextBox desde código
		void DGVProps()
		{
			// Cambiar los colores de las filas de un DataGridView
			[DataGridView].RowsDefaultCellStyle.BackColor = Color.[Color];
			[DataGridView].AlternatingRowsDefaultCellStyle.BackColor = Color.[Color];
			
			// Capturar datos de un DataGridView
			try
			{
				// Mostrar datos texto en un Label/TextBox/Variable
				[Label/TextBox] = [DataGridView].CurrentRow.Cells[Columna].Value.ToString();
				// Mostrar datos bit/boolean en un CheckBox
				var [Variable] = [DataGridView].CurrentRow.Cells[Columna].Value;
				[CheckBox].Checked = Convert.ToBoolean([Variable]);
				// Mostrar datos blob/longblob/varbinary en un PictureBox
				byte[] Bytes = (byte[])[DataGridView].CurrentRow.Cells[Columna].Value;
				MemoryStream MS = new MemoryStream(Bytes);
				[PictureBox].Image = Image.FromStream(MS);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			
			// Cambiar el propiedades de las columnas de un DataGridView
			try
			{
				// Ancho de columna
				[DataGridView].Columns[Columna].Width = [Value];
				// Texto/cabecera de columna
				[DataGridView].Columns[Columna].HeaderText = [Text];
				// Visible
				[DataGridView].Columns[Columna].Visible = [true/false];
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			
			// Eliminar todas las columnas de un DataGridView
			[DataGridView].Columns.Clear();
		}
		
		// MessageBox.Show
		void MessageBoxShow()
		{
			// MessageBox con texto, sin título
			MessageBox.Show("");
			// MessageBox con texto, y con título
			MessageBox.Show("", "");
			// MessageBox con iconos del sistema
			MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); // Mensaje de exclamación
			MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Question);    // Mensaje de interrogación
			MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Error);       // Mensaje de error
			MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Information); // Mensaje de información
			// MessageBox multilinea
			MessageBox.Show([Linea 1] + Environment.NewLine + [Linea 2] + Environment.NewLine + [Linea N]);
			// If MessageBox.Show
			if (MessageBox.Show("", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
			{
				
			}
			else
			{
				
			}
		}
		
		// Instrucción Try Catch
		void TryCatch()
		{
			try
			{
				
			}
			catch (Exception ex)
			{
				// Cuadro de mensaje (WinForms)
				MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				// Alamcenar en variable para uso posterior
				string [Variable] = ex.Message;
			}
		}
		
		/*
		Instrucción If Else
		&& = AND
		|| = OR
		== es = (Igual a...)
		!= es <> (Diferente de...)
		*/
		void IfElse()
		{
			// Forma 1
			if ([Operacion logica])
			{
				
			}
			else
			{
				
			}
			// Forma 2
			if ([Operacion logica])
			{
				
			}
			else if ([Operacion logica])
			{
				
			}
		}
		
		// Instrucción Switch (Select Case)
		void SelCase()
		{
			switch ([Variable])
			{
				case [Value1]:
				    [Instrucción1]
					break;
				case [Value2]:
				    [Instrucción2]
					break;
				case [ValueN]:
				    [Instrucción3]
					break;
				default:
				    [Instrucción]
					break;
			}
		}
		
		// Configurar un OpenFileDialog en un para cargar una imagen en un PictureBox
		void OpenFile()
		{
			OpenFileDialog OFileD = new OpenFileDialog
            {
                Filter = "Imagenes PNG|*.png|" +
                "Imagenes JPG/JPEG|*.jpg;*.jpeg|" +
                "Imagenes GIF|*.gif|" +
                "Imagenes BMP|*.bmp|" +
                "Imagenes TIFF|*.tif;*.tiff|" +
                "Todos los archivos|*.*",
                Title = "Seleccionar imagen",
                FilterIndex = 1,
                RestoreDirectory = true
            };
			
            if (OFileD.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    [PictureBox].Image = Image.FromFile(OFileD.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
		}
		
		// Configurar SaveFileDialog para guardar imagen desde un PictureBox
		void SaveFile()
        {
            SaveFileDialog SaveDialog = new SaveFileDialog
            {
                Filter = "Imagenes PNG|*.png|" +
                "Imagenes JPG/JPEG|*.jpg;*.jpeg|" +
                "Imagenes GIF|*.gif|" +
                "Imagenes BMP|*.bmp|" +
                "Imagenes TIFF|*.tif;*.tiff|" +
                "Todos los archivos|*.*",
                Title = "Guardar imagen",
                FilterIndex = 1,
                RestoreDirectory = true
            };
			
            if (SaveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Image Img = PBxImage.Image;
                    Img.Save(SaveDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
		
		// Abrir formularios para una unica pantalla
		private void AbrirForm(object Modulo, Panel Pnl)
        {
            // Vaciar panel en caso de tener otro form abierto
			if (Pnl.Controls.Count > 0)
            {
                Pnl.Controls.RemoveAt(0);
            }
            Form Frm = Modulo as Form;
            Frm.TopLevel = false;
            Frm.Dock = DockStyle.Fill;
            Frm.BackColor = BackColor;
            Pnl.Controls.Add(Frm);
            Pnl.Tag = Frm;
            Frm.Show();
        }
		
		// Mostrar respectivo Form llamando 'AbrirForm'
		void Abrir()
		{
			AbrirForm(new [Form]());
		}
		
		// Instrucción Show para mostrar Forms, similar al llamdo de clase
		void ShowForms()
		{
			[Form] [Variable] = new [Form]();
			[Variable].Show();
			// Mostrar Form unida con instrucción frm_closing
			[Variable].FormClosing += Frm_Closing;
		}
		
		// Cerrar/Ocultar Forms
		void EndProj()
		{
			// Ocultar
			this.Hide();
			// Cerrar
			this.Close();
			// Equivalente End
			Environment.Exit(0);
			// Cerrar aplicación/proyecto
			Application.Exit();
			// Reiniciar aplicación/proyecto
			Application.Restart();
		}
		
		// Mostrar Form al cerrar otro Form sin necesidad de un evento FormClosed
		private void Frm_Closing(object sender, FormClosingEventArgs e)
		{
			this.Show();
		}
		
		// Mostrar un Form en instancia única
		[Formulario] [Variable];
        void FormInstaciaUnica()
        {
			if ([Variable] == null)
			{
				[Variable] = new [Formulario]();
				[Variable].FormClosed += (o, args) => [Variable] = null;
			}
			[Variable].Show();
			[Variable].BringToFront();
        }
		
		// Calcular edad a partir de DateTimePicker
		void CalcEdad()
		{
			// double TotalDays = (EndDate - StartDate).TotalDays;
			// Calcular la diferencia de días entre 2 fechas
			double Dias = Convert.ToDouble((DateTime.Today - [DateTimePicker].Value).TotalDays);
			// Convertir diferencia de días a años (365.25 días)
			// Math.Truncate elimina los decimales, mostrando solo la parte entera
			double Edad = Math.Truncate(Dias / 365.25);
			// Mostrar edad como texto
			[TextBox/Variable] = Edad.ToString();
		}
		
		// Unir cadenas string &
		void UnirCadenas()
		{
			[String] = String.Concat([String 1] , [String 2]),... [String N];
		}
		
		// Procedimiento para captar y separar parte entera y decimales de un número decimal
		string NumText, NumEntero, NumDecimal;
        string[] TextSplit;
        void CaptarIntDec(double Numero)
        {
            try
            {
                var DecimalNumber = String.Format("{0:0.00}", Numero);
                NumText = DecimalNumber.ToString();
                TextSplit = NumText.Split(',');
                NumEntero = TextSplit[0];
                NumDecimal = TextSplit[1];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
		
		// Procedimiento para unir parte entera y parte decimal como valor double
		void StringToDouble()
		{
			string Partes = "[ParteEntera]" + "," + "[ParteDecimal]";
			double Numero = Convert.ToDouble(Partes);
		}
		
		// Procedimiento para copiar texto al portapapeles
		void CopyText(string Text)
		{
			try
			{
				Clipboard.SetText(Text);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
	}
}

// Verificar y crear nuevas carpetas/directorios personalizados y manejo de archivos
using System;
using System.IO;

namespace [Proyecto]
{
	public class [Clase]
	{
		// Captar directorio desde donde se ejecuta la aplicación
		string PathBase = AppDomain.CurrentDomain.BaseDirectory;
		
		void CrearCarpeta()
		{
			// Definir ruta de la carpeta/directorio nueva
			string NewPath1 = Path.Combine(PathBase, @"[Directorio]");
			// Verificar si la carpeta/directorio existe
			if (!Directory.Exists(NewPath1))
			{
				// Crear nuevo carpeta/directorio
				Directory.CreateDirectory(NewPath1);
			}
			
			// Definir ruta de subcarpeta/subdirectorio
			string NewPath2 = Path.Combine(PathBase, @"[Directorio]\[Subdirectorio]");
			// Verificar si la subcarpeta/subdirectorio existe
			if (!Directory.Exists(NewPath2))
			{
				// Crear nuevo directorio
				Directory.CreateDirectory(NewPath2);
			}
		}
		// Manejo de archivos
		void ManejarArchivos()
		{
			string Ruta = @"[Ruta]"; // Ruta de archivo con nombre y extensión incluido
			// Verificar si existe el archivo
			if (File.Exists(Ruta))
			{
				// Eliminar archivo
				File.Delete(Ruta);
			}
		}
	}
}

// Procedimiento para abrir una url como hipervínculo
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace [Proyecto]
{
	partial class [Form] : Form
	{
		public [Form]()
        {
            InitializeComponent();
        }
		
		void OpenUrl(string UrlW)
		{
			try
			{
				if (Uri.IsWellFormedUriString(UrlW, UriKind.Absolute))
				{
					Process.Start(new ProcessStartInfo(UrlW) { UseShellExecute = true });
				}
			}
			catch (Exception)
			{
				MessageBox.Show("URL No Válida", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}
	}
}

// Dar formato moneda al texto de un Label/TextBox
using System.Globalization;

namespace [Proyecto]
{
	public class [Clase]
	{
		void Formatear()
		{
			//
			int [Variable] = Convert.ToInt32("[Texto]");
			[Label/TextBox].Text = [Variable].ToString("C", CultureInfo.CurrentCulture);  // Con decimales
			[Label/TextBox].Text = [Variable].ToString("C0", CultureInfo.CurrentCulture); // Sin decimales
			[Label/TextBox].Text = [Variable].ToString("C2", CultureInfo.CurrentCulture); // Con 2 decimales
			[Label/TextBox].Text = [Variable].ToString("C4", CultureInfo.CurrentCulture); // con 4 decimales
			
			// Mostrar formato miles sin decimales
			[Label/TextBox].Text = [Variable].ToString("C0", CultureInfo.CurrentCulture); // Sin decimales
		}
	}
}

// Configuración para exportar datos mostrados en un DataGridView a libro de Excel
using System.Windows.Forms;
using Excel= Microsoft.Office.Interop.Excel;

namespace [Proyecto]
{
    public class ExportExc
    {
        public void Export(DataGridView DGV)
        {
            Excel.Application ExcelApp = new Excel.Application();
            Excel.Workbook WorkBook = ExcelApp.Workbooks.Add(Type.Missing);
            Excel.Worksheet WorkSheet = WorkBook.Sheets[1];
            WorkSheet = WorkBook.ActiveSheet;
            WorkSheet.Name = "DatosExportados";
            // Exportar los encabezados
            for (int i = 1; i < DGV.Columns.Count + 1; i++)
            {
                WorkSheet.Cells[1, i] = DGV.Columns[i - 1].HeaderText;
            }
            // Exportar los datos
            for (int i = 0; i < DGV.Rows.Count; i++)
            {
                for (int j = 0; j < DGV.Columns.Count; j++)
                {
                    WorkSheet.Cells[i + 2, j + 1] = DGV.Rows[i].Cells[j].Value.ToString();
                }
            }
            // Guardar el archivo
            SaveFileDialog SaveDialog = new SaveFileDialog
            {
                Filter = "Archivos de Excel|*.xlsx",
                Title = "Guardar archivo de Excel",
				/*
				Esta última línea [FileName] puede ser removida, ya que solo es para dar un 
				nombre automático al archivo que se desa guardar
				*/
                FileName = "DatosExportados"
            };
            if (SaveDialog.ShowDialog() == DialogResult.OK)
            {
                WorkBook.SaveAs(SaveDialog.FileName);
                WorkBook.Close();
                ExcelApp.Quit();
            }
        }
    }
}

// Métodos para encriptar y desencriptar texto utilizando el algoritmo AES (Advanced Encryption Standard) con C#
using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace [Proyecto]
{
    public class AES
    {
		// Encryption Key (clave de cifrado)
		private static readonly string key = "0123456789012345"; // Debe ser de 16, 24 o 32 caracteres
        // Initialization Vector (vector de inicialización)
		private static readonly string iv = "5432109876543210";  // Debe ser de 16 caracteres
		
        public static string AESEncrypt(string Input)
        {
            using (Aes AESAlg = Aes.Create())
            {
                AESAlg.Key = Encoding.UTF8.GetBytes(key);
                AESAlg.IV = Encoding.UTF8.GetBytes(iv);

                ICryptoTransform Encryptor = AESAlg.CreateEncryptor(AESAlg.Key, AESAlg.IV);

                using (MemoryStream MSe = new MemoryStream())
                {
                    using (CryptoStream CSe = new CryptoStream(MSe, Encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter SWe = new StreamWriter(CSe))
                        {
                            SWe.Write(Input);
                        }
                        return Convert.ToBase64String(MSe.ToArray());
                    }
                }
            }
        }
		
        public static string AESDecrypt(string Input)
        {
            using (Aes AESAlg = Aes.Create())
            {
                AESAlg.Key = Encoding.UTF8.GetBytes(key);
                AESAlg.IV = Encoding.UTF8.GetBytes(iv);

                ICryptoTransform Decryptor = AESAlg.CreateDecryptor(AESAlg.Key, AESAlg.IV);

                using (MemoryStream MSd = new MemoryStream(Convert.FromBase64String(Input)))
                {
                    using (CryptoStream CSd = new CryptoStream(MSd, Decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader SRd = new StreamReader(CSd))
                        {
                            return SRd.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}

// Encriptar y desencriptar datos en Base64 con C#
using System;
using System.Text;

namespace [Proyecto]
{
    public class Base64
    {
        public static string Base64Encrypt(string Input)
        {
            byte[] Bytes = Encoding.Unicode.GetBytes(Input);
            string Output = Convert.ToBase64String(Bytes);
            return Output;
        }
		
        public static string Base64Decrypt(string Input)
        {
            byte[] Bytes = Convert.FromBase64String(Input);
            string Output = Encoding.Unicode.GetString(Bytes);
            return Output;
        }
    }
}

// Clase para encriptar texto con métodos MD5/SHA-1/SHA-256/SHA-512 con C#
using System.Text;
using System.Security.Cryptography;

namespace [Proyecto]
{
    public class SHA
    {
        private static string HashEncrypt(string Input, HashAlgorithm Method)
        {
            byte[] TextBytes = Encoding.UTF8.GetBytes(Input);
            byte[] HashBytes = Method.ComputeHash(TextBytes);
            StringBuilder SB = new StringBuilder();
            foreach (byte B in HashBytes)
            {
                SB.Append(B.ToString("X2"));
            }
            return SB.ToString();
        }
		
        public static string MD5Encrypt(string Input)
        {
            MD5 Md5 = MD5.Create();
            return HashEncrypt(Input, Md5);
        }
		
        public static string SHA1Encrypt(string Input)
        {
            SHA1 Sha1 = SHA1.Create();
            return HashEncrypt(Input, Sha1);
        }
		
        public static string SHA256Encrypt(string Input)
        {
            SHA256 sha256 = SHA256.Create();
            return HashEncrypt(Input, sha256);
        }
		
        public static string SHA512Encrypt(string Input)
        {
            SHA512 sha512 = SHA512.Create();
            return HashEncrypt(Input, sha512);
        }
    }
}

// Procedimiento para eliminar espacios en blanco de un string
using System.Text;
using System.Text.RegularExpressions;

namespace [Proyecto]
{
	public class RemoverEspacios
	{
		string [String con Espacios], [String sin Espacios];
		
		void RemoverVacio()
		{
			string [String sin Espacios] = Regex.Replace("String con Espacios", @"\s", string.Empty);
			// Con Replace() no se requiere de la librería System.Text.RegularExpressions
			string [String sin Espacios] = [TextBox/RichTextBox/Label].Text.Replace(" ", string.Empty);
		}
	}
}

// Procedimiento para permitir una única instancia de un proyecto Windows Forms .NET Framework
using System;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

namespace [Proyecto]
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            var ProjInfo = typeof(Program).Assembly;
            var Atributos = (GuidAttribute)ProjInfo.GetCustomAttributes(typeof(GuidAttribute), true)[0];
            string MyGUID = Atributos.Value.ToString();
            Mutex Mtx = new Mutex(true, "{" + MyGUID +"}");
			
			if(Mtx.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new [Form]());
            }
            else
            {
                MessageBox.Show("Instancia abierta", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

// Procedimiento para leer el texto de un archivo de texto y almacenarlo en una variable
using System.IO;
using System.Windows.Forms;

namespace [Proyecto]
{
	public class LeerFileText
	{
		string [Variable];
		void FileText()
		{
			string Line;
			try
			{
				StreamReader SR = new StreamReader("[Archivo]");
				Line = SR.ReadLine();
				while (Line != null)
				{
					[Variable] = Line;
					Line = SR.ReadLine();
				}
				SR.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
	}
}

/*
Procedimiento para capturar la cámara web y tomar una foto con las librería AForge.NET
AForge.Video: 
AForge.Video.DirectShow: 
*/
using System;
using System.Drawing;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace [Proyecto]
{
    public partial class [Form] : Form
    {
        private FilterInfoCollection VidDevices;
        private VidCaptureDevice VidSource;
		
        public YourForm()
        {
            InitializeComponent();
            LoadDevices();
        }
		
        private void LoadDevices()
        {
            VidDevices = new FilterInfoCollection(FilterCategory.VidInputDevice);
            foreach (FilterInfo Device in VidDevices)
            {
                [ComboBox].Items.Add(Device.Name);
            }
            [ComboBox].SelectedIndex = 0;
        }
		
        private void [Button]_Click(object sender, EventArgs e)
        {
            VidSource = new VidCaptureDevice(VidDevices[[ComboBox].SelectedIndex].MonikerString);
            VidSource.NewFrame += new NewFrameEventHandler(VidSource_NewFrame);
            VidSource.Start();
        }
		
        private void VidSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap BitMp = (Bitmap)eventArgs.Frame.Clone();
            [PictureBox_Video].Image = BitMp;
        }
		
        private void [Button]_Click(object sender, EventArgs e)
        {
            if ([PictureBox_Video].Image != null)
            {
                [PictureBox_Photo].Image = (Bitmap)[PictureBox_Video].Image.Clone();
            }
        }
		
        private void [Button]_Click(object sender, EventArgs e)
        {
            if (VidSource != null && VidSource.IsRunning)
            {
                VidSource.SignalToStop();
                VidSource.WaitForStop();
            }
        }
		
        private void YourForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (VidSource != null && VidSource.IsRunning)
            {
                VidSource.SignalToStop();
                VidSource.WaitForStop();
            }
        }
    }
}

// Crear un List<string> con items en negrita(blod) y mostrarlos en un RichTextBox
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace [Proyecto]
{
    public partial class [Form] : Form
    {
		void ListInRichTextBox()
		{
			List<string> Lista = new List<string>
			{
				@"{\b Item en negrita}",
                "Item en texto normal"
			};
			ShowItems(Lista);
		}
		
		void ShowItems(List<string> Lista)
        {
            [RichTextBox].Rtf = BuildRtf(Lista);
        }
		
		string BuildRtf(List<string> Lista)
        {
			string FontName = Font.Name; // Obtener el nombre de la fuente del formulario
            int FontSize = [#FontSize]; // Tamaño de la fuente
            string RtfCabecera = $@"{{\rtf1\ansi\deff0 {{\fonttbl {{\f0 {FontName};}}}} ";
            string RtfContenido = "";
			
            foreach (string Item in Lista)
            {
				RtfContenido += $@"\f0\fs{FontSize * 2} {Item}\par ";
            }
			
            string RtfFinal = "}";
            return RtfCabecera + RtfContenido + RtfFinal;
        }
    }
}
