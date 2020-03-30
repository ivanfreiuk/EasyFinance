using System.Collections.Generic;
using EasyFinance.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyFinance.DataAccess.Initializers
{
    public class DefaultDatabaseInitializer
    {
        public void Initialize(ModelBuilder builder)
        {
            InitializeCategories(builder);
            InitializeCurrencies(builder);
            InitializePaymentMethods(builder);
        }

        public void InitializeCategories(ModelBuilder builder)
        {
            builder.Entity<Category>()
                .HasData(new List<Category>
                {
                    new Category {Id = 1, Name = "Продукти"},
                    new Category {Id = 2, Name = "Сніданок"},
                    new Category {Id = 3, Name = "Обід"},
                    new Category {Id = 4, Name = "Вечеря"},
                    new Category {Id = 5, Name = "Таксі/Автобус"},
                    new Category {Id = 6, Name = "Потяг"},
                    new Category {Id = 7, Name = "Книги/Журнали"},
                    new Category {Id = 8, Name = "Подарунок"},
                    new Category {Id = 9, Name = "Дозвілля"},
                    new Category {Id = 10, Name = "Одяг"},
                    new Category {Id = 11, Name = "Бензин"},
                    new Category {Id = 12, Name = "Інше"},
                });
        }

        public void InitializeCurrencies(ModelBuilder builder)
        {
            builder.Entity<Currency>()
                .HasData(new List<Currency>
                {
                    new Currency {Id = 1, Name = "Hryvnia", GenericCode = "UAH", MatchPattern = @" ?грн.? ?"},
                    new Currency {Id = 2, Name = "Euro", GenericCode = "EUR"},
                    new Currency {Id = 3, Name = "US Dollar", GenericCode = "USD"}
                });
        }

        public void InitializePaymentMethods(ModelBuilder builder)
        {
            builder.Entity<PaymentMethod>()
                .HasData(new List<PaymentMethod>
                {
                    new PaymentMethod {Id = 1, Name = "Готівка", MatchPattern = @"г[ao]тівк[а]"},
                    new PaymentMethod {Id = 2, Name = "Картка", MatchPattern = @"(к[ао]ртк[ао]|безг[ао]тівк[ао]в[ао])"}
                });
        }
    }
}
