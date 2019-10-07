using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Application.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
    }
}
