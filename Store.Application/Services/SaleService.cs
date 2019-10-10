using Store.Application.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository saleRepository;

        public SaleService(ISaleRepository saleRepository)
        {
            this.saleRepository = saleRepository ?? throw new ArgumentNullException(nameof(saleRepository));
        }

        public async Task<int> Create(Sale sale)
        {
            if (sale == null)
            {
                throw new ArgumentNullException(nameof(sale));
            }

            var rowsAffected = await saleRepository.Create(sale);

            return rowsAffected;
        }

        public async Task<Sale> Find(Guid id)
        {
            if (Guid.Empty == id)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await saleRepository.Find(id);
        }

        public async Task<IEnumerable<Sale>> GetSaleFiltered(Sale sale)
        {
            if (sale == null)
            {
                throw new ArgumentNullException(nameof(sale));
            }

            return await saleRepository.GetSaleFiltered(sale);
        }

        public async Task<int> UpdateSale(Sale sale)
        {
            if (sale == null)
            {
                throw new ArgumentNullException(nameof(sale));
            }

            var rowsAffected = await saleRepository.Update(sale);

            return rowsAffected;
        }
    }
}
