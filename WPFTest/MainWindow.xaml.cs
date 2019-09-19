using System;
using System.Collections;
using System.Net;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Resources;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace WPFTest
{
	public class SupplyCommands
	{
		private static RoutedUICommand _apply;

		static SupplyCommands()
		{
			var inputs = new InputGestureCollection();
			inputs.Add(new KeyGesture(Key.A, ModifierKeys.Control, "Ctrl+A"));
			_apply = new RoutedUICommand("Apply", "Apply", typeof(SupplyCommands), inputs);
		}

		public static RoutedUICommand Apply => _apply;
	}

	public partial class MainWindow : Window
    {

	    public string TestMessage { get; set; } = "TestTextMessage-";
		public bool IsApplyEnabled { get; set; } = false;


	    public MainWindow()
        {
            InitializeComponent();

			sliderFontSize.Value = 10;

			btnApplyFontSize.CommandParameter ="TestValueMessage";
			
			/*ApplicationCommands.New.Text = "Apply";
			var newCommandBinding = new CommandBinding(ApplicationCommands.New);
			newCommandBinding.Executed += ApplyFontSize;
			newCommandBinding.CanExecute += ApplyCommand_CanExecute;
			this.CommandBindings.Add(newCommandBinding);*/

			var applyCommandBinding = new CommandBinding(SupplyCommands.Apply);
			applyCommandBinding.Executed += ApplyCommand_Executed;
			applyCommandBinding.CanExecute += ApplyCommand_CanExecute;
			this.CommandBindings.Add(applyCommandBinding);


/*			var bindingFontSize = new Binding()
			{
				Source = sliderFontSize,
				Path = new PropertyPath(nameof(sliderFontSize.Value)),
				Mode = BindingMode.TwoWay
			};
			var bindingText = new Binding();

			lblSampleText.SetBinding(TextBlock.FontSizeProperty, bindingFontSize);*/
        }

		private void element_MouseEnter(object sender, MouseEventArgs e)
		{
			((TextBlock)sender).Background = new SolidColorBrush(Colors.Bisque);
		}

		private void element_MouseLeave(object sender, MouseEventArgs e)
		{
			((TextBlock)sender).Background = null;
		}

        private void CmdSetSmallFontSize_OnClick(object sender, RoutedEventArgs e)
        {
	        /*			var binding = BindingOperations.GetBinding(lblSampleText, TextBlock.FontSizeProperty);
				        if (binding != null)
				        {
					        BindingOperations.ClearBinding(lblSampleText, TextBlock.FontSizeProperty);
					        binding.Mode = BindingMode.TwoWay;
					        lblSampleText.SetBinding(TextBlock.FontSizeProperty, binding);
				        }*/

			//lblSampleText.FontSize = 5;
			TestMessage += "+";
		}

        private void ApplyCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
			var bindingExpression = BindingOperations.GetBindingExpression(tbFontSize, TextBox.TextProperty);
			bindingExpression.UpdateSource();
			lblSaveCommandReport.Text = $"Save command was triggered by {e.Source.ToString()}; {(string)e.Parameter}";

		}

		private void ApplyCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = IsApplyEnabled;
		}

        private void BtnAlterApproach_OnClick(object sender, RoutedEventArgs e)
        {
	        SupplyCommands.Apply.Execute("Alter approach", (IInputElement)sender);
        }
    }
}
