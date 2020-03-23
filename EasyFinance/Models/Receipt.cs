using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyFinance.Models
{
    public class Receipt
    {
        public string CategoryName { get; set; }
        public string PaymentMethod { get; set; }

        public decimal? TotalAmount { get; set; }

        public string Currency { get; set; }

        public DateTime? PurchaseDate { get; set; }
    }
}
