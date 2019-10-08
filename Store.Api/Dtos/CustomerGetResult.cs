using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Api.Dtos
{
    public class CustomerGetResult
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Address { get; set; }
    }
}
