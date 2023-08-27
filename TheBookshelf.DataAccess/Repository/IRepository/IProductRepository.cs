using TheBookshelf.Models;

namespace TheBookshelf.DataAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {

        void Update(Product obj);

    }
}

