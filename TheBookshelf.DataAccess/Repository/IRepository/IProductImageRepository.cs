﻿using TheBookshelf.Models;

namespace TheBookshelf.DataAccess.Repository.IRepository
{
    public interface IProductImageRepository : IRepository<ProductImage>
    {
        void Update(ProductImage obj);

    }
}
