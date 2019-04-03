using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBildo.DMSounds.App.ViewModels
{
	public interface IMainViewModel
	{
		ISoundLibraryViewModel SoundLibraryViewModel { get; }

		ISceneSelectionViewModel SceneSelectionViewModel { get; }

		ISceneSoundsViewModel MusicBedViewModel { get; }
		ISceneSoundsViewModel SoundEffectsViewModel { get; }
		ISceneSoundsViewModel AmbientSoundsViewModel { get; }
	}
}
