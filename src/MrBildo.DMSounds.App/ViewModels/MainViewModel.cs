using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBildo.DMSounds.App.ViewModels
{
	public class MainViewModel : ViewModel, IMainViewModel
	{
		ISoundLibraryViewModel _soundLibraryViewModel;
		readonly ISceneSelectionViewModel _sceneSelectionViewModel;

		public MainViewModel(
				ISoundLibraryViewModel soundLibraryViewModel,
				ISceneSelectionViewModel sceneSelectionViewModel
			)
		{
			SoundLibraryViewModel = soundLibraryViewModel;
			//_sceneSelectionViewModel = sceneSelectionViewModel;
		}

		public ISoundLibraryViewModel SoundLibraryViewModel
		{
			get
			{
				return _soundLibraryViewModel;
			}

			set
			{
				SetProperty(ref _soundLibraryViewModel, value);
			}
		}

		public ISceneSelectionViewModel SceneSelectionViewModel => _sceneSelectionViewModel;

		public ISceneSoundsViewModel MusicBedViewModel => throw new NotImplementedException();

		public ISceneSoundsViewModel SoundEffectsViewModel => throw new NotImplementedException();

		public ISceneSoundsViewModel AmbientSoundsViewModel => throw new NotImplementedException();
	}
}
