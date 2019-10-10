using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Interfaces
{
    public interface IProductRepository : IDisposable
    {
        Task<IEnumerable<Product>> GetProductsFiltered(Product product);
        Task<Product> Find(Guid id);
        Task<int> Create(Product product);
        Task<int> Update(Product product);
    }
}
