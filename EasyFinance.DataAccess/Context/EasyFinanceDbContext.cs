using EasyFinance.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyFinance.DataAccess.Context
{
    public class EasyFinanceDbContext: DbContext
    {
        public virtual DbSet<Receipt> Receipts { get; set; }

        public virtual DbSet<ReceiptPhoto> ReceiptPhotos { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Currency> Currencies { get; set; }

        public EasyFinanceDbContext(DbContextOptions<EasyFinanceDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
