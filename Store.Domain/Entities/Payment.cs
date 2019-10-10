using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Domain.Entities
{
    public class Payment
    {
        public Guid PaymentId { get; set; }
        public string Description { get; set; }
        public string FormPayment { get; set; }
        public DateTime PaymentDate { get; set; }

        public virtual ICollection<ShoppingCart> ShoppingCart { get; set; }
    }
}
