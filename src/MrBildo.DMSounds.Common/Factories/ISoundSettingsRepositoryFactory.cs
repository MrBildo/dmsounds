using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBildo.DMSounds
{
	public interface ISoundSettingsRepositoryFactory
	{
		ISoundSettingsRepository Create(string path);
	}
}
