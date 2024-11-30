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

namespace [Proyecto]
{
	public partial class [Form] : Form
	{
		// [Comentario]
		// Comentarios multi-linea
		/*
		
		*/
		
		// Declarar variable, en este caso una de tipo string
		string [Variable];
		// Declarar variable de acceso público
		public string [Variable];
		// Variable para acceder a procesos y variables de otra
		[Clase] [Variable] = new [Clase]();
		
		// Procedimiento
		void [Procedimiento]()
		{
			
		}
		
		// Procedimiento de acceso público
		public void [Procedimiento]()
		{
			
		}
		
		// Obtener información de ensamblado de aplicación/Proyecto
		void AppInfo()
        {
            string AppTit = Application.ProductName;
            string AppDev = Application.CompanyName;
            string AppVer = Application.ProductVersion;
        }
		
		// Personalizar control ComboBox desde código
		void CBxItems()
        {
            // Impedir editar texto de los elementos dentros de un ComboBox
			[ComboBox].DropDownStyle = ComboBoxStyle.DropDownList;
            [ComboBox].FlatStyle = FlatStyle.Popup;
			
			// Agregar elementos a un ComboBox de forma manual
			// Agregar elemento por elemento
			[ComboBox].Items.Add("Elemento 1");
            [ComboBox].Items.Add("Elemento 2");
            [ComboBox].Items.Add("Elemento N");
			// Agregar desde una lista de elementos
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
		
		// Perzonalizar control DateTimePicker
		void DTPItems()
		{
			
		}
		
		// Personalizar control TextBox desde código
		void TBxProps()
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
				var [Variable 1] = (Byte[])[DataGridView].CurrentRow.Cells[Columna].Value;
				var [Variable 2] = new MemoryStream([Variable 1);
				[PictureBox].Image = Image.FromStream([Variable 2]);
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
		
		// Proceso para string como arreglo de bytes
		void ConvertStringToBytes()
		{
			// Convertir string en bytes
			byte[] Bytes = Encoding.UTF8.GetBytes([Variable/TextBox]);
			[Variable/TextBox] = BitConverter.ToString(Bytes);
			// Restaurar string desde bytes
			[Variable/TextBox] = UTF8Encoding.UTF8.GetString(Bytes);
		}
		
		// MessageBox.Show
		void MessageBoxShow()
		{
			MessageBox.Show("");
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
		void OFile()
		{
			OpenFileDialog [Variable] = new OpenFileDialog
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
            if ([Variable].ShowDialog() == DialogResult.OK)
            {
                [PictureBox].Image = Image.FromFile([Variable].FileName);
            }
		}
		
		// Abrir formularios para una unica pantalla
		private void AbrirForm(object Modulo)
        {
            // Vaciar panel en caso de tener otro form abierto
			if ([Panel].Controls.Count > 0)
            {
                [Panel].Controls.RemoveAt(0);
            }
            Form [Variable] = Modulo as Form;
            [Variable].TopLevel = false;
            [Variable].Dock = DockStyle.Fill;
            [Variable].BackColor = BackColor;
            [Panel].Controls.Add([Variable]);
            [Panel].Tag = [Variable];
            [Variable].Show();
        }
		
		// Mostrar respectivo Form llamando 'AbrirForm'
		void Abrir()
		{
			AbrirForm(new [Form]());
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
			// Equivalente End
			Environment.Exit(0);
			// Equivalente Me.Close()
			this.Close();
			// Ocultar
			this.Hide();
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
		
		/*
		Procedimiento para captar y separar parte entera y decimales 
		de un número decimal
		*/
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
		void StringToDouvle()
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

// Dar formato moneda al texto de un Label/TextBox
using System.Globalization;

namespace [Proyecto]
{
	public class [Clase]
	{
		void Formatear()
		{
			string [Variable] = "[Texto]"
			[Label/TextBox].Text = [Variable].ToString("C", CultureInfo.CurrentCulture);  // Con decimales
			[Label/TextBox].Text = [Variable].ToString("C0", CultureInfo.CurrentCulture); // Sin decimales
			[Label/TextBox].Text = [Variable].ToString("C2", CultureInfo.CurrentCulture); // Con 2 decimales
			[Label/TextBox].Text = [Variable].ToString("C4", CultureInfo.CurrentCulture); // con 4 decimales
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

/*
Este proceso solo sirve para encriptar los datos en MD5/SHA256/SHA512
Para el proceso de encriptación, se debe agregar la librería/paquete NuGet OC.Core.Crypto
https://www.nuget.org/packages/OC.Core.Crypto/
*/
using OC.Core.Crypto;

namespace [Proyecto]
{
	class MetodoEcriptar1
    {
        public string [String Sin Encriptar], [String Encriptado];
		
		// Encriptado MD5/SHA256/SHA512
        public void Metodo_MD5_SHA256_SHA512()
        {
            // Instanciamos al metodo Hash de la libreria OC.Core.Crypto
            var [Variable Hash] = new Hash();
            // Encriptamos el string recibido al metodo MD5/SHA256/SHA512
			[String Encriptado] = [Variable Hash].[MD5/SHA256/SHA512]([String Sin Encriptar]).ToString();
        }
    }
}

// Encriptar y desencriptar datos en Base64
using System;
using System.Text;

namespace [Proyecto]
{
    public class MetodoEcriptar2
    {
        string Resultado = String.Empty;
		
        public string Encriptar(string TxtNoEnc)
        {
            byte[] Encriptado = Encoding.Unicode.GetBytes(TxtNoEnc);
            Resultado = Convert.ToBase64String(Encriptado);
            return Resultado;
        }
		
        public string Desencriptar(string TxtEncri)
        {
            byte[] Desencriptado = Convert.FromBase64String(TxtEncri);
            Resultado = Encoding.Unicode.GetString(Desencriptado);
            return Resultado;
        }
    }
}

/*
Este proceso solo sirve para encriptar los datos con la librería/paquete Eramake.eCryptography
https://www.nuget.org/packages/Eramake.eCryptography/
*/
using Eramake;

namespace System.Cryp
{
    public class EramCryp
    {
        public String TxEncryp, TxDecryp;

        public void Encryp(String Texto)
        {
            TxEncryp = eCryptography.Encrypt(Texto);
        }

        public void Decryp(String Texto)
        {
            TxDecryp = eCryptography.Decrypt(Texto);
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
			[String sin Espacios] = Regex.Replace([String con Espacios], @"\s", String.Empty);
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
            Mutex mutex = new Mutex(true, "{" + MyGUID +"}");
			
			if(mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new [Form]());
            }
            else
            {
                MessageBox.Show(Application.ProductName + ": Instancia abierta", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

// Procedimiento para permitir una única instancia de un proyecto Windows Forms .NET 5.0
using System;
using System.Windows.Forms;
using System.Threading;

namespace ContarDinero
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            string ProdName = Application.ProductName;
            bool CreatedNew;
            using Mutex Mtx = new(true, ProdName, out CreatedNew);
            {
                if (CreatedNew)
                {
                    Application.SetHighDpiMode(HighDpiMode.SystemAware);
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new [Form]());
                }
                else
                {
                    MessageBox.Show(Application.ProductName + ": Instancia abierta", String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}