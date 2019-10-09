using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Api.Dtos
{
    public class SalesManGetResult
    {
        public Guid SalesManId { get; set; } 
        public string Name { get; set; }
    }
}
