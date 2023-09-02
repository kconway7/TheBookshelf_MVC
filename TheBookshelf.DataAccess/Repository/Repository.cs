namespace TheBookshelf.DataAccess.Repository;

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
        _db.Products.Include(u => u.Category);
    }

    public void Add(T entity)
    {
        dbset.Add(entity);
    }

    public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
    {
        IQueryable<T> query;
        if (tracked)
        {
            query = dbset;
        }
        else
        {
            query = dbset.AsNoTracking();
        }

        query = query.Where(filter);

        if (!String.IsNullOrEmpty(includeProperties))
        {
            foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }

        return query.FirstOrDefault();
    }

    //Category, CateogryId
    public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
    {
        IQueryable<T> query = dbset;
        if (filter != null)
        {

            query = query.Where(filter);
        }
        if (!String.IsNullOrEmpty(includeProperties))
        {
            foreach (var includeProp in includeProperties
                .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }

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
