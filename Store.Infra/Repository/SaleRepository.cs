using Dapper;
using Microsoft.Extensions.Configuration;
using Store.Domain.Entities;
using Store.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infra.Repository
{
    public class SaleRepository : ISaleRepository
    {
        private readonly string ConnectionString;
        public SaleRepository(IConfiguration configuration)
        {
            ConnectionString = configuration.GetValue<string>("ConnectionString");
        }
        public async Task<IEnumerable<Sale>> GetSaleFiltered(Sale sale)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                string sqlQuery = @"SELECT * FROM dbo.Sale";

                var result = await db.QueryAsync<Sale>(sqlQuery, sale);

                if (Guid.Empty != sale.SaleId)
                {
                    result = result.Where(x => x.SaleId == sale.SaleId).ToList();
                }

                if (!string.IsNullOrEmpty(sale.Description))
                {
                    result = result.Where(x => x.Description.Contains(sale.Description)).ToList();
                }

                return result;
            }
        }

        public async Task<Sale> Find(Guid id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                try
                {
                    return await db.QuerySingleAsync<Sale>("SELECT * FROM dbo.Sale where SaleId = @id", new { id });
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<int> Create(Sale sale)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                string sqlQuery = @"INSERT INTO Sale
                                           (SaleId
                                           ,[Description]
                                           ,SaleAmount
                                           ,SaleDate
                                           ,ShoppingCartId)
                                     VALUES
                                           (@SaleId
                                           ,@Description
                                           ,@SaleAmount
                                           ,@SaleAmount
                                           ,@ShoppingCartId)";
                try
                {
                    int rowsAffected = await db.ExecuteAsync(sqlQuery, sale);
                    return rowsAffected;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }

            }
        }
        public async Task<int> Update(Sale sale)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                string sqlQuery = @"UPDATE Sale
                                     SET [Description] = @Description
                                        ,SaleAmount = @SaleAmount
                                        ,SaleDate = @SaleDate
                                        ,ShoppingCartId = @ShoppingCartId
                                   WHERE SaleId = @SaleId";

                try
                {
                    int rowsAffected = await db.ExecuteAsync(sqlQuery, sale);
                    return rowsAffected;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }

            }
        }

        public void Dispose()
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                db.Dispose();
            }
        }
    }
}
