using AutoMapper;
using DevCommerce.Business.Abstract;
using DevCommerce.Business.Concrete;
using DevCommerce.Core.CrossCuttingConcerns.Email;
using DevCommerce.Core.Entities.AppSettingsModels;
using DevCommerce.DataAccess.Concrete.DapperRepositories;
using DevCommerce.DataAccess.Concrete.DapperRepositories.Abstract;
//using DevCommerce.DataAccess.Concrete.EntityFramework;
//using DevCommerce.DataAccess.Concrete.EntityFramework.Abstract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace DevCommerce.WebApi
{
    /*
     Automapper References
     https://stackoverflow.com/questions/40275195/how-to-setup-automapper-in-asp-net-core

         */
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<DevCommerceContext>(o => o.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddIdentity<User, Role>(config =>
            //{
            //    //Email Confirm
            //    //config.SignIn.RequireConfirmedEmail = true;
            //}).AddEntityFrameworkStores<DevCommerceContext>().AddDefaultTokenProviders();

            services.AddScoped<DataAccess.Concrete.DapperRepositories.Abstract.IConnectionFactory, ConnectionHelper>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            //Token based authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
           )
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateAudience = true,
                   ValidAudience = "DevCommerce.Core",
                   ValidateIssuer = true,
                   ValidIssuer = "DevCommerce.Core",
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes("dev-security-value-wep-api"))
               };

               options.Events = new JwtBearerEvents
               {
                   OnTokenValidated = ctx =>
                   {
                       return Task.CompletedTask;
                   },
                   OnAuthenticationFailed = ctx =>
                   {
                       Console.WriteLine("Exception:{0}", ctx.Exception.Message);
                       return Task.CompletedTask;
                   }
               };
           });

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IStringLocalizer, LocalizationService>();


            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IProductRepository,ProductRepository>();
            services.AddScoped<IOrderRepository,OrderRepository>();
            services.AddScoped<ITokenRepository,TokenRepository>();
            services.AddScoped<ICultureRepository, CultureRepository>();
            services.AddScoped<IResourceRepository, ResourceRepository>();

            services.AddScoped<IEmailSender, EmailSender>();

            services.Configure<DapperConectionOptions>(Configuration.GetSection("ConnectionStrings"));
            services.Configure<AuthMessageSenderOptions>(Configuration.GetSection("AuthMessageSenderOptions"));
            services.Configure<JwtTokenParameter>(Configuration.GetSection("JwtTokenParameters"));
            services.Configure<EmailParameter>(Configuration.GetSection("EmailParameters"));

            services.AddAutoMapper();
            services.AddCors();
            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            //services.AddDistributedRedisCache(option =>
            //{
            //    option.Configuration = "127.0.0.1:6379";
            //    option.InstanceName = "master";
            //});
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-US"),
                new CultureInfo("en-AU"),
                new CultureInfo("en-GB"),
                new CultureInfo("es-ES"),
                new CultureInfo("ja-JP"),
                new CultureInfo("fr-FR"),
                new CultureInfo("zh"),
                new CultureInfo("zh-CN"),
                new CultureInfo("tr-TR")
            };

            var options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            app.UseRequestLocalization(options);

            app.UseAuthentication();

            app.UseCors(builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            app.UseMvc();
        }
    }
}


