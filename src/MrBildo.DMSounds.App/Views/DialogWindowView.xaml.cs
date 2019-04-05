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
using System.Windows.Shapes;

namespace MrBildo.DMSounds.App.Views
{
    /// <summary>
    /// Interaction logic for WindowView.xaml
    /// </summary>
    public partial class DialogWindowView : Window, IDialogWindowView
    {
        public DialogWindowView(IViewModel viewModel)
        {
			DataContext = viewModel;

            InitializeComponent();
        }

		public bool? ShowDialog(Window owner)
		{
			Owner = owner;

			return ShowDialog();
		}
	}
}
