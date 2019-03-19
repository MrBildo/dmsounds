using MrBildo.DMSounds.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBildo.DMSounds
{
	public class SoundRepository : ISoundRepository
	{
		public void DeleteScene(IScene scene)
		{
			throw new NotImplementedException();
		}

		public void DeleteSession(ISession session)
		{
			throw new NotImplementedException();
		}

		public void DeleteSound(ISoundSettings sound)
		{
			throw new NotImplementedException();
		}

		public IScene LoadScene(string filename)
		{
			throw new NotImplementedException();
		}

		public ISession LoadSession(string filename)
		{
			throw new NotImplementedException();
		}

		public ISoundSettings LoadSoundSettings(string filename)
		{
			var settings = new JsonSerializerSettings();

			settings.Converters.Add(new SoundSettingsConverter(filename));

			var sound = JsonConvert.DeserializeObject<ISoundSettings>(File.ReadAllText(filename), settings);

			return sound;
		}

		public void SaveScene(IScene scene, string filename)
		{
			throw new NotImplementedException();
		}

		public void SaveSession(ISession session, string filename)
		{
			throw new NotImplementedException();
		}

		public void SaveSoundSettings(ISoundSettings sound, string filename)
		{
			var resolver = new MappedContractResolver();

			resolver.Mapping.Map("Filename").Ignore();

			var json = JsonConvert.SerializeObject(sound, Formatting.Indented, new JsonSerializerSettings() { ContractResolver = resolver });

			File.WriteAllText(filename, json);
		}

		private string SerializeSceneSounds(IEnumerable<Sound> sounds)
		{
			throw new NotImplementedException();
		}
	}
}
