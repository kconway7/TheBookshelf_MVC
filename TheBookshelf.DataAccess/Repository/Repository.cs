﻿namespace TheBookshelf.DataAccess.Repository;

using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TheBookshelf.DataAccess.Repository.IRepository;
using TheBookshelfWeb.DataAccess.Data;

public class Repository<T> : IRepository<T> where T : class
{

    private readonly ApplicationDbContext _db;
    internal DbSet<T> dbset;

    public Repository(ApplicationDbContext db)
    {
        _db = db;
        this.dbset = _db.Set<T>();
    }

    public void Add(T entity)
    {
        dbset.Add(entity);
    }

    public T Get(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = dbset;
        query = query.Where(filter);

        return query.FirstOrDefault();
    }

    public IEnumerable<T> GetAll()
    {
        IQueryable<T> query = dbset;

        return query.ToList();
    }

    public void Remove(T entity)
    {
        dbset.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entity)
    {
        dbset.RemoveRange(entity);
    }
}
