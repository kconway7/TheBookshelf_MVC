using TheBookshelf.Models;

namespace TheBookshelf.DataAccess.Repository.IRepository
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        void Update(OrderDetail obj);

    }
}
