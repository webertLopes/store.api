using Moq;
using Store.Application.Interfaces;
using Store.Application.Services;
using Store.Domain.Entities;
using Store.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Store.Test
{
    public class CustomerServiceTests
    {
        private readonly ICustomerService customerService;
        private readonly Mock<ICustomerRepository> customerRepositoryMock;

        public CustomerServiceTests()
        {
            customerRepositoryMock = new Mock<ICustomerRepository>();
            customerService = new CustomerService(customerRepositoryMock.Object);
        }

        [Fact]
        [Trait(nameof(ICustomerService.Find), "Success")]
        public void GetCustomerById_Success()
        {
            var expected = new Customer
            {
                CustomerId = Guid.NewGuid(),
                Name = "Barack Obama",
                Cpf = "67323320747",
                Address = "134 Wooster Street, Soho Nova Iorque"
            };

            customerRepositoryMock
                .Setup(m => m.Find(expected.CustomerId))
                .Returns(expected);

            var customer = customerService.Find(expected.CustomerId);

            var actual = customer;

            Assert.Equal(expected, actual);
        }

        [Fact]
        [Trait(nameof(ICustomerService.GetCustomerFiltered), "Success")]
        public void GetCustomer_Success()
        {
            var expected = new List<Customer>()
            {
                new Customer()
                {
                    CustomerId = Guid.NewGuid(),
                    Name = "Barack Obama",
                    Cpf = "67323320747",
                    Address = "134 Wooster Street, Soho Nova Iorque"
                }
            };

            customerRepositoryMock
                .Setup(m => m.GetCustomerFiltered(It.IsAny<Customer>()))
                .Returns(expected);

            var customers = customerService.GetCustomerFiltered(new Customer()
            {
                CustomerId = Guid.NewGuid(),
                Name = "Barack Obama",
                Cpf = "67323320747",
                Address = "134 Wooster Street, Soho Nova Iorque"
            });

            var expectedSingle = expected.Single();

            Assert.Contains(customers, f =>
                            f.CustomerId == expectedSingle.CustomerId &&
                            f.Name == expectedSingle.Name &&
                            f.Cpf == expectedSingle.Cpf &&
                            f.Address == expectedSingle.Address);
        }


    }
}
