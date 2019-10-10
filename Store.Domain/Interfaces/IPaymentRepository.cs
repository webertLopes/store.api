using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Interfaces
{
    public interface IPaymentRepository : IDisposable
    {
        Task<IEnumerable<Payment>> GetPaymentFiltered(Payment payment);
        Task<Payment> Find(Guid id);
        Task<int> Create(Payment payment);
        Task<int> Update(Payment payment);
    }
}
