using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EasyFinance.Constans;
using EasyFinance.Interfaces;
using EasyFinance.Models;

namespace EasyFinance.Builders
{
    public class ReceiptObjectBuilder: IReceiptObjectBuilder
    {
        private Receipt _receipt;

        public ReceiptObjectBuilder()
        {
            _receipt=new Receipt();
        }

        public IReceiptObjectBuilder BuildPaymentMethod(string text)
        {
            return this;
        }

        public IReceiptObjectBuilder BuildTotalAmount(string text)
        {
            var totalPattern = @"(С[аоу]м[а]|Картка):?\s*(\d+\s?.\s?\d{2})";
            var totalMatch = GetFirstSuccessMatch(totalPattern, text);

            var totalText = totalMatch
                ?.Groups
                .LastOrDefault()
                ?.Value
                .Replace(" ", "");

            _receipt.TotalAmount = decimal.TryParse(totalText, out var totalAmount) ? totalAmount : (decimal?) null;

            return this;
        }

        public IReceiptObjectBuilder BuildPurchaseDate(string text)
        {
            //string datetimePattern = @"(\d+)[-.\/](\d+)[-.\/](\d+)\s+((?:0?[0-9]|1[0-9]|2[0-3])[.:][0-5][0-9]$)?";

            var datetime = GetFirstSuccessMatch(RegularExpressionConstants.DATETIME_PATTERN, text)?.Value;//$"{date} {time}";

            _receipt.PurchaseDate = DateTime.TryParse(datetime, out var result) ? result :  (DateTime?) null;

            return this;
        }

        public Receipt GetReceipt()
        {
            var result = _receipt;

            Reset();

            return result;
        }

        public void Reset()
        {
            _receipt = new Receipt();
        }

        private Match GetFirstSuccessMatch(string pattern, string text)
        {
            var regex = new Regex(pattern);

            var collection = regex.Matches(text);

            return collection.FirstOrDefault(c => c.Success);
        }
    }
}
