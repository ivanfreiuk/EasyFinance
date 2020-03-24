using System;
using System.Collections.Generic;
using System.Text;

namespace EasyFinance.DataAccess.Entities
{
    public class Receipt
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public string PaymentMethod { get; set; }

        public decimal? TotalAmount { get; set; }

        public int CurrencyId { get; set; }

        public Currency Currency { get; set; }

        public DateTime? PurchaseDate { get; set; }
    }
}
