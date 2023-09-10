using TheBookshelf.DataAccess.Repository.IRepository;
using TheBookshelf.Models;
using TheBookshelfWeb.DataAccess.Data;

namespace TheBookshelf.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            var objFromDb = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Title = obj.Title;
                objFromDb.Description = obj.Description;
                objFromDb.Price = obj.Price;
                objFromDb.Price50 = obj.Price50;
                objFromDb.Price100 = obj.Price100;
                obj.ListPrice = objFromDb.ListPrice;
                obj.ISBN = objFromDb.ISBN;
                obj.CategoryId = objFromDb.CategoryId;
                obj.Author = objFromDb.Author;
                //if (obj.ImageUrl != null)
                //{
                //    objFromDb.ImageUrl = obj.ImageUrl;
                //}
            }
        }
    }
}
