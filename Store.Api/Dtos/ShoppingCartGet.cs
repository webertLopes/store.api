using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Api.Dtos
{
    public class ShoppingCartGet
    {
        public Guid ShoppingCartId { get; set; } 
        public string Description { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public Guid SalesManId { get; set; }
        public DateTime ShoppingCartDate { get; set; }
        public decimal? Price { get; set; }
    }
}
