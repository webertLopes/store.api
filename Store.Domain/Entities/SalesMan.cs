using System;
using System.Collections.Generic;

namespace Store.Domain.Entities
{
    public class SalesMan
    {
        public Guid SalesManId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ShoppingCart> ShoppingCart { get; set; }
    }
}
