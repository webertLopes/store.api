using System;
using System.Collections.Generic;

namespace Store.Domain.Entities
{
    public class Payment
    {
        public Guid PaymentId { get; set; } = Guid.NewGuid();
        public string Description { get; set; }
        public string FormPayment { get; set; }
        public DateTime PaymentDate { get; set; }
        public Guid SaleId { get; set; }

        public virtual Sale Sale { get; set; }
    }
}
