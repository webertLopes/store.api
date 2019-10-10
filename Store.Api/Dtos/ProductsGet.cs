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
    }
}
