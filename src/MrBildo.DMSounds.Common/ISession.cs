using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBildo.DMSounds
{
	public interface ISession : ISoundEntity
	{
		string Name { get; set; }

		ITheme Theme { get; set; }

		IEnumerable<IScene> Scenes { get; }

		void AddScene(IScene scene, int position = 0);

		void RepositionScene(IScene scene, int position);

		void RemoveScene(IScene scene);

		void TransitionScenes(IScene fromScene, IScene toScene, TimeSpan fade, TimeSpan gap);
	}
}
