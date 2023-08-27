using TheBookshelf.Models;

namespace TheBookshelf.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category ob);

    }
}
