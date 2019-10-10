using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Domain.Entities
{
    public class Customer
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Address { get; set; }

        public virtual ICollection<ShoppingCart> ShoppingCart { get; set; }
    }
}
