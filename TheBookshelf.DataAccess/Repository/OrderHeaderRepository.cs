using TheBookshelf.DataAccess.Repository.IRepository;
using TheBookshelf.Models;
using TheBookshelfWeb.DataAccess.Data;

namespace TheBookshelf.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private ApplicationDbContext _db;
        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderHeader obj)
        {
            _db.OrderHeaders.Update(obj);
        }
    }
}
