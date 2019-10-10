using Store.Application.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            this.paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
        }

        public async Task<int> Create(Payment payment)
        {
            if (payment == null)
            {
                throw new ArgumentNullException(nameof(payment));
            }

            var rowsAffected = await paymentRepository.Create(payment);

            return rowsAffected;
        }

        public async Task<Payment> Find(Guid id)
        {
            if (Guid.Empty == id)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await paymentRepository.Find(id);
        }

        public async Task<IEnumerable<Payment>> GetPaymentFiltered(Payment payment)
        {
            if (payment == null)
            {
                throw new ArgumentNullException(nameof(payment));
            }

            return await paymentRepository.GetPaymentFiltered(payment);
        }

        public async Task<int> UpdatePayment(Payment payment)
        {
            if (payment == null)
            {
                throw new ArgumentNullException(nameof(payment));
            }

            var rowsAffected = await paymentRepository.Update(payment);

            return rowsAffected;
        }
    }
}
