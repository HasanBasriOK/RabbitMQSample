using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankTransactionApi.Models
{
    public class InformationModel
    {
        public DateTime date { get; set; }
        public string email { get; set; }
        public double price { get; set; }
        public string lastSixCharacter { get; set; }
    }
}
