using System;
using System.Collections.Generic;

namespace Store.Domain.Entities
{
    public class ShoppingCart
    {
        public Guid ShoppingCartId { get; set; } = Guid.NewGuid();
        public string Description { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public Guid SalesManId { get; set; }
        public DateTime ShoppingCartDate { get; set; }
        public decimal? Price { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
        public virtual SalesMan SalesMan { get; set; }
        public virtual ICollection<Sale> Sale { get; set; }
    }
}
