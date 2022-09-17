using System;
using Domain;

namespace IDataAccess
{
    public interface IBaseRepository<T> where T : class
    {
        T Create(T entity);
    }
}