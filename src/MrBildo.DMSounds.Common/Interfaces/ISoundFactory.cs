using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBildo.DMSounds
{
	public interface ISoundFactory
	{
		ISound Create(string name, string audioFile, SoundType type);
		ISound Create(Proxy<Sound> proxy);
	}
}
