using System;
using System.Collections.Generic;

namespace Store.Domain.Entities
{
    public class Sale
    {
        public Guid SaleId { get; set; } = Guid.NewGuid();
        public string Description { get; set; }
        public decimal? SaleAmount { get; set; }
        public DateTime SaleDate { get; set; }
        public Guid ShoppingCartId { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }
    }
}
