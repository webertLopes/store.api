using FluentValidation;
using Microsoft.AspNetCore.Http;
using Store.Application.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Exceptions;
using Store.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Store.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<IEnumerable<Product>> GetProductsFiltered(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            return await productRepository.GetProductsFiltered(product);
        }

        public async Task<string> ImageToBase64(IFormFile uploadedFile)
        {
            if (uploadedFile == null)
            {
                throw new ArgumentNullException(nameof(uploadedFile));
            }

            using (var ms = new MemoryStream())
            {
                uploadedFile.CopyTo(ms);
                var fileBytes = ms.ToArray();
                return await Task.Run(() => Convert.ToBase64String(fileBytes));
            }
        }


        public async Task<int> UpdateProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var rowsAffected = await productRepository.Update(product);

            return rowsAffected;
        }


        public async Task<int> CreateProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var rowsAffected = await productRepository.Create(product);

            return rowsAffected;
        }

        public async Task<Product> Find(Guid id)
        {
            if (Guid.Empty == id)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await productRepository.Find(id);
        }


    }
}
