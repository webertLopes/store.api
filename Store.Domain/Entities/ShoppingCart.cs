using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Domain.Entities
{
    public class ShoppingCart
    {
        public Guid ShoppingCartId { get; set; }
        public Guid PaymentId { get; set; }
        public string Description { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public DateTime ShoppingCartDate { get; set; }
        public decimal? Price { get; set; }
        public int? Qtd { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual Product Product { get; set; }
    }
}
