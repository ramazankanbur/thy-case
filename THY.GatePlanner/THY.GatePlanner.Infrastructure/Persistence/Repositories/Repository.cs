﻿using System.Linq.Expressions;
using THY.GatePlanner.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace THY.GatePlanner.Infrastructure.Persistence.Repositories;

public class Repository<T> : IRepository<T> where T : Base
{
    private readonly DbContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public Repository(DbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _dbSet = dbContext.Set<T>();
    }

    public void Add(T entity)
    {
        _dbSet.Add(entity);
    }

    public void Delete(Expression<Func<T, bool>> predicate)
    {
        var removeItems = _dbSet.Where(predicate).ToList();
        _dbSet.RemoveRange(removeItems);
    }

    public IQueryable<T> GetAll()
    {
        return _dbSet;
    }

    public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
    {
        return _dbSet.Where(predicate);
    }

    public T? GetByIdAsync(int id)
    {
        return   _dbSet.Find(id);
    }

    public void Update(T entity)
    {
        _dbSet.Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}