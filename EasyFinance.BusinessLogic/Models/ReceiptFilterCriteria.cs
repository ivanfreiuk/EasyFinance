using System;

namespace EasyFinance.BusinessLogic.Models
{
    public class ReceiptFilterCriteria
    {
        public int? UserId { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public int? CategoryId { get; set; }

        public decimal? TotalFrom { get; set; }

        public decimal? TotalTo { get; set; }
    }
}
