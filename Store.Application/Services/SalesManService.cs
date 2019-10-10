using Store.Application.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services
{
    public class SalesManService : ISalesManService
    {
        private readonly ISalesManRepository salesManRepository;
        public SalesManService(ISalesManRepository salesManRepository)
        {
            this.salesManRepository = salesManRepository ?? throw new ArgumentNullException(nameof(salesManRepository));
        }

        public async Task<IEnumerable<SalesMan>> GetSalesManFiltered(SalesMan salesMan)
        {
            if (salesMan == null)
            {
                throw new ArgumentNullException(nameof(salesMan));
            }

            return await salesManRepository.GetSalesManFiltered(salesMan);
        }       


        public async Task<int> UpdateSalesMan(SalesMan salesMan)
        {
            if (salesMan == null)
            {
                throw new ArgumentNullException(nameof(salesMan));
            }

            var rowsAffected = await salesManRepository.Update(salesMan);

            return rowsAffected;
        }


        public async Task<int> CreateSalesMan(SalesMan salesMan)
        {
            if (salesMan == null)
            {
                throw new ArgumentNullException(nameof(salesMan));
            }

            var rowsAffected = await salesManRepository.Create(salesMan);

            return rowsAffected;
        }

        public async Task<SalesMan> Find(Guid id)
        {
            if (Guid.Empty == id)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await salesManRepository.Find(id);
        }

    }
}
