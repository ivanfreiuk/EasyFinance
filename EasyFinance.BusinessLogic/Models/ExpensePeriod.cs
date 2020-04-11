using System;

namespace EasyFinance.BusinessLogic.Models
{
    public class ExpensePeriod
    {
        public int UserId { get; set; }

        public DateTime PurchaseDate { get; set; }

        public decimal Total { get; set; }
    }
}
