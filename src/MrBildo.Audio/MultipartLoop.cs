using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace MrBildo.Audio
{
	public class MultipartLoop
	{
		private const string FILE_IDENT = "MultipartLoop";
		private const int FILE_VERSION = 1;

		public MultipartLoop(TimeSpan startTime, TimeSpan endTime)
		{
			StartTime = startTime;
			EndTime = endTime;
		}

		public MultipartLoop(TimeSpan startTime, TimeSpan endTime, (TimeSpan StartTime, TimeSpan EndTime) cue) : this(startTime, endTime)
		{
			AddCue(cue);
		}

		public TimeSpan StartTime { get; set; }

		public TimeSpan EndTime { get; set; }

		public bool IsAtEnd { get; private set; } = true;

		private Queue<(TimeSpan Start, TimeSpan End)> Cues { get; } = new Queue<(TimeSpan Start, TimeSpan End)>();

		public void AddCue((TimeSpan Start, TimeSpan End) cue)
		{
			//if this is the first cue, just make it current
			if (Cues.Count == 0)
			{
				CurrentCue = cue;
			}
			else
			{
				Cues.Enqueue(cue);
			}

			IsAtEnd = false;
		}

		public (TimeSpan Start, TimeSpan End) CurrentCue { get; private set; }

		public void NextCue()
		{
			if (Cues.Count > 0)
			{
				CurrentCue = Cues.Dequeue();
			}
			else
			{
				IsAtEnd = true;
			}
		}

	}
}
