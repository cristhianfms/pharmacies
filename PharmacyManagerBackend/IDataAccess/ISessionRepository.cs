using System;
using Domain;

namespace IDataAccess
{
    public interface ISessionRepository
    {
        Session Create(Session session);
        Session FindSessionByUserId(int id);
    }
}
