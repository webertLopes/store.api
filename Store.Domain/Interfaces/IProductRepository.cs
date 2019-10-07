using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Domain.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product Find(Guid id);
        int Create(Product product);
        int Update(Product product);
    }
}
