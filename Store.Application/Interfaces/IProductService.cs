using Microsoft.AspNetCore.Http;
using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Application.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetProductsFiltered(Product product);
        string ImageToBase64(IFormFile uploadedFile);
        int CreateProduct(Product product);
    }
}
