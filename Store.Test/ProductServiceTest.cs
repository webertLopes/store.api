using Moq;
using Store.Application.Interfaces;
using Store.Application.Services;
using Store.Domain.Entities;
using Store.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Store.Test
{
    public class ProductServiceTest
    {
        private readonly IProductService productService;
        private readonly Mock<IProductRepository> productRepositoryMock;

        public ProductServiceTest()
        {
            productRepositoryMock = new Mock<IProductRepository>();
            productService = new ProductService(productRepositoryMock.Object);
        }

        [Fact]
        [Trait(nameof(IProductService.Find), "Success")]
        public async Task GetProductById_Success()
        {
            var expected = new Product
            {
                ProductId = Guid.NewGuid(),
                Code = 10,
                Description = "T-Shirt",
                PriceBase = 190,
                ProductDate = DateTime.Now
            };

            productRepositoryMock
                .Setup(m => m.Find(expected.ProductId))
                .ReturnsAsync(expected);

            var product = await productService.Find(expected.ProductId);

            var actual = product;

            Assert.Equal(expected, actual);
        }

        [Fact]
        [Trait(nameof(ICustomerService.GetCustomerFiltered), "Success")]
        public async Task GetProducts_Success()
        {
            var expected = new List<Product>()
            {
                new Product()
                {
                    ProductId = Guid.NewGuid(),
                    Code = 10,
                    Description = "T-Shirt",
                    PriceBase = 190,
                    ProductDate = DateTime.Now                  
                }
            };

            productRepositoryMock
               .Setup(m => m.GetProductsFiltered(It.IsAny<Product>()))
               .ReturnsAsync(expected);

            var products = await productService.GetProductsFiltered(new Product()
            {
                ProductId = Guid.NewGuid(),
                Code = 10,
                Description = "T-Shirt",
                PriceBase = 190,
                ProductDate = DateTime.Now
            });

            var expectedSingle = expected.Single();

            Assert.Contains(products, f =>
                            f.ProductId == expectedSingle.ProductId &&
                            f.Code == expectedSingle.Code &&
                            f.Description == expectedSingle.Description &&
                            f.PriceBase == expectedSingle.PriceBase);
        }


    }
}
