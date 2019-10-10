using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Interfaces
{
    public interface ISalesManService
    {
        Task<IEnumerable<SalesMan>> GetSalesManFiltered(SalesMan salesMan);
        Task<int> UpdateSalesMan(SalesMan salesMan);
        Task<int> CreateSalesMan(SalesMan salesMan);
        Task<SalesMan> Find(Guid id);
    }
}
