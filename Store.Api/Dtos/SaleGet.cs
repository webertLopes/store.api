using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Api.Dtos
{
    public class SaleGet
    {
        public Guid SaleId { get; set; }
        public string Description { get; set; }
        public decimal? SaleAmount { get; set; }
        public DateTime SaleDate { get; set; }
        public Guid ShoppingCartId { get; set; }
    }
}
