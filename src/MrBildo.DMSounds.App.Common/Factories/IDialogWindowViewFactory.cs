using MrBildo.DMSounds.App.ViewModels;
using MrBildo.DMSounds.App.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBildo.DMSounds.App.Factories
{
	public interface IDialogWindowViewFactory
	{
		IDialogWindowView Create(IViewModel viewModel);
	}
}
