using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MrBildo.DMSounds
{
	public class Session : ISession
	{
		readonly List<IScene> _scenes = new List<IScene>();

		Timer _timer;

		public Session(string name)
		{
			if (name.IsNullorWhitespace())
			{
				throw new ArgumentNullException(nameof(name));
			}
		}

		public string Name { get; set; }

		public ITheme Theme { get; set; }

		public IEnumerable<IScene> Scenes => _scenes.ToArray();

		public string Filename { get; private set; }

		public void AddScene(IScene scene, int position = 0)
		{
			if(scene == null)
			{
				throw new ArgumentNullException(nameof(scene));
			}

			_scenes.Insert(position, scene);
		}

		public void RemoveScene(IScene scene)
		{
			if (scene == null)
			{
				throw new ArgumentNullException(nameof(scene));
			}

			if (!_scenes.Contains(scene))
			{
				throw new ArgumentException("scene does not exist in the collection");
			}

			_scenes.Remove(scene);
		}

		public void RepositionScene(IScene scene, int position)
		{
			if (scene == null)
			{
				throw new ArgumentNullException(nameof(scene));
			}

			if (!_scenes.Contains(scene))
			{
				throw new ArgumentException("scene does not exist in the collection");
			}

			_scenes.Remove(scene);
			_scenes.Insert(position, scene);
		}

		public void TransitionScenes(IScene fromScene, IScene toScene, TimeSpan fade, TimeSpan gap)
		{
			if (fromScene == null)
			{
				throw new ArgumentNullException(nameof(fromScene));
			}

			if (toScene == null)
			{
				throw new ArgumentNullException(nameof(toScene));
			}

			if (!_scenes.Contains(fromScene) || !_scenes.Contains(toScene))
			{
				throw new ArgumentException("scene does not exist in the collection");
			}

			var totalTime = fade + gap;

			fromScene.FadeOut(fade);

			_timer = new Timer(TransitionStop, (toScene, fade), totalTime, TimeSpan.Zero);
		}

		private void TransitionStop(object state)
		{
			(IScene scene, TimeSpan fade) = ((IScene, TimeSpan)) state;

			scene.FadeIn(fade);

			_timer.Dispose();
		}
		
	}
}
