using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MrBildo.Audio.Effects
{
	public enum PanDirection
	{
		RightToLeft,
		LeftToRight
	}
	public class OscillatingPanEffect : Effect
	{
		private bool _disposed = false;

		Timer _timer;

		public OscillatingPanEffect(AudioTrack audioTrack, TimeSpan speed, float increment, PanDirection startDirection = PanDirection.LeftToRight) : base(audioTrack)
		{
			Speed = speed;
			Increment = increment;
		}

		public void Start()
		{
			if (!AudioTrack.PanningEnabled)
			{
				return;
			}

			_timer = new Timer(Speed.TotalMilliseconds);
			_timer.Elapsed += _timer_Elapsed;
			_timer.Start();
		}

		private void _timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			var currentPan = AudioTrack.Pan;

			if(currentPan == 1)
			{
				PanDirection = PanDirection.RightToLeft;
			}
			else if(currentPan == -1)
			{
				PanDirection = PanDirection.LeftToRight;
			}

			var multiplier = PanDirection == PanDirection.LeftToRight ? 1 : -1;

			AudioTrack.Pan += multiplier * Increment;

		}

		public void Stop()
		{
			_timer.Stop();
			_timer.Dispose();
			_timer = null;
		}

		public TimeSpan Speed { get; private set; }

		public float Increment { get; private set; }

		private PanDirection PanDirection { get; set; }

		protected override void Dispose(bool disposing)
		{
			if (_disposed)
			{
				return;
			}

			if (disposing)
			{
				if(_timer != null)
				{
					_timer.Dispose();
				}
			}

			base.Dispose(disposing);
		}
	}
}
