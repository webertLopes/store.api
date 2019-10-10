using Store.Application.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository shoppingCartRepository;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository ?? throw new ArgumentNullException(nameof(shoppingCartRepository));
        }

        public async Task<int> Create(ShoppingCart shoppingCart)
        {
            if (shoppingCart == null)
            {
                throw new ArgumentNullException(nameof(shoppingCart));
            }

            var rowsAffected = await shoppingCartRepository.Create(shoppingCart);

            return rowsAffected;
        }

        public async Task<ShoppingCart> Find(Guid id)
        {
            if (Guid.Empty == id)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await shoppingCartRepository.Find(id);
        }

        public async Task<IEnumerable<ShoppingCart>> GetShoppingCartFiltered(ShoppingCart shoppingCart)
        {
            if (shoppingCart == null)
            {
                throw new ArgumentNullException(nameof(shoppingCart));
            }

            return await shoppingCartRepository.GetShoppingCartFiltered(shoppingCart);
        }

        public async Task<int> UpdateShoppingCart(ShoppingCart shoppingCart)
        {
            if (shoppingCart == null)
            {
                throw new ArgumentNullException(nameof(shoppingCart));
            }

            var rowsAffected = await shoppingCartRepository.Update(shoppingCart);

            return rowsAffected;
        }
    }
}
