using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrBildo.Audio;

namespace MrBildo.DMSounds
{
	public class SoundService : ISoundService
	{
		public SoundService()
		{
			AudioEngine = AudioPlaybackEngine.CreateAudioPlaybackEngine(DeviceInterfaceType.WasapiOut);
		}

		public AudioPlaybackEngine AudioEngine { get; private set; }
	}
}
