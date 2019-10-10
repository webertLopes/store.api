using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Interfaces
{
    public interface ISaleRepository : IDisposable
    {
        Task<IEnumerable<Sale>> GetSaleFiltered(Sale sale);
        Task<Sale> Find(Guid id);
        Task<int> Create(Sale sale);
        Task<int> Update(Sale sale);
    }
}
