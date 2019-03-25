using System;
using System.Threading.Tasks;

namespace MrBildo.DMSounds
{
	public sealed class SceneRepository : ISceneRepository
	{
		public void DeleteScene(IScene scene)
		{
			throw new NotImplementedException();
		}

		public IScene LoadScene(string filename)
		{
			throw new NotImplementedException();
		}

		public Task<IScene> LoadSceneAsync(string filename)
		{
			throw new NotImplementedException();
		}

		public void SaveScene(IScene scene, string filename)
		{
			throw new NotImplementedException();
		}

		public Task SaveSceneAsync(IScene scene, string filename)
		{
			throw new NotImplementedException();
		}
	}
}
