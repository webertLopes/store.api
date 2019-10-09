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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string ConnectionString;

        public CustomerRepository(IConfiguration configuration)
        {
            ConnectionString = configuration.GetValue<string>("ConnectionString");
        }

        public async Task<IEnumerable<Customer>> GetCustomerFiltered(Customer customer)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                string sqlQuery = @"SELECT * FROM dbo.Customer";

                var result = await db.QueryAsync<Customer>(sqlQuery, customer);

                if (Guid.Empty != customer.CustomerId)
                {
                    result = result.Where(x => x.CustomerId == customer.CustomerId).ToList();
                }

                if (!string.IsNullOrEmpty(customer.Name))
                {
                    result = result.Where(x => x.Name.Contains(customer.Name)).ToList();
                }

                if (!string.IsNullOrEmpty(customer.Cpf))
                {
                    result = result.Where(x => x.Cpf.Contains(customer.Cpf)).ToList();
                }

                return result;
            }
        }

        public async Task<Customer> Find(Guid id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                try
                {
                    return await db.QuerySingleAsync<Customer>("SELECT * FROM dbo.Customer where CustomerId = @id", new { id });
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<int> Update(Customer customer)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                string sqlQuery = @"UPDATE Customer
                                    SET Name = @Name
                                       ,Cpf = @Cpf
                                       ,[Address] = @Address
                                   WHERE CustomerId = @CustomerId";

                try
                {
                    int rowsAffected = await db.ExecuteAsync(sqlQuery, customer);
                    return rowsAffected;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<int> Create(Customer customer)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                string sqlQuery = @"INSERT INTO Customer 
                                                (CustomerId
                                                ,Name
                                                ,Cpf
                                                ,[Address])
                                          VALUES
                                                (@CustomerId
                                                ,@Name
                                                ,@Cpf
                                                ,@Address)";
                try
                {
                    int rowsAffected = await db.ExecuteAsync(sqlQuery, customer);
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
