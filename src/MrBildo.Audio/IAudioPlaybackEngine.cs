using System;
using System.Collections.Generic;
using NAudio.Wave;

namespace MrBildo.Audio
{
	public interface IAudioPlaybackEngine : IDisposable
	{
		IWavePlayer DeviceInterface { get; }

		IEnumerable<IAudioTrack> Tracks { get; }

		IAudioTrack AddAudioTrack(string filename);
		void RemoveAudioTrack(IAudioTrack audioTrack);
	}
}