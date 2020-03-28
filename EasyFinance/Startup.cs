using EasyFinance.Builders;
using EasyFinance.Builders.Interfaces;
using EasyFinance.BusinessLogic.Builders;
using EasyFinance.BusinessLogic.Builders.Interfaces;
using EasyFinance.BusinessLogic.Interfaces;
using EasyFinance.BusinessLogic.Services;
using EasyFinance.Constans;
using EasyFinance.DataAccess.Context;
using EasyFinance.Helpers;
using EasyFinance.Interfaces;
using EasyFinance.OCR.Interfaces;
using EasyFinance.OCR.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyFinance
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var customVisionSecrets = Configuration.GetSection("CustomVisionSecrets");
            services.Configure<CustomVisionSecrets>(customVisionSecrets);

            #region Add Entity Framework

            services.AddDbContext<EasyFinanceDbContext>(options =>
            {
                options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=EasyFinanceDB;Trusted_Connection=True;",
                    b => b.MigrationsAssembly("EasyFinance"));
            });

            #endregion

            services.AddCors();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #region Add DI for application services
            services.AddTransient<IReceiptPhotoService, ReceiptPhotoService>();
            services.AddTransient<IReceiptService, ReceiptService>();
            services.AddTransient<IPaymentMethodService, PaymentMethodService>();
            services.AddTransient<ICurrencyService, CurrencyService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IReceiptHelper, ReceiptHelper>();
            services.AddTransient<IFileHelper, FileHelper>();
            services.AddSingleton<IOCRService, TesseractOCRService>();
            // BUILDERS
            services.AddTransient<IReceiptObjectBuilder, ReceiptObjectBuilder>();
            services.AddTransient<IReceiptObjectDirector, ReceiptObjectDirector>();
            services.AddTransient<IReceiptScanTextBuilder, ReceiptScanTextBuilder>();
            services.AddTransient<IReceiptScanTextDirector, ReceiptScanTextDirector>();
            

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
