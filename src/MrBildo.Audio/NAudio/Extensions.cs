using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBildo.Audio
{
	public static class Extensions
	{
		public static bool AreFormatsEqual(this WaveFormat a, WaveFormat b, bool ignoreChannels = false)
		{
			return
				ignoreChannels || (a.Channels == b.Channels)
				&& a.SampleRate == b.SampleRate
				&& a.AverageBytesPerSecond == b.AverageBytesPerSecond
				&& a.BlockAlign == b.BlockAlign
				&& a.BitsPerSample == b.BitsPerSample
				&& a.ExtraSize == b.ExtraSize;

		}
	}
}
