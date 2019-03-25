using System;
using System.Threading.Tasks;

namespace MrBildo.DMSounds
{
	public sealed class SessionRepository : ISessionRepository
	{
		public void DeleteSession(ISession session)
		{
			throw new NotImplementedException();
		}

		public ISession LoadSession(string filename)
		{
			throw new NotImplementedException();
		}

		public Task<ISession> LoadSessionAsync(string filename)
		{
			throw new NotImplementedException();
		}

		public void SaveSession(ISession session, string filename)
		{
			throw new NotImplementedException();
		}

		public Task SaveSessionAsync(ISession session, string filename)
		{
			throw new NotImplementedException();
		}
	}
}
