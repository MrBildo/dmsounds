using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBildo.DMSounds
{
	public class Session : ISession
	{
		readonly List<IScene> _scenes = new List<IScene>();

		public string Name { get; set; }

		public ITheme Theme { get; set; }

		public IEnumerable<IScene> Scenes => _scenes.ToArray();

		public string Filename { get; private set; }

		public void AddScene(IScene scene, int postion = 0)
		{
			throw new NotImplementedException();
		}

		public void RemoveScene(IScene scene)
		{
			throw new NotImplementedException();
		}

		public void RepositionScene(IScene scene)
		{
			throw new NotImplementedException();
		}

		public void TransitionScenes(IScene fromScene, IScene toScene, TimeSpan gap, bool fade = true)
		{
			throw new NotImplementedException();
		}
	}
}
