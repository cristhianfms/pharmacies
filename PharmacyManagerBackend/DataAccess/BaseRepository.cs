﻿using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using IDataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
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

        public T GetFirst(Func<T, bool> expresion)
        {
            IEnumerable<T> entities = _table.Where(expresion);
            
            return entities.First(expresion);
        }
    }
}
