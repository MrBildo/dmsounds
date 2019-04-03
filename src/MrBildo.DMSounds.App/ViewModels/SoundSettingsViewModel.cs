using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBildo.DMSounds.App.ViewModels
{
	public class SoundSettingsViewModel : ViewModel, ISoundSettingsViewModel
	{
		readonly ISoundLibraryViewModel _soundLibraryViewModel;
		readonly ISoundSettingsEditorViewModel _soundSettingsEditorViewModel;

		public SoundSettingsViewModel(ISoundLibraryViewModel soundLibraryViewModel, ISoundSettingsEditorViewModel soundSettingsEditorViewModel)
		{
			_soundLibraryViewModel = soundLibraryViewModel;
			_soundSettingsEditorViewModel = soundSettingsEditorViewModel;

			_soundLibraryViewModel.SelectedItemChanged += OnSoundLibrarySelectedItemChanged;
		}

		public ISoundLibraryViewModel SoundLibraryViewModel => _soundLibraryViewModel;

		public ISoundSettingsEditorViewModel SoundSettingsEditorViewModel => _soundSettingsEditorViewModel;

		private void OnSoundLibrarySelectedItemChanged(ISoundSettings obj)
		{
			
		}
	}
}
