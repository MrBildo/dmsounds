using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBildo.DMSounds.Serialization
{
	public class SoundSettingsConverter : CustomCreationConverter<ISoundSettings>
	{
		readonly string _filename;

		public SoundSettingsConverter(string filename) : base()
		{
			_filename = filename;
		}

		public override ISoundSettings Create(Type objectType)
		{
			return new SoundSettings(_filename);
		}
	}
}
