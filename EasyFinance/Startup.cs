using System.Text;
using EasyFinance.Builders;
using EasyFinance.Builders.Interfaces;
using EasyFinance.BusinessLogic.Builders;
using EasyFinance.BusinessLogic.Builders.Interfaces;
using EasyFinance.BusinessLogic.Interfaces;
using EasyFinance.BusinessLogic.Services;
using EasyFinance.Constans;
using EasyFinance.DataAccess.Context;
using EasyFinance.DataAccess.Identity;
using EasyFinance.Helpers;
using EasyFinance.Interfaces;
using EasyFinance.OCR.Interfaces;
using EasyFinance.OCR.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

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
           
            #region Add Entity Framework and Identity Framework

            services.AddDbContext<EasyFinanceDbContext>(options =>
            {
                options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=EasyFinanceDB;Trusted_Connection=True;",
                    b => b.MigrationsAssembly("EasyFinance"));
            });

            services.AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 3;
            }).AddEntityFrameworkStores<EasyFinanceDbContext>().AddDefaultTokenProviders();

            #endregion

            #region Add CORS

            services.AddCors();

            #endregion

            #region Add Authentication

            var tokenSettings = Configuration.GetSection("TokenSettings");
            services.Configure<TokenSettings>(tokenSettings);

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenSettings:Key"]));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = signingKey,
                    ValidateAudience = true,
                    ValidAudience = Configuration["TokenSettings:Audience"],
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["TokenSettings:Issuer"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });

            #endregion

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            #region Add DI for application services
            services.AddTransient<IReceiptPhotoService, ReceiptPhotoService>();
            services.AddTransient<IReceiptService, ReceiptService>();
            services.AddTransient<IPaymentMethodService, PaymentMethodService>();
            services.AddTransient<ICurrencyService, CurrencyService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IReceiptHelper, ReceiptHelper>();
            services.AddTransient<IFileHelper, FileHelper>();
            services.AddSingleton<IOCRService, TesseractOCRService>();
            services.AddTransient<ITokenHelper, TokenHelper>();
            // BUILDERS
            services.AddTransient<IReceiptObjectBuilder, ReceiptObjectBuilder>();
            services.AddTransient<IReceiptObjectDirector, ReceiptObjectDirector>();
            services.AddTransient<IReceiptScanTextBuilder, ReceiptScanTextBuilder>();
            services.AddTransient<IReceiptScanTextDirector, ReceiptScanTextDirector>();
            services.AddTransient<IMapperFactory, MapperFactory>();

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

            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
