using System;
using System.Net;
using System.Collections.Generic;
using System.IO;
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

namespace WPFTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
			txb_fileUri.Text = "C:\\Users\\oholenko\\source\\repos\\NetworkProgramming\\WPFTest\\MainWindow.xaml.cs";

		}

		private void openFile_Click(object sender, RoutedEventArgs e)
		{
			var fileUri = txb_fileUri.Text;

			var fileWebRequest = (FileWebRequest)WebRequest.Create(fileUri);

			using (var streamReader = new StreamReader(fileWebRequest.GetResponse().GetResponseStream()))
			{
				txb_fileContent.Text = streamReader.ReadToEnd();
			}
		}

		private void writeFile_Click(object sender, RoutedEventArgs e)
		{
			var fileWebRequest = (FileWebRequest)WebRequest.Create(txb_fileUri.Text);
			fileWebRequest.Method = "PUT";

			using (var streamWriter = new StreamWriter(fileWebRequest.GetRequestStream()))
			{
				streamWriter.Write(txb_fileContent.Text);
			}
		}
	}
}
