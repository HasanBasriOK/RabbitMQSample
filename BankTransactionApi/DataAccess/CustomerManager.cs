using BankTransactionApi.Models.DbModel;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankTransactionApi.DataAccess
{
    public static class CustomerManager
    {
        public static Customer GetCustomerByNo(string customerNo)
        {
            var commandString = @"SELECT * FROM Customer WHERE No=@No";
            var result = new Customer();

            using (var connection = new NpgsqlConnection(AppSettings.ConnectionString))
            {
                connection.Open();
                using (var command=new NpgsqlCommand(commandString,connection))
                {
                    command.Parameters.AddWithValue("@No", customerNo);

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : string.Empty;
                        result.Name = reader["Name"] != DBNull.Value ? reader["Name"].ToString() : string.Empty;
                        result.No = reader["No"] != DBNull.Value ? reader["No"].ToString() : string.Empty;
                        result.Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : default(int);
                    }
                }
            }
            return result;
        }
    }
}
