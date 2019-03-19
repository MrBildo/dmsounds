using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBildo.DMSounds.Serialization
{
	public class SceneConverter : JsonConverter
	{
		string _filename = null;
		IEnumerable<ISound> _sounds = null;

		public SceneConverter(string filename, IEnumerable<ISound> sounds) : base()
		{
			_filename = filename;
			_sounds = sounds;
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(SoundSettings);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();

			var jsonObject = JObject.Load(reader);

			//return new Scene(_filename, _sounds);
		}

		public override bool CanWrite => false;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}
