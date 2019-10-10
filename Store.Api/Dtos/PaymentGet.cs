using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Api.Dtos
{
    public class PaymentGet
    {
        public Guid PaymentId { get; set; }
        public string Description { get; set; }
        public string FormPayment { get; set; }
        public DateTime PaymentDate { get; set; }
        public Guid SaleId { get; set; }
    }
}
