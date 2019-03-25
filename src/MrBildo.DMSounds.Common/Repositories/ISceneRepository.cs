using System.Threading.Tasks;

namespace MrBildo.DMSounds
{
	public interface ISceneRepository
	{
		IScene LoadScene(string filename);
		void SaveScene(IScene scene, string filename);

		Task<IScene> LoadSceneAsync(string filename);
		Task SaveSceneAsync(IScene scene, string filename);

		void DeleteScene(IScene scene);

	}
}
