using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Api.Dtos
{
    public class ProductGetResult
    {
        public Guid ProductId { get; set; } 
        public string Description { get; set; }
        public int? Code { get; set; }
        public decimal? PriceBase { get; set; }
        public DateTime ProductDate { get; set; }
        public double? Discount { get; set; }
        public string Image { get; set; }
    }
}
