using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MrBildo.DMSounds.App.Views
{
	public interface IDialogWindowView
	{
		bool? ShowDialog(Window owner);
	}
}
