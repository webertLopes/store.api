﻿using System;
using System.Collections.Generic;

namespace Store.Domain.Entities
{
    public class Product
    {
        public Guid ProductId { get; set; } = Guid.NewGuid();
        public string Description { get; set; }
        public int? Code { get; set; }
        public decimal? PriceBase { get; set; }
        public DateTime ProductDate { get; set; }
        public double? Discount { get; set; }
        public string Image { get; set; }

        public virtual ICollection<ShoppingCart> ShoppingCart { get; set; }
    }
}
