using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using EasyFinance.BusinessLogic.Builders.Interfaces;
using EasyFinance.BusinessLogic.Constans;
using EasyFinance.BusinessLogic.Interfaces;
using EasyFinance.DataAccess.Entities;

namespace EasyFinance.BusinessLogic.Builders
{
    public class ReceiptObjectBuilder: IReceiptObjectBuilder
    {
        private readonly IPaymentMethodService _paymentMethodService;
        private readonly ICurrencyService _currencyService;
        private Receipt _receipt;

        public ReceiptObjectBuilder(IPaymentMethodService paymentMethodService, ICurrencyService currencyService)
        {
            _paymentMethodService = paymentMethodService;
            _currencyService = currencyService;
            _receipt=new Receipt();
        }

        public IReceiptObjectBuilder BuildPaymentMethod(string text)
        {
            var paymentMethods = _paymentMethodService.GetPaymentMethodsAsync().Result;

            var paymentMethodId = paymentMethods.FirstOrDefault(pm => !string.IsNullOrWhiteSpace(pm.MatchPattern) && Regex.IsMatch(text, pm.MatchPattern, RegexOptions.IgnoreCase))?.Id;

            _receipt.PaymentMethodId = paymentMethodId;

            return this;
        }

        public IReceiptObjectBuilder BuildCurrency(string text)
        {
            var currencies = _currencyService.GetCurrenciesAsync().Result;

            var currencyId = currencies.FirstOrDefault(c => !string.IsNullOrWhiteSpace(c.MatchPattern) && Regex.IsMatch(text, c.MatchPattern, RegexOptions.IgnoreCase))?.Id;

            _receipt.CategoryId = currencyId;

            return this;
        }

        public IReceiptObjectBuilder BuildTotalAmount(string text)
        {
            var totalText = RegularExpressions.TotalAmount
                .Match(text)
                .Groups
                .LastOrDefault()
                ?.Value;
            
            var totalValue = Regex.Replace(totalText ?? string.Empty, "[,;:]", ".").Replace(" ", "");
            
            _receipt.TotalAmount = decimal.TryParse(totalValue, out var totalAmount) ? totalAmount : 0;

            return this;
        }

        public IReceiptObjectBuilder BuildPurchaseDate(string text)
        {
            var datetimeText = RegularExpressions.Datetime.Match(text).Value;
            
            var cultureInfo = new CultureInfo("uk-UA");
            var isParsed = DateTime.TryParseExact(datetimeText, cultureInfo.DateTimeFormat.ShortDatePattern,  cultureInfo, DateTimeStyles.None, out var result);

           _receipt.PurchaseDate = isParsed ? result :  DateTime.Now;

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
    }
}
