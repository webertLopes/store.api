using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Api.Dtos
{
    public class PaymentPost
    {
        public string Description { get; set; }
        public string FormPayment { get; set; }
        public DateTime PaymentDate { get; set; }
        public Guid SaleId { get; set; }
    }
}
