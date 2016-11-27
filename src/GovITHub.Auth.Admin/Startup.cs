using GovITHub.Auth.Admin.Services;
using GovITHub.Auth.Admin.Services.Impl;
using GovITHub.Auth.Common.Data;
using GovITHub.Auth.Common.Data.Impl;
using GovITHub.Auth.Common.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MySQL.Data.Entity.Extensions;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection;

namespace GovITHub.Auth.Admin
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("connectionstrings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string mySqlConnectionString = Configuration.GetConnectionString("DefaultConnection");
            var migrationsAssembly = typeof(ApplicationUser).GetTypeInfo().Assembly.GetName().Name;      

            services.
                AddEntityFramework().
                AddDbContext<ApplicationDbContext>(options => options.UseMySQL(mySqlConnectionString));

            services.AddMvc();

            services.AddSingleton<ISampleRepository, SampleRepository>();
            services.AddTransient<IOrganizationRepository, OrganizationRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            /*app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Cookies"
            });*/
            var jwtOptions = new JwtBearerOptions()
            {
                Authority = "http://localhost:5000/",
                Audience = "http://localhost:5000/resources",
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,                
                RequireHttpsMetadata = false
            };
            app.UseJwtBearerAuthentication(jwtOptions);


        var angularRoutes = new[] {
                "/forbidden",
                "/authorized",
                "/authorize",
                "/unauthorized",
                "/index",
                "/logoff"
            };

            app.Use(async (context, next) =>
            {
                if (context.Request.Path.HasValue && null != angularRoutes.FirstOrDefault(
                    (ar) => context.Request.Path.Value.StartsWith(ar, StringComparison.OrdinalIgnoreCase)))
                {
                    context.Request.Path = "/";
                }
                await next();
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
