using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Domain.Entities
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int? Code { get; set; }
        public decimal? PriceBase { get; set; }
        public DateTime ProductDate { get; set; }
        public string ImageOne { get; set; }
        public string ImageTwo { get; set; }
        public string Size { get; set; }
        public string Collor { get; set; }
        public int? QtyStock { get; set; }
        public int? Discount { get; set; }

        public virtual ICollection<ShoppingCart> ShoppingCart { get; set; }
    }
}
