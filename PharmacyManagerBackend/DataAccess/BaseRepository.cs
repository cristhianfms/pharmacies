using System;
using System.Collections.Generic;
using Domain;
using IDataAccess;

namespace DataAccess
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public Session Create(Session session)
        {
            throw new NotImplementedException();
        }

        public T Create(T entity)
        {
            throw new NotImplementedException();
        }

        public Session FindSessionByUserId(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll(Func<T, bool> expresion = null)
        {
            throw new NotImplementedException();
        }

        public T GetFirst(Func<T, bool> expresion)
        {
            throw new NotImplementedException();
        }
    }
}
