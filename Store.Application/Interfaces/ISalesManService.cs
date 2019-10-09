using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Application.Interfaces
{
    public interface ISalesManService
    {
        IEnumerable<SalesMan> GetSalesManFiltered(SalesMan salesMan);
        int UpdateSalesMan(SalesMan salesMan);
        int CreateSalesMan(SalesMan salesMan);
        SalesMan Find(Guid id);
    }
}
