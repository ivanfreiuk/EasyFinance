using System;
using EasyFinance.DataAccess.Entities;

namespace EasyFinance.ViewModels
{
    public class ReceiptModel
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Merchant { get; set; }

        public string Description { get; set; }

        public int? ReceiptPhotoId { get; set; }

        public ReceiptPhoto ReceiptPhoto { get; set; }

        public int? CategoryId { get; set; }

        public Category Category { get; set; }

        public int? PaymentMethodId { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public decimal? TotalAmount { get; set; }

        public int? CurrencyId { get; set; }

        public Currency Currency { get; set; }

        public DateTime? PurchaseDate { get; set; }
    }
}
