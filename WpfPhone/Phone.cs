using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace WpfPhone
{
	public class Phone : INotifyPropertyChanged
	{
		private string _title;
		private string _company;
		private int _price;

		public string Title
		{
			get => _title;
			set
			{
				_title = value;
				OnPropertyChanged(nameof(Title));
			}
		}

		public string Company
		{
			get => _company;
			set
			{
				_company = value;
				OnPropertyChanged(nameof(Company));
			}
		}

		public int Price
		{
			get => _price;
			set
			{
				_price = value;
				OnPropertyChanged(nameof(Price));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
