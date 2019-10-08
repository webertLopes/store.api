using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Domain.Interfaces
{
    public interface IProductRepository : IDisposable
    {
        IEnumerable<Product> GetProductsFiltered(Product product);
        Product Find(Guid id);
        int Create(Product product);
        int Update(Product product);
    }
}
