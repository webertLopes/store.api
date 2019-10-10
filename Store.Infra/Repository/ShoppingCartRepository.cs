using Dapper;
using Microsoft.Extensions.Configuration;
using Store.Domain.Entities;
using Store.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Infra.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly string ConnectionString;
        public ShoppingCartRepository(IConfiguration configuration)
        {
            ConnectionString = configuration.GetValue<string>("ConnectionString");
        }

        public async Task<IEnumerable<ShoppingCart>> GetShoppingCartFiltered(ShoppingCart shoppingCart)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                string sqlQuery = @"SELECT * FROM dbo.ShoppingCart";

                var result = await db.QueryAsync<ShoppingCart>(sqlQuery, shoppingCart);

                if (!string.IsNullOrEmpty(shoppingCart.Description))
                {
                    result = result.Where(x => x.Description.Contains(shoppingCart.Description)).ToList();
                }

                return result;
            }
        }

        public async Task<ShoppingCart> Find(Guid id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                try
                {
                    return await db.QuerySingleAsync<ShoppingCart>("SELECT * FROM dbo.ShoppingCart where ShoppingCartId = @id", new { id });
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<int> Create(ShoppingCart shoppingCart)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                string sqlQuery = @"INSERT INTO ShoppingCart
                                           ([ShoppingCartId]
                                           ,[PaymentId]
                                           ,[Description]
                                           ,[CustomerId]
                                           ,[ProductId]
                                           ,[ShoppingCartDate]
                                           ,[Price]
                                           ,[Qtd])
                                     VALUES
                                           (@ShoppingCartId
                                           ,@PaymentId
                                           ,@Description
                                           ,@CustomerId
                                           ,@ProductId
                                           ,@ShoppingCartDate
                                           ,@Price
                                           ,@Qtd)";
                try
                {
                    int rowsAffected = await db.ExecuteAsync(sqlQuery, shoppingCart);
                    return rowsAffected;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }

            }
        }
        public async Task<int> Update(ShoppingCart shoppingCart)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                string sqlQuery = @"UPDATE ShoppingCart
                                     SET [PaymentId] = @PaymentId
                                        ,[Description] = @Description
                                        ,[CustomerId] = @CustomerId
                                        ,[ProductId] = @ProductId
                                        ,[ShoppingCartDate] = @ShoppingCartDate
                                        ,[Price] = @Price
                                        ,[Qtd] = @Qtd
                                   WHERE [ShoppingCartId] = @ShoppingCartId";

                try
                {
                    int rowsAffected = await db.ExecuteAsync(sqlQuery, shoppingCart);
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
