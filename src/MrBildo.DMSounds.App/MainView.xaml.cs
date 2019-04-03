using MrBildo.DMSounds.App.Factories;
using MrBildo.DMSounds.App.ViewModels;
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

namespace MrBildo.DMSounds.App
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainView : Window
	{
		readonly IDialogWindowViewFactory _dialogWindowViewFactory;
		readonly ISoundSettingsViewModelFactory _soundSettingsViewModelFactory;
		readonly IWindowViewModelFactory _windowViewModelFactory;

		public MainView(IDialogWindowViewFactory dialogWindowViewFactory, ISoundSettingsViewModelFactory soundSettingsViewModelFactory, IWindowViewModelFactory windowViewModelFactory)
		{
			_dialogWindowViewFactory = dialogWindowViewFactory;
			_soundSettingsViewModelFactory = soundSettingsViewModelFactory;
			_windowViewModelFactory = windowViewModelFactory;

			//DataContext = viewModel;

			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			var viewModel = _soundSettingsViewModelFactory.Create();

			var container = _windowViewModelFactory.Create(viewModel);

			var view = _dialogWindowViewFactory.Create(container);

			//TODO: add this to interface
			((Window)view).Owner = this;

			//var view = _dialogWindowViewFactory.Create(_soundSettingsViewModelFactory.Create());

			view.ShowDialog();
		}
	}
}
