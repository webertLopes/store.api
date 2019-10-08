using FluentValidation;
using Microsoft.AspNetCore.Http;
using Store.Application.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Exceptions;
using Store.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace Store.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public IEnumerable<Product> GetAll()
        {
            return productRepository.GetAll();
        }

        public string ImageToBase64(IFormFile uploadedFile)
        {
            if (uploadedFile == null)
            {
                throw new ArgumentNullException(nameof(uploadedFile));
            }

            using (var ms = new MemoryStream())
            {
                uploadedFile.CopyTo(ms);
                var fileBytes = ms.ToArray();
                return Convert.ToBase64String(fileBytes);
            }
        }

        public int CreateProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var rowsAffected = productRepository.Create(product);

            return rowsAffected;
        }


    }
}
