using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBildo.DMSounds
{
	public interface ISoundRepository
	{
		ISession LoadSession(string filename);
		void SaveSession(ISession session, string filename);

		IScene LoadScene(string filename);
		void SaveScene(IScene scene, string filename);

		ISoundSettings LoadSoundSettings(string filename);
		void SaveSoundSettings(ISoundSettings soundSettings, string filename);

		void DeleteSession(ISession session);
		void DeleteScene(IScene scene);
		void DeleteSound(ISoundSettings sound);

	}
}
