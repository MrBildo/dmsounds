using NAudio.CoreAudioApi;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBildo.Audio
{
	public enum DeviceInterfaceType
	{
		WaveOut,		// classic Windows audio API. Good performance and compatibility
		WaveOutEvent,	// same as WaveOut, but uses an event callback scheme. Preferred over WaveOut unless there is an issue
		WasapiOut,      // modern (post-Vista) Windows audio API. Has excellent performance and compatibility
		DirectSoundOut	// not really better than the other options. May be a use for it in certain scenarios
		//AsioOut		// unsupported by most consumer soundcards and only really needed if you're doing studio mixing
	}

	public enum SampleRate : int
	{
		Telephony = 8000,
		VoIP = 16000,
		CD = 44100,
		DVD = 48000,
		HD_96 = 96000,
		HD_192 = 192000
	}

	public enum Channels : int
	{
		Mono = 1,
		Stereo = 2
	}

	public class AudioPlaybackEngine : IDisposable, IAudioPlaybackEngine
	{
		private bool _disposed = false;

		private List<IAudioTrack> _tracks = new List<IAudioTrack>();

		private AudioPlaybackEngine(IWavePlayer deviceInterface, int sampleRate, int channels)
		{
			Mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channels));

			Mixer.ReadFully = true;

			DeviceInterface = deviceInterface;

			DeviceInterface.Init(Mixer);
			DeviceInterface.Play();
		}

		public IWavePlayer DeviceInterface { get; private set; }

		private MixingSampleProvider Mixer { get; set; }

		public IEnumerable<IAudioTrack> Tracks => _tracks.ToArray();

		public static IAudioPlaybackEngine CreateAudioPlaybackEngine(DeviceInterfaceType type)
		{
			return CreateAudioPlaybackEngine(type, SampleRate.CD, Channels.Stereo, 200);
		}

		public static AudioPlaybackEngine CreateAudioPlaybackEngine(DeviceInterfaceType type, SampleRate sampleRate, Channels channels, int latency)
		{
			IWavePlayer deviceInterface = null;

			switch (type)
			{
				case DeviceInterfaceType.WaveOut:
					deviceInterface = new WaveOut()
					{
						DesiredLatency = latency
					};
					break;

				case DeviceInterfaceType.WaveOutEvent:
					deviceInterface = new WaveOutEvent()
					{
						DesiredLatency = latency
					};
					break;

				case DeviceInterfaceType.WasapiOut:
					deviceInterface = new WasapiOut(AudioClientShareMode.Shared, latency);
					break;

				case DeviceInterfaceType.DirectSoundOut:
					deviceInterface = new DirectSoundOut(latency);
					break;

				default:
					throw new ArgumentException("type");

			}

			return new AudioPlaybackEngine(deviceInterface, (int)sampleRate, (int)channels);
		}

		public IAudioTrack AddAudioTrack(string filename)
		{
			var audioTrack = new AudioTrack(filename, Mixer);

			_tracks.Add(audioTrack);

			return audioTrack;
		}

		public void RemoveAudioTrack(IAudioTrack audioTrack)
		{
			_tracks.Remove(audioTrack);

			audioTrack.Dispose();
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
				foreach(var track in _tracks)
				{
					track.Dispose();
				}

				DeviceInterface.Dispose();
			}

			_disposed = true;
		}
	}
}
