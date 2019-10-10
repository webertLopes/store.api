using Microsoft.AspNetCore.Http;
using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsFiltered(Product product);
        Task<string> ImageToBase64(IFormFile uploadedFile);
        Task<int> CreateProduct(Product product);
        Task<Product> Find(Guid id);
        Task<int> UpdateProduct(Product product);
    }
}
