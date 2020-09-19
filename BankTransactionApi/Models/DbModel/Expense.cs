using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankTransactionApi.Models.DbModel
{
    public class Expense
    {
        public int ExpenseId { get; set; }
        public string CustomerNo { get; set; }
        public double Price { get; set; }
        public string CompanyName { get; set; }

    }
}
