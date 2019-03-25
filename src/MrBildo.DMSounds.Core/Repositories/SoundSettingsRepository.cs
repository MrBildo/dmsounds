using MrBildo.DMSounds.IO;
using MrBildo.DMSounds.Serialization;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MrBildo.DMSounds
{
	public sealed class SoundSettingsRepository : ISoundSettingsRepository
	{

		public ISoundSettings LoadSoundSettings(string filename)
		{
			var settings = new JsonSerializerSettings();

			settings.Converters.Add(new SoundSettingsConverter(filename));

			var sound = JsonConvert.DeserializeObject<ISoundSettings>(File.ReadAllText(filename), settings);

			return sound;
		}

		public async Task<ISoundSettings> LoadSoundSettingsAsync(string filename)
		{
			throw new NotImplementedException();
		}


		public void SaveSoundSettings(ISoundSettings sound, string filename)
		{
			File.WriteAllText(filename, GetSaveSoundSettings(sound, filename));
		}

		public async Task SaveSoundSettingsAsync(ISoundSettings soundSettings, string filename)
		{
			await FileAsync.WriteAllTextAsync(filename, GetSaveSoundSettings(soundSettings, filename));
		}


		public void DeleteSoundSettings(ISoundSettings soundSettings)
		{
			throw new NotImplementedException();
		}


		private string GetSaveSoundSettings(ISoundSettings soundSettings, string filename)
		{
			var resolver = new MappedContractResolver();

			resolver.Mapping.Map("Filename").Ignore();

			var json = JsonConvert.SerializeObject(soundSettings, Formatting.Indented, new JsonSerializerSettings() { ContractResolver = resolver });

			return json;
		}

	}
}
