// Librerías estandar de un WPF
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace App_WPF
{
	public partial class MainWindow : Window
	{
		// Procedimieto para obtener nombre de la aplicación
		private string AppName()
		{
			string MyText = Application.ResourceAssembly.GetName().Name;
			return MyText;
		}
	}
}