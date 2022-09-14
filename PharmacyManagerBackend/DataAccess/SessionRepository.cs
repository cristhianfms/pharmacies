using System;
using Domain;
using IDataAccess;

namespace DataAccess
{
    public class SessionRepository : ISessionRepository
    {
        public Session Create(Session session)
        {
            throw new NotImplementedException();
        }

        public Session FindSessionByUserId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
