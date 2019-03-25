using System.Threading.Tasks;

namespace MrBildo.DMSounds
{
	public interface ISoundSettingsRepository
	{
		ISoundSettings LoadSoundSettings(string filename);
		void SaveSoundSettings(ISoundSettings soundSettings, string filename);

		Task<ISoundSettings> LoadSoundSettingsAsync(string filename);
		Task SaveSoundSettingsAsync(ISoundSettings soundSettings, string filename);

		void DeleteSoundSettings(ISoundSettings soundSettings);
	}
}
