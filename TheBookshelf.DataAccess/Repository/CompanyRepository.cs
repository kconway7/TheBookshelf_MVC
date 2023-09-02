using TheBookshelf.DataAccess.Repository.IRepository;
using TheBookshelf.Models;
using TheBookshelfWeb.DataAccess.Data;

namespace TheBookshelf.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Company obj)
        {
            _db.Companies.Update(obj);
        }
    }
}
