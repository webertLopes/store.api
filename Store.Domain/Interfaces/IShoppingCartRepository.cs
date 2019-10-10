using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Interfaces
{
    public interface IShoppingCartRepository : IDisposable
    {
        Task<IEnumerable<ShoppingCart>> GetShoppingCartFiltered(ShoppingCart shoppingCart);
        Task<ShoppingCart> Find(Guid id);
        Task<int> Create(ShoppingCart shoppingCart);
        Task<int> Update(ShoppingCart shoppingCart);
    }
}
