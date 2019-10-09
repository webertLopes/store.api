using Store.Application.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public async Task<int> Create(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            var rowsAffected = await customerRepository.Create(customer);

            return rowsAffected;
        }

        public async Task<int> UpdateCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            var rowsAffected = await customerRepository.Update(customer);

            return rowsAffected;
        }

        public async Task<Customer> Find(Guid id)
        {
            if (Guid.Empty == id)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await customerRepository.Find(id);
        }

        public async Task<IEnumerable<Customer>> GetCustomerFiltered(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            return await customerRepository.GetCustomerFiltered(customer);
        }
    }
}
