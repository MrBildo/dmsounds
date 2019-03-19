using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBildo.DMSounds
{
	public interface ISoundSettingsFactory
	{
		ISoundSettings Create(string name, string audioFile, SoundType type);
	}
}
