using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetPaymentFiltered(Payment payment);
        Task<Payment> Find(Guid id);
        Task<int> Create(Payment payment);
        Task<int> UpdatePayment(Payment payment);
    }
}
