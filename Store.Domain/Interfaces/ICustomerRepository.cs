using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Interfaces
{
    public interface ICustomerRepository : IDisposable
    {
        Task<IEnumerable<Customer>> GetCustomerFiltered(Customer customer);
        Task<Customer> Find(Guid id);
        Task<int> Create(Customer customer);
        Task<int> Update(Customer customer);
    }
}
