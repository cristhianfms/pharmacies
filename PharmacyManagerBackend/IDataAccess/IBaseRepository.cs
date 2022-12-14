using System;
using System.Collections.Generic;
using Domain;

namespace IDataAccess;

public interface IBaseRepository<T> where T : class
{
    T Create(T entity);
    IEnumerable<T> GetAll(Func<T, bool> expresion = null);
    T GetFirst(Func<T, bool> expresion);
    void Delete(T elem);
    void Update(T elem);
}
