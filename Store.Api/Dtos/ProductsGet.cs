using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Api.Dtos
{
    public class ProductsGet
    {      
        public string Description { get; set; }
        public int? Code { get; set; }                
        public double Discount { get; set; }
    }
}
