using MrBildo.DMSounds.IO;
using MrBildo.DMSounds.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace MrBildo.DMSounds
{
	public sealed class SoundSettingsRepository : ISoundSettingsRepository
	{
		const string FILE_EXTENSION = "dmsndsetting";

		Dictionary<string, ISoundSettings> _cache;

		readonly string _path;

		public SoundSettingsRepository(string path)
		{
			if(path.IsNullorWhitespace())
			{
				throw new ArgumentNullException(nameof(path));
			}

			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}

			_path = path;
		}

		public async Task<IEnumerable<ISoundSettings>> LoadAllAsync()
		{
			var all = await GetCache();

			return all.Values;
		}

		public async Task<IEnumerable<ISoundSettings>> LoadAllByTypeAsync(SoundType soundType)
		{
			var all = await LoadAllAsync();

			return all.Where(s => s.Type == soundType);
		}

		public async Task<(IEnumerable<string> Categories, IEnumerable<ISoundSettings> Items)> LoadAllByTypeAndCategories(SoundType soundType, string[] categories)
		{
			var all = await LoadAllByTypeAsync(soundType);

			//filter based on selected categories
			all = all.Where(s => categories.ContainsAll(s.Categories));

			var categorizedItems = new List<ISoundSettings>();

			var filteredCategories = new List<string>();

			//create category groups and rank by numbers (Tavern: 3, City: 2, Fighting: 2, etc.)
			var rankedCategories =
				all
					.SelectMany(s => s.Categories)
						.Where(c => !categories.Contains(c))
							.GroupBy(c => c)
								.ToDictionary(c => c.Key, c => c.Count());


			foreach(var rankedCategory in rankedCategories.OrderByDescending(c => c.Value))
			{
				foreach(var soundSettings in all.Where(s => s.Categories.Contains(rankedCategory.Key)))
				{
					var category = rankedCategories.Where(k => soundSettings.Categories.Contains(k.Key)).OrderByDescending(c => c.Value).FirstOrDefault();

					if (!categorizedItems.Contains(soundSettings))
					{
						categorizedItems.Add(soundSettings);

						if (!filteredCategories.Contains(category.Key))
						{
							filteredCategories.Add(category.Key);
						}

						rankedCategories[category.Key]++;

						foreach (var categoryToRemove in soundSettings.Categories.Except(categories.Concat(new string[] { category.Key })))
						{
							rankedCategories[categoryToRemove]--;
						}
					}

				}
			}

			var items = all.Except(categorizedItems);

			return (filteredCategories, items);
		}

		public async Task<ISoundSettings> LoadSoundSettingsAsync(string filename)
		{
			var cache = await GetCache();

			var file = Path.Combine(_path, Path.GetFileName(filename));

			if (cache.ContainsKey(file))
			{
				return cache[file];
			}

			throw new ArgumentException($"sound setting not found");
		}

		public async Task SaveSoundSettingsAsync(ISoundSettings soundSettings)
		{
			var resolver = new MappedContractResolver();

			resolver.Mapping.Map("Filename").Ignore();

			var json = JsonConvert.SerializeObject(soundSettings, Formatting.Indented, new JsonSerializerSettings() { ContractResolver = resolver });

			var filename = GetFilename(soundSettings);

			await FileAsync.WriteAllTextAsync(filename, json);

			await WriteSoundSettingsToCache(filename, _cache);
		}

		public void DeleteSoundSettings(ISoundSettings soundSettings)
		{
			throw new NotImplementedException();
		}

		private async Task<Dictionary<string, ISoundSettings>> GetCache()
		{
			if(_cache == null)
			{
				_cache = new Dictionary<string, ISoundSettings>();

				var tasks = Directory.GetFiles(_path, $"*.{FILE_EXTENSION}")
					.Select(f => WriteSoundSettingsToCache(f, _cache));

				await Task.WhenAll(tasks.ToArray());
			}

			return _cache;
		}

		private async Task WriteSoundSettingsToCache(string filename, Dictionary<string, ISoundSettings> cache)
		{
			var data = await FileAsync.ReadAllTextAsync(filename);

			var settings = new JsonSerializerSettings();

			settings.Converters.Add(new SoundSettingsConverter(filename));

			cache[filename] = JsonConvert.DeserializeObject<ISoundSettings>(data, settings);
		}

		private string GetFilename(ISoundSettings soundSettings)
		{
			var name = $"{GetTypeAbbreviation(soundSettings.Type)}-{soundSettings.Name.ToLower().SpacesToDashes()}";

			var filename = Path.Combine(_path, $"{name}.{FILE_EXTENSION}");

			var index = 1;

			while (File.Exists(filename))
			{
				var indexedName = $"{GetTypeAbbreviation(soundSettings.Type)}-{soundSettings.Name.ToLower().SpacesToDashes()}-{index}";

				filename = Path.Combine(_path, $"{indexedName}.{FILE_EXTENSION}");

				index++;
			}

			return filename;
		}

		private string GetTypeAbbreviation(SoundType soundType)
		{
			switch (soundType)
			{
				case SoundType.MusicBed:
					return "mb";
				case SoundType.AmbientSound:
					return "as";
				case SoundType.SoundEffect:
					return "se";
				default:
					throw new ArgumentException("invalid sound type");
			}
		}
	}
}
