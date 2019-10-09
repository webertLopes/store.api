using Store.Application.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Application.Services
{
    public class SalesManService : ISalesManService
    {
        private readonly ISalesManRepository salesManRepository;
        public SalesManService(ISalesManRepository salesManRepository)
        {
            this.salesManRepository = salesManRepository ?? throw new ArgumentNullException(nameof(salesManRepository));
        }

        public IEnumerable<SalesMan> GetSalesManFiltered(SalesMan salesMan)
        {
            if (salesMan == null)
            {
                throw new ArgumentNullException(nameof(salesMan));
            }

            return salesManRepository.GetSalesManFiltered(salesMan);
        }       


        public int UpdateSalesMan(SalesMan salesMan)
        {
            if (salesMan == null)
            {
                throw new ArgumentNullException(nameof(salesMan));
            }

            var rowsAffected = salesManRepository.Update(salesMan);

            return rowsAffected;
        }


        public int CreateSalesMan(SalesMan salesMan)
        {
            if (salesMan == null)
            {
                throw new ArgumentNullException(nameof(salesMan));
            }

            var rowsAffected = salesManRepository.Create(salesMan);

            return rowsAffected;
        }

        public SalesMan Find(Guid id)
        {
            if (Guid.Empty == id)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return salesManRepository.Find(id);
        }

    }
}
