using MrBildo.DMSounds.App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBildo.DMSounds.App.ViewModels
{
	public interface ISoundLibraryViewModel
	{
		event Action<ISoundSettings> SelectedItemChanged;

		ObservableCollection<SoundLibraryViewItem> Categories { get; set; }
		ObservableCollection<SoundLibraryViewSoundSetting> Items { get; set; }

		SoundLibraryViewSoundSetting SelectedItem { get; set; }

		RelayCommand<SoundLibraryViewCategory> CategorySelectCommand { get; }
		RelayCommand<SoundLibraryViewType> TypeSelectCommand { get; }
		RelayCommand BackCommand { get; }

		void Load();

	}
}
