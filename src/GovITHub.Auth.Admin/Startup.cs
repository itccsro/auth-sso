using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;

namespace GovITHub.Auth.Admin
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
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

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions
            {
                AuthenticationScheme = "oidc",
                SignInScheme = "Cookies",

                Authority = "http://localhost:5000",
                RequireHttpsMetadata = false,

                ClientId = "mvc",
                ClientSecret = "secret",

                ResponseType = "code id_token",
                Scope = { "api1", "offline_access" },

                GetClaimsFromUserInfoEndpoint = true,
                SaveTokens = true
            });

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }

        //public Startup(IHostingEnvironment env)
        //{
        //    var builder = new ConfigurationBuilder()
        //        .SetBasePath(env.ContentRootPath)
        //        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        //        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
        //        .AddEnvironmentVariables();
        //    Configuration = builder.Build();
        //}

        //public IConfigurationRoot Configuration { get; }

        //// This method gets called by the runtime. Use this method to add services to the container.
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    // Add framework services.
        //    services.AddMvc();
        //}

        //// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        //{
        //    JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //        app.UseBrowserLink();
        //    }
        //    else
        //    {
        //        app.UseExceptionHandler("/Home/Error");
        //    }

        //    loggerFactory.AddConsole(Configuration.GetSection("Logging"));
        //    loggerFactory.AddDebug();

        //    app.UseCookieAuthentication(new CookieAuthenticationOptions
        //    {
        //        AuthenticationScheme = "Cookies"
        //    });

        //    //IdentityServerAuthenticationOptions identityServerValidationOptions = new IdentityServerAuthenticationOptions
        //    //{
        //    //    Authority = "http://localhost:5000",
        //    //    ScopeName = "mvc",
        //    //    ScopeSecret = "secret",
        //    //    AutomaticAuthenticate = true,
        //    //    SupportedTokens = SupportedTokens.Both,
        //    //    // TokenRetriever = _tokenRetriever,
        //    //    // required if you want to return a 403 and not a 401 for forbidden responses

        //    //    AutomaticChallenge = true,
        //    //    RequireHttpsMetadata = false,
        //    //    AuthenticationScheme = "oidc"


        //    //};


        //    //app.UseIdentityServerAuthentication(identityServerValidationOptions);

        //    //var options = new OpenIdConnectOptions("oidc")
        //    //{
        //    //    SignInScheme = "Cookies",
        //    //    // Set the authority to your Auth0 domain
        //    //    Authority = "http://localhost:5000",
        //    //    RequireHttpsMetadata = false,

        //    //    // Configure the Auth0 Client ID and Client Secret
        //    //    ClientId = "mvc",
        //    //    ClientSecret = "secret",

        //    //    // Do not automatically authenticate and challenge
        //    //    AutomaticAuthenticate = false,
        //    //    AutomaticChallenge = false,

        //    //    // Set response type to code
        //    //    ResponseType = "code id_token",

        //    //    // Set the callback path, so Auth0 will call back to http://localhost:5000/signin-auth0 
        //    //    // Also ensure that you have added the URL as an Allowed Callback URL in your Auth0 dashboard 
        //    //    CallbackPath = new PathString("/account")

        //    //};
        //    //options.Scope.Clear();
        //    //options.Scope.Add("api1");
        //    //options.Scope.Add("openid");
        //    //options.Scope.Add("profile");
        //    //app.UseOpenIdConnectAuthentication(options);

        //    app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions
        //    {
        //        AuthenticationScheme = "oidc",
        //        SignInScheme = "Cookies",

        //        Authority = "http://localhost:5000",
        //        RequireHttpsMetadata = false,

        //        ClientId = "mvc",
        //        ClientSecret = "secret",

        //        ResponseType = "code id_token",
        //        Scope = { "api1", "offline_access" },

        //        GetClaimsFromUserInfoEndpoint = true,
        //        SaveTokens = true
        //    });

        //    app.UseStaticFiles();
        //    app.UseMvcWithDefaultRoute();

        //    app.UseStaticFiles();
        //    //app.UseMvc(routes =>
        //    //{
        //    //    routes.MapRoute(
        //    //        name: "default",
        //    //        template: "{controller=Home}/{action=Index}");
        //    //});
        //    app.UseMvcWithDefaultRoute();
        //}
    }
}
