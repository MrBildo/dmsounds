using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBildo.DMSounds.App.Messages
{
	public class SoundLibrarySelectedItemChanged
	{
		readonly ISoundSettings _soundSettings;

		public SoundLibrarySelectedItemChanged(ISoundSettings soundSettings)
		{
			_soundSettings = soundSettings;
		}

		public ISoundSettings SoundSettings => _soundSettings;
	}
}
