using System;
using System.ComponentModel.Design;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Linq;

namespace ConsoleTest
{
	internal class Program
	{
		internal static void Main(string[] args)
		{
			Console.WriteLine(GetRateFromNbu());
		}

		public static string GetRateFromNbu(string currencyName = "USD", string date = "20190822")
		{
			var requestUri = new UriBuilder("https", "bank.gov.ua");
			requestUri.Path = "NBUStatService/v1/statdirectory/exchange";
			requestUri.Query = $"valcode={currencyName}&date={date}";

			var webRequest = WebRequest.Create(requestUri.Uri);
			var webResponseStream = webRequest.GetResponse().GetResponseStream();

			if (webResponseStream != null)
			{
				using (var xmlReader = XmlReader.Create(webResponseStream))
				{
					xmlReader.MoveToContent();

					while (xmlReader.Read())
						switch (xmlReader.NodeType)
						{
							case XmlNodeType.Element:
								if (xmlReader.Name == "rate")
								{
									var value = ((XElement)XElement.ReadFrom(xmlReader)).Value;

									if (string.IsNullOrEmpty(value.Trim()))
										return string.Empty;
									else
										return $"{currencyName}/UAH={value}";
								}

								break;
						}
				}
			}

			return string.Empty;
		}
	}
}

