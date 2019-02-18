using System.Collections.Generic;
using MrBildo.Audio;

namespace MrBildo.DMSounds
{
	public interface ISound : IProxySerializable
	{
		string AudioFile { get; set; }
		List<string> Categories { get; }
		List<string> Keywords { get; }
		bool LoopEnabled { get; set; }
		bool MultipartLoopEnabled { get; set; }
		MultipartLoop MultipartLoopSettings { get; set; }
		string Name { get; set; }
		SoundType SoundType { get; set; }
	}
}