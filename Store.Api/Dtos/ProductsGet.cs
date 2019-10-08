using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Api.Dtos
{
    public class ProductsGet
    {

        public Guid ProductId { get; set; } 
        public string Description { get; set; }
        public int? Code { get; set; }
        public decimal? PriceBase { get; set; }
        public double? Discount { get; set; }
    }
}
