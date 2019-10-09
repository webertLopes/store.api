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

namespace Store.Infra.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string ConnectionString;

        public CustomerRepository(IConfiguration configuration)
        {
            ConnectionString = configuration.GetValue<string>("ConnectionString");
        }

        public IEnumerable<Customer> GetCustomerFiltered(Customer customer)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                string sqlQuery = @"SELECT * FROM dbo.Customer";

                var result = db.Query<Customer>(sqlQuery, customer).ToList();

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

        public Customer Find(Guid id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                try
                {
                    return db.Query<Customer>("SELECT * FROM dbo.Customer where CustomerId = @id", new { id }).SingleOrDefault();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }

        public int Update(Customer customer)
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
                    int rowsAffected = db.Execute(sqlQuery, customer);
                    return rowsAffected;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }
        public int Create(Customer customer)
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
                    int rowsAffected = db.Execute(sqlQuery, customer);
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
