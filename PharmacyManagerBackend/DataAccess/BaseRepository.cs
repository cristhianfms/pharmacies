using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Exceptions;
using IDataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;
public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly DbContext _context;
    private readonly DbSet<T> _table;
    public BaseRepository(DbContext dbContext)
    {
        this._context = dbContext;
        this._table = _context.Set<T>();
    }

    public T Create(T entity)
    {
        _table.Add(entity);
        _context.SaveChanges();

        return entity;
    }

    public IEnumerable<T> GetAll(Func<T, bool> expresion = null)
    {
        IEnumerable<T> entities;

        if (expresion == null)
        {
            entities = _table;
        }
        else
        {

            entities = _table.Where(expresion);
        }

        return entities;
    }

    public virtual T GetFirst(Func<T, bool> expresion)
    {
        IEnumerable<T> entities = _table.Where(expresion);
        T entityToReturn;
        try
        {
            entityToReturn = entities.First(expresion);
        }
        catch (InvalidOperationException)
        {
            throw new ResourceNotFoundException("resource not found");
        }

        return entityToReturn;
    }

    public virtual void Delete(T elem)
    {
        _table.Remove(elem);
        _context.SaveChanges();
    }

    public void Update(T elem)
    {
        _table.Update(elem);
        _context.SaveChanges();
    }
}

