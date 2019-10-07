using Dapper;
using Microsoft.Extensions.Configuration;
using Store.Domain.Entities;
using Store.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Store.Infra.Repository
{
    public class ProductRepository : IProductRepository
    {       
        private readonly string ConnectionString;
        public ProductRepository(IConfiguration configuration)
        {
            ConnectionString = configuration.GetValue<string>("ConnectionString");
        }

        public IEnumerable<Product> GetAll()
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Product>("SELECT * FROM dbo.Product").ToList();
            }
        }
        public Product Find(Guid id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Product>("SELECT * FROM dbo.Product where ProductId = @id", new { id }).SingleOrDefault();
            }
        }
        public int Create(Product product)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                string sqlQuery = @"INSERT INTO Product(ProductId,[Description],Code,PriceBase, ProductDate,Discount,Image) 
                                            VALUES (@ProductId, @Description, @Code, @PriceBase, @ProductDate, @Discount, @Image)";

                int rowsAffected = db.Execute(sqlQuery, product);
                return rowsAffected;
            }
        }
        public int Update(Product product)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                string sqlQuery = @"UPDATE dbo.Product 
                                    SET [Description] = @Description, 
                                        Code = @Code, 
                                        PriceBase = @PriceBase, 
                                        ProductDate = @ProductDate,
                                        Discount = @Discount
                                    WHERE ProductId = @ProductId";

                int rowsAffected = db.Execute(sqlQuery, product);
                return rowsAffected;
            }
        }

    }
}
