using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBildo.Audio.Effects
{
	public abstract class Effect : IDisposable
	{
		private bool _disposed = false;

		protected Effect(AudioTrack audioTrack)
		{
			AudioTrack = audioTrack ?? throw new ArgumentNullException("audioTrack");
		}

		protected AudioTrack AudioTrack { get; private set; }

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

			_disposed = true;
		}
	}
}
