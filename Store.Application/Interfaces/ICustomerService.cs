using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Application.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetCustomerFiltered(Customer customer);
        Customer Find(Guid id);
        int Create(Customer customer);
        int UpdateCustomer(Customer customer);
    }
}
