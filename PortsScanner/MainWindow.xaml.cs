using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
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

namespace PortsScanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

			List<PortInfo> portInformations = GetOpenPorts();
			listViewScanner.ItemsSource = portInformations;

		}

		private static List<PortInfo> GetOpenPorts()
		{
			var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
			IPEndPoint[] tcpEndPoints = ipGlobalProperties.GetActiveTcpListeners();
			TcpConnectionInformation[] tcpConnections = ipGlobalProperties.GetActiveTcpConnections();

			return tcpConnections
				.Select(tcpConnection => new PortInfo(
					portNumber: tcpConnection.LocalEndPoint.Port,
					localAddress: $"{tcpConnection.LocalEndPoint.Address}:{tcpConnection.LocalEndPoint.Port}",
					remoteAddress: $"{tcpConnection.RemoteEndPoint.Address}:{tcpConnection.RemoteEndPoint.Port}",
					state: tcpConnection.State.ToString())
				)
				.OrderBy(portInfo => portInfo.State)
				.ToList();
		}
    }

	public class PortInfo
	{
		public int PortNumber {get; set;}
		public string LocalAddress {get; set;}
		public string RemoteAddress {get; set;}
		public string State {get; set;}

		public PortInfo(int portNumber, string localAddress, string remoteAddress, string state)
		{
			PortNumber = portNumber;
			LocalAddress = localAddress;
			RemoteAddress = remoteAddress;
			State = state;
		}
	}
}
