using System.Threading.Tasks;

namespace MrBildo.DMSounds
{
	public interface ISessionRepository
	{
		ISession LoadSession(string filename);
		void SaveSession(ISession session, string filename);

		Task<ISession> LoadSessionAsync(string filename);
		Task SaveSessionAsync(ISession session, string filename);

		void DeleteSession(ISession session);

	}
}
