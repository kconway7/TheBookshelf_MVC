using System.Linq.Expressions;


namespace TheBookshelf.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        // T - Category, later can be any other generic model such as product
        IEnumerable<T> GetAll();
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
