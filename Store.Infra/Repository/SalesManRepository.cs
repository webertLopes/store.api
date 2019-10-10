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
    public class SalesManRepository : ISalesManRepository
    {
        private readonly string ConnectionString;

        public SalesManRepository(IConfiguration configuration)
        {
            ConnectionString = configuration.GetValue<string>("ConnectionString");
        }

        public async Task<IEnumerable<SalesMan>> GetSalesManFiltered(SalesMan salesMan)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                string sqlQuery = @"SELECT * FROM dbo.SalesMan";

                var result = await db.QueryAsync<SalesMan>(sqlQuery, salesMan);

                if (Guid.Empty != salesMan.SalesManId)
                {
                    result = result.Where(x => x.SalesManId == salesMan.SalesManId).ToList();
                }

                if (!string.IsNullOrEmpty(salesMan.Name))
                {
                    result = result.Where(x => x.Name.Contains(salesMan.Name)).ToList();
                }

                return result;
            }
        }

        public async Task<SalesMan> Find(Guid id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                try
                {
                    return await db.QuerySingleAsync<SalesMan>("SELECT * FROM dbo.SalesMan where SalesManId = @id", new { id });
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<int> Create(SalesMan salesMan)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                string sqlQuery = @"INSERT INTO SalesMan
                                    (SalesManId ,Name)
                              VALUES
                                    (@SalesManId ,@Name)";
                try
                {
                    int rowsAffected = await db.ExecuteAsync(sqlQuery, salesMan);
                    return rowsAffected;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }

            }
        }
        public async Task<int> Update(SalesMan salesMan)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                string sqlQuery = @"UPDATE SalesMan
                                     SET Name = @Name
                                   WHERE SalesManId = @SalesManId";

                try
                {
                    int rowsAffected = await db.ExecuteAsync(sqlQuery, salesMan);
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
