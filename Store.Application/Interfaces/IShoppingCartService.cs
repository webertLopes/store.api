using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Interfaces
{
    public interface IShoppingCartService
    {
        Task<IEnumerable<ShoppingCart>> GetShoppingCartFiltered(ShoppingCart shoppingCart);
        Task<ShoppingCart> Find(Guid id);
        Task<int> Create(ShoppingCart shoppingCart);
        Task<int> UpdateShoppingCart(ShoppingCart shoppingCart);
    }
}
