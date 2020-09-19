using BankTransactionApi.Models.DbModel;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankTransactionApi.DataAccess
{
    public static class ExpenseManager
    {
        public static void CreateExpense(Expense expense)
        {
            var commandString = @"INSERT INTO Expense 
(CustomerNo,
Price,
CompanyName) 
VALUES(@CustomerNo,
@Price,
@CompanyName)";

            using (var connection=new NpgsqlConnection(AppSettings.ConnectionString))
            {
                connection.Open();
                using (var command=new NpgsqlCommand(commandString,connection))
                {
                    command.Parameters.AddWithValue("@CustomerNo", expense.CustomerNo);
                    command.Parameters.AddWithValue("@Price", expense.Price);
                    command.Parameters.AddWithValue("@CompanyName", expense.CompanyName);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
