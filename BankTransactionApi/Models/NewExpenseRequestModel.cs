using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankTransactionApi.Models
{
    public class NewExpenseRequestModel
    {
        public string CCNo { get; set; }
        public string CustomerNo { get; set; }
        public double Price { get; set; }
        public string CompanyName { get; set; }
    }
}
