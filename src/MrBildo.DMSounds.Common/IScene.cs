using System;
using System.Collections.Generic;

namespace MrBildo.DMSounds
{
	public interface IScene : ISoundEntity
	{
		string Name { get; set; }

		ITheme Theme { get; set; }

		IEnumerable<ISound> AllSounds { get; }

		IEnumerable<ISound> AmbientSounds { get; }

		IEnumerable<ISound> SoundEffects { get; }

		IEnumerable<ISound> MusicBeds { get; }

		void AddAmbientSound(ISoundSettings soundSettings, int position = 0);
		void AddSoundEffect(ISoundSettings soundSettings, int position = 0);
		void AddMusicBed(ISoundSettings soundSettings, int position = 0);

		void RepositionAmbientSound(ISound sound, int postion);
		void RepositionSoundEffect(ISound sound, int postion);
		void RepositionMusicBed(ISound sound, int postion);

		void RemoveAmbientSound(ISound sound);
		void RemoveSoundEffect(ISound sound);
		void RemoveMusicBed(ISound sound);

		void FadeIn(TimeSpan duration);
		void FadeOut(TimeSpan duration);

		void Pause();
		void Play();
		void Stop();
	}
}