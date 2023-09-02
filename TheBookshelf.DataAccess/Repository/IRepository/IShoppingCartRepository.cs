﻿using TheBookshelf.Models;

namespace TheBookshelf.DataAccess.Repository.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        void Update(ShoppingCart obj);

    }
}
