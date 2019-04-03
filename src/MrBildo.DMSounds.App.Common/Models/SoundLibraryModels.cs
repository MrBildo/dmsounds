using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBildo.DMSounds.App.Models
{
	//TODO: refactor to interface or something useful
	public abstract class SoundLibraryViewItem
	{

	}

	public sealed class SoundLibraryViewSoundSetting : SoundLibraryViewItem
	{
		readonly ISoundSettings _soundSettings;

		public SoundLibraryViewSoundSetting(ISoundSettings soundSettings)
		{
			_soundSettings = soundSettings;
		}

		public ISoundSettings SoundSettings => _soundSettings;
	}

	public sealed class SoundLibraryViewType : SoundLibraryViewItem
	{
		readonly SoundType _type;

		public SoundLibraryViewType(SoundType type)
		{
			_type = type;
		}

		public SoundType Type => _type;

	}

	public sealed class SoundLibraryViewCategory : SoundLibraryViewItem
	{
		public SoundLibraryViewCategory(string name)
		{
			Name = name;
		}

		public string Name { get; private set; }

	}

	public sealed class BackSoundLibraryViewCategory : SoundLibraryViewItem
	{

	}

}
