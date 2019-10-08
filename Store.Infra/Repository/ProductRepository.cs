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

        public IEnumerable<Product> GetProductsFiltered(Product product)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                string sqlQuery = @"SELECT * FROM dbo.Product";

                var result = db.Query<Product>(sqlQuery, product).ToList();

                if (Guid.Empty != product.ProductId)
                {
                    result = result.Where(x => x.ProductId == product.ProductId).ToList();
                }

                if (!string.IsNullOrEmpty(product.Description))
                {
                    result = result.Where(x => x.Description.Contains(product.Description)).ToList();
                }

                if (!string.IsNullOrEmpty(product.Code.ToString()))
                {
                    result = result.Where(x => x.Code.ToString().Contains(product.Code.ToString())).ToList();
                }
                
                if (!string.IsNullOrEmpty(product.PriceBase.ToString()))
                {
                    result = result.Where(x => x.PriceBase.ToString().Contains(product.PriceBase.ToString())).ToList();
                }

                if (!string.IsNullOrEmpty(product.Discount.ToString()))
                {
                    result = result.Where(x => x.Discount.ToString().Contains(product.Discount.ToString())).ToList();
                }

                return result;
            }
        }

        public Product Find(Guid id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                try
                {
                    return db.Query<Product>("SELECT * FROM dbo.Product where ProductId = @id", new { id }).SingleOrDefault();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }                
            }
        }
        public int Create(Product product)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                string sqlQuery = @"INSERT INTO Product(ProductId,[Description],Code,PriceBase, ProductDate,Discount,Image) 
                                            VALUES (@ProductId, @Description, @Code, @PriceBase, @ProductDate, @Discount, @Image)";
                try
                {
                    int rowsAffected = db.Execute(sqlQuery, product);
                    return rowsAffected;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
               
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

                try
                {
                    int rowsAffected = db.Execute(sqlQuery, product);
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
