using System;
using MrBildo.Audio;

namespace MrBildo.DMSounds
{
	public interface ISound : IDisposable
	{
		string AudioFile { get; }
		bool LoopEnabled { get; set; }
		bool MultipartLoopEnabled { get; set; }
		MultipartLoop MultipartLoopSettings { get; }
		string Name { get; set; }
		float Pan { get; set; }
		bool PanningEnabled { get; set; }
		SoundType Type { get; }
		AudioTrackState State { get; }
		float Volume { get; set; }

		void FadeIn(TimeSpan duration);
		void FadeOut(TimeSpan duration);
		void Pause();
		void Play();
		void Stop();
	}
}