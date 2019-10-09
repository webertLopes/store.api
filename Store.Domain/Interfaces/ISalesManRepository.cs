using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Domain.Interfaces
{
    public interface ISalesManRepository : IDisposable
    {
        IEnumerable<SalesMan> GetSalesManFiltered(SalesMan salesMan);
        SalesMan Find(Guid id);
        int Create(SalesMan salesMan);
        int Update(SalesMan salesMan);
    }
}
