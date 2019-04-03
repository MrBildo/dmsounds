using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrBildo.DMSounds
{
	public interface ISoundSettingsRepository
	{
		Task<IEnumerable<ISoundSettings>> LoadAllAsync();
		Task<IEnumerable<ISoundSettings>> LoadAllByTypeAsync(SoundType soundType);
		Task<(IEnumerable<string> Categories, IEnumerable<ISoundSettings> Items)> LoadAllByTypeAndCategories(SoundType soundType, string[] categories);

		Task<ISoundSettings> LoadSoundSettingsAsync(string filename);

		Task SaveSoundSettingsAsync(ISoundSettings soundSettings);

		void DeleteSoundSettings(ISoundSettings soundSettings);

	}
}
