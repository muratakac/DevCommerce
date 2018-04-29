using DevCommerce.Entities;
using DevCommerce.WebUI.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace DevCommerce.WebUI
{
    public class Startup
    {
        //https://www.codeproject.com/Articles/1205161/ASP-NET-Core-Cookie-Authentication
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication("FiverSecurityScheme")
                   .AddCookie("FiverSecurityScheme", options =>
                   {
                       options.AccessDeniedPath = new PathString("/Login/Access");
                       options.LoginPath = new PathString("/Login/Index");
                       options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                       options.SlidingExpiration = true;
                       options.Events = new CookieAuthenticationEvents
                       {
                           OnSignedIn = context =>
                           {
                               //Console.WriteLine("{0} - {1}: {2}", DateTime.Now,
                               //  "OnSignedIn", context.Principal.Identity.Name);
                               return Task.CompletedTask;
                           },
                           OnSigningOut = context =>
                           {
                               //Console.WriteLine("{0} - {1}: {2}", DateTime.Now,
                               //  "OnSigningOut", context.HttpContext.User.Identity.Name);
                               return Task.CompletedTask;
                           },
                           OnValidatePrincipal = context =>
                           {
                               //Console.WriteLine("{0} - {1}: {2}", DateTime.Now,
                               //  "OnValidatePrincipal", context.Principal.Identity.Name);
                               return Task.CompletedTask;
                           },
                       };
                   });

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });

            services.AddScoped(typeof(Basket<>), typeof(Basket<>));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            FileExtensionContentTypeProvider typeProvider = new FileExtensionContentTypeProvider();
            typeProvider.Mappings.Remove(".eot");
            typeProvider.Mappings.Remove(".woff");
            typeProvider.Mappings.Remove(".ttf");
            typeProvider.Mappings.Remove(".svg");
            typeProvider.Mappings.Remove(".woff2");
            typeProvider.Mappings.Remove(".otf");

            typeProvider.Mappings[".eot"] = "application/vnd.ms-fontobject";
            typeProvider.Mappings[".woff"] = "application/font-woff";
            typeProvider.Mappings[".ttf"] = "application/font-sfnt";
            typeProvider.Mappings[".svg"] = "image/svg+xml";
            typeProvider.Mappings[".woff2"] = "font/woff2";
            typeProvider.Mappings[".otf"] = "font/otf";

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "Contents")),
                RequestPath = "/Contents",
                ContentTypeProvider = typeProvider
            });

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute("productList", "Product/Index/{CategoryId}", defaults: new { controller = "Product", action = "Index"});
                routes.MapRoute("productDetail", "Product/Detail/{ProductId}", defaults: new { controller = "Product", action = "Detail" });
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });


        }
    }
}

