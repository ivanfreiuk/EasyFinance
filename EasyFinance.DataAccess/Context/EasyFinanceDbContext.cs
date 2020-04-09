using EasyFinance.DataAccess.Entities;
using EasyFinance.DataAccess.Identity;
using EasyFinance.DataAccess.Initializers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EasyFinance.DataAccess.Context
{
    public class EasyFinanceDbContext: IdentityDbContext<User, Role, int>
    {
        public virtual DbSet<Receipt> Receipts { get; set; }

        public virtual DbSet<ReceiptPhoto> ReceiptPhotos { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Currency> Currencies { get; set; }

        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

        public EasyFinanceDbContext(DbContextOptions<EasyFinanceDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new DefaultDatabaseInitializer().Initialize(modelBuilder);
            
            base.OnModelCreating(modelBuilder);
        }


    }
}
