using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBildo.DMSounds.App.ViewModels
{
    public class WindowViewModel : ViewModel, IWindowViewModel
	{
		readonly IViewModel _viewModel;

		public WindowViewModel(IViewModel viewModel)
		{
			_viewModel = viewModel;
		}

		public IViewModel ViewModel => _viewModel;
    }
}
