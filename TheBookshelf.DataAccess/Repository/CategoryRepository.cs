using TheBookshelf.DataAccess.Repository.IRepository;
using TheBookshelf.Models;
using TheBookshelfWeb.DataAccess.Data;

namespace TheBookshelf.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
