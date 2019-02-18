using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBildo.Audio.NAudio
{
	public class LoopingWaveStream : WaveStream
	{
		private WaveStream _source;

		public LoopingWaveStream(WaveStream sourceStream)
		{
			_source = sourceStream ?? throw new ArgumentNullException("sourceStream");
		}

		public bool EnableLooping { get; set; } = false;

		public override WaveFormat WaveFormat => _source.WaveFormat;

		public override long Length => _source.Length;

		public override long Position { get => _source.Position; set => _source.Position = value; }

		public override int Read(byte[] buffer, int offset, int count)
		{
			var totalBytesRead = 0;

			while (totalBytesRead < count)
			{
				var bytesRead = _source.Read(buffer, offset + totalBytesRead, count - totalBytesRead);

				if (bytesRead == 0)
				{
					if (_source.Position == 0 || !EnableLooping)
					{
						break;
					}

					_source.Position = 0;
				}

				totalBytesRead += bytesRead;
			}

			return totalBytesRead;
		}
	}
}
