using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBildo.DMSounds
{
	public class Scene : IScene
	{
		readonly ISoundFactory _soundFactory;
		readonly ISoundService _soundService;

		readonly List<ISound> _ambientSounds = new List<ISound>();
		readonly List<ISound> _soundEffects = new List<ISound>();
		readonly List<ISound> _musicBeds = new List<ISound>();

		public Scene(string name, ISoundFactory soundFactory, ISoundService soundService)
		{
			if (name.IsNullorWhitespace())
			{
				throw new ArgumentNullException(nameof(name));
			}

			_soundFactory = soundFactory ?? throw new ArgumentNullException(nameof(soundFactory));

			_soundService = soundService ?? throw new ArgumentNullException(nameof(soundService));
		}

		public string Name { get; set; }

		public ITheme Theme { get; set; }

		public IEnumerable<ISound> AllSounds
		{
			get
			{
				var sounds = new List<ISound>();

				sounds.AddRange(AmbientSounds);
				sounds.AddRange(SoundEffects);
				sounds.AddRange(MusicBeds);

				return sounds;
			}
		}

		public IEnumerable<ISound> AmbientSounds => _ambientSounds.ToArray();

		public IEnumerable<ISound> SoundEffects => _soundEffects.ToArray();

		public IEnumerable<ISound> MusicBeds => _musicBeds.ToArray();

		public string Filename { get; private set; }

		//add stuff
		public void AddAmbientSound(ISoundSettings soundSettings, int position = 0)
		{
			if (soundSettings.Type != SoundType.AmbientSound)
			{
				throw new ArgumentException("sound must be an ambient sound type");
			}

			var sound = _soundFactory.Create(soundSettings);

			_ambientSounds.Insert(position, sound);
		}

		public void AddSoundEffect(ISoundSettings soundSettings, int position = 0)
		{
			if (soundSettings.Type != SoundType.SoundEffect)
			{
				throw new ArgumentException("sound must be a sound effect");
			}

			var sound = _soundFactory.Create(soundSettings);

			_soundEffects.Insert(position, sound);
		}

		public void AddMusicBed(ISoundSettings soundSettings, int position = 0)
		{
			if (soundSettings.Type != SoundType.MusicBed)
			{
				throw new ArgumentException("sound must be a music bed");
			}

			var sound = _soundFactory.Create(soundSettings);

			_musicBeds.Insert(position, sound);
		}

		//remove stuff
		public void RemoveAmbientSound(ISound sound)
		{
			if (sound.Type != SoundType.AmbientSound)
			{
				throw new ArgumentException("sound must be an ambient sound type");
			}

			if (!_ambientSounds.Contains(sound))
			{
				throw new ArgumentException("sound does not exist in the collection");
			}

			_ambientSounds.Remove(sound);

			sound.Dispose();

		}

		public void RemoveSoundEffect(ISound sound)
		{
			if (sound.Type != SoundType.SoundEffect)
			{
				throw new ArgumentException("sound must be a sound effect");
			}

			if (!_soundEffects.Contains(sound))
			{
				throw new ArgumentException("sound does not exist in the collection");
			}

			_soundEffects.Remove(sound);

			sound.Dispose();
		}

		public void RemoveMusicBed(ISound sound)
		{
			if (sound.Type != SoundType.MusicBed)
			{
				throw new ArgumentException("sound must be a music bed");
			}

			if (!_musicBeds.Contains(sound))
			{
				throw new ArgumentException("sound does not exist in the collection");
			}

			_musicBeds.Remove(sound);

			sound.Dispose();

		}

		//reposition stuff
		public void RepositionAmbientSound(ISound sound, int postion)
		{
			if (sound.Type != SoundType.AmbientSound)
			{
				throw new ArgumentException("sound must be an ambient sound type");
			}

			if (!_ambientSounds.Contains(sound))
			{
				throw new ArgumentException("sound does not exist in the collection");
			}

			_ambientSounds.Remove(sound);
			_ambientSounds.Insert(postion, sound);

		}

		public void RepositionSoundEffect(ISound sound, int postion)
		{
			if (sound.Type != SoundType.SoundEffect)
			{
				throw new ArgumentException("sound must be a sound effect");
			}

			if (!_soundEffects.Contains(sound))
			{
				throw new ArgumentException("sound does not exist in the collection");
			}

			_soundEffects.Remove(sound);
			_soundEffects.Insert(postion, sound);

		}

		public void RepositionMusicBed(ISound sound, int postion)
		{
			if (sound.Type != SoundType.MusicBed)
			{
				throw new ArgumentException("sound must be a music bed");
			}

			if (!_musicBeds.Contains(sound))
			{
				throw new ArgumentException("sound does not exist in the collection");
			}

			_musicBeds.Remove(sound);
			_musicBeds.Insert(postion, sound);

		}

		//scene controls
		public void FadeIn(TimeSpan duration)
		{
			foreach(var sound in AllSounds)
			{
				sound.FadeIn(duration);
			}
		}

		public void FadeOut(TimeSpan duration)
		{
			foreach (var sound in AllSounds)
			{
				sound.FadeOut(duration);
			}
		}

		public void Pause()
		{
			foreach (var sound in AllSounds)
			{
				sound.Pause();
			}
		}

		public void Play()
		{
			foreach (var sound in AllSounds)
			{
				sound.Play();
			}
		}

		public void Stop()
		{
			foreach (var sound in AllSounds)
			{
				sound.Stop();
			}
		}
	}
}
