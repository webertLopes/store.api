using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Interfaces
{
    public interface ISalesManRepository : IDisposable
    {
        Task<IEnumerable<SalesMan>> GetSalesManFiltered(SalesMan salesMan);
        Task<SalesMan> Find(Guid id);
        Task<int> Create(SalesMan salesMan);
        Task<int> Update(SalesMan salesMan);
    }
}
