using MrBildo.Audio;

namespace MrBildo.DMSounds
{
	internal class SceneSoundAudioTrackInfo
	{
		public float Volume { get; set; }

		public bool LoopEnabled { get; set; }

		public bool PanningEnabled { get; set; }

		public float Pan { get; set; }

		public bool MultipartLoopEnabled { get; set; }

		public MultipartLoop MultipartLoopSettings { get; set; }
	}
}
