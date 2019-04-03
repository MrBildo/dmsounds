using MrBildo.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace MrBildo.DMSounds
{
	public class Sound : ISound
	{
		private bool _disposed = false;

		public Sound(ISoundSettings soundSettings, ISoundService soundService)
		{
			//first, create the audio track
			AudioTrack = soundService.AudioEngine.AddAudioTrack(soundSettings.AudioFile);
			
			//set the name as the sound name initially
			Name = soundSettings.Name;

			//need to save the audio file for later
			AudioFile = soundSettings.AudioFile;

			Type = soundSettings.Type;

			//settings for audio track
			AudioTrack.Loop = soundSettings.LoopEnabled;
			AudioTrack.MultipartLoopEnabled = soundSettings.MultipartLoopEnabled;
			AudioTrack.MultipartLoop = soundSettings.MultipartLoopSettings;

		}

		internal Sound(string audioFile, ISoundService soundService)
		{
			AudioTrack = soundService.AudioEngine.AddAudioTrack(audioFile);
		}

		public string Name { get; set; }

		public string AudioFile { get; internal set; }

		public SoundType Type { get; internal set; }

		private IAudioTrack AudioTrack { get; set; }

		public float Volume { get => AudioTrack.Volume; set => AudioTrack.Volume = value; }

		public bool LoopEnabled { get => AudioTrack.Loop; set => AudioTrack.Loop = value; }

		public bool PanningEnabled { get => AudioTrack.PanningEnabled; set => AudioTrack.PanningEnabled = value; }

		public float Pan { get => AudioTrack.Pan; set => AudioTrack.Pan = value; }

		public bool MultipartLoopEnabled { get => AudioTrack.MultipartLoopEnabled; set => AudioTrack.MultipartLoopEnabled = value; }

		public MultipartLoop MultipartLoopSettings => AudioTrack.MultipartLoop;

		public AudioTrackState State => AudioTrack.State;

		public void FadeIn(TimeSpan duration)
		{
			AudioTrack.FadeIn(duration);
		}

		public void FadeOut(TimeSpan duration)
		{
			AudioTrack.FadeOut(duration);
		}

		public void Play()
		{
			AudioTrack.Play();
		}

		public void Pause()
		{
			AudioTrack.Pause();
		}

		public void Stop()
		{
			AudioTrack.Stop();
		}

		public void Dispose()
		{
			Dispose(true);

			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_disposed)
			{
				return;
			}

			if (disposing)
			{
				if(AudioTrack != null)
				{
					AudioTrack.Dispose();
				}
			}

			_disposed = true;
		}
	}
}
