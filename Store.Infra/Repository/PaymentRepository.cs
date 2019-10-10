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
    public class PaymentRepository : IPaymentRepository
    {
        private readonly string ConnectionString;

        public PaymentRepository(IConfiguration configuration)
        {
            ConnectionString = configuration.GetValue<string>("ConnectionString");
        }

        public async Task<IEnumerable<Payment>> GetPaymentFiltered(Payment payment)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                string sqlQuery = @"SELECT * FROM dbo.Payment";

                var result = await db.QueryAsync<Payment>(sqlQuery, payment);

                if (Guid.Empty != payment.PaymentId)
                {
                    result = result.Where(x => x.PaymentId == payment.PaymentId).ToList();
                }

                if (!string.IsNullOrEmpty(payment.Description))
                {
                    result = result.Where(x => x.Description.Contains(payment.Description)).ToList();
                }

                return result;
            }
        }

        public async Task<Payment> Find(Guid id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                try
                {
                    return await db.QuerySingleAsync<Payment>("SELECT * FROM dbo.Payment where PaymentId = @id", new { id });
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<int> Create(Payment payment)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                string sqlQuery = @"INSERT INTO Payment
                                          (PaymentId
                                          ,[Description]
                                          ,FormPayment
                                          ,PaymentDate
                                          ,SaleId)
                                    VALUES
                                          (@PaymentId
                                          ,@Description
                                          ,@FormPayment
                                          ,@PaymentDate
                                          ,@SaleId)";
                try
                {
                    int rowsAffected = await db.ExecuteAsync(sqlQuery, payment);
                    return rowsAffected;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }

            }
        }
        public async Task<int> Update(Payment payment)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                string sqlQuery = @"UPDATE Payment
                                      SET [Description] = @Description
                                         ,FormPayment = @FormPayment
                                         ,PaymentDate = @PaymentDate
                                         ,SaleId = @SaleId
                                    WHERE PaymentId = @PaymentId";
                try
                {
                    int rowsAffected = await db.ExecuteAsync(sqlQuery, payment);
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
