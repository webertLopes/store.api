using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Domain.Interfaces
{
    public interface ICustomerRepository : IDisposable
    {
        IEnumerable<Customer> GetCustomerFiltered(Customer customer);
        Customer Find(Guid id);
        int Create(Customer customer);
        int Update(Customer customer);
    }
}
