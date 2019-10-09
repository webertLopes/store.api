using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetCustomerFiltered(Customer customer);
        Task<Customer> Find(Guid id);
        Task<int> Create(Customer customer);
        Task<int> UpdateCustomer(Customer customer);
    }
}
