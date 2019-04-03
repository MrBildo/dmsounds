using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBildo.DMSounds.App.ViewModels
{
	public interface ISoundSettingsViewModel : IViewModel
	{
		ISoundLibraryViewModel SoundLibraryViewModel { get; }

		ISoundSettingsEditorViewModel SoundSettingsEditorViewModel { get; }
	}
}
