using System;

namespace MrBildo.Audio
{
	public interface IAudioTrack : IDisposable
	{
		TimeSpan CurrentTime { get; }
		bool Loop { get; set; }
		MultipartLoop MultipartLoop { get; set; }
		bool MultipartLoopEnabled { get; set; }
		float Pan { get; set; }
		bool PanningEnabled { get; set; }
		AudioTrackState State { get; set; }
		float Volume { get; set; }

		void FadeIn(TimeSpan duration);
		void FadeOut(TimeSpan duration);
		void Pause();
		void Play();
		void Stop();
	}
}