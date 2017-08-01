﻿//using System.Reflection;
//using GovITHub.Auth.Common.Models;
//using IdentityServer4.EntityFramework.DbContexts;
//using IdentityServer4.EntityFramework.Options;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using MySQL.Data.Entity.Extensions;
//using Localization.SqlLocalizer.DbStringLocalizer;

//namespace GovITHub.Auth.Common.Data
//{
//    public class ApplicationDbContextFactory : IDbContextFactory<ApplicationDbContext>
//    {
//        public ApplicationDbContext Create(DbContextFactoryOptions options)
//        {
//            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();           
//            optionsBuilder.UseMySQL("DefaultConnection;");

//            return new ApplicationDbContext(optionsBuilder.Options);
//        }
//    }

//    public class ConfigurationDbContextFactory : IDbContextFactory<ConfigurationDbContext>
//    {
//        public ConfigurationDbContext Create(DbContextFactoryOptions options)
//        {
//            var optionsBuilder = new DbContextOptionsBuilder<ConfigurationDbContext>();
//            var migrationsAssembly = typeof(ApplicationUser).GetTypeInfo().Assembly.GetName().Name;
//            optionsBuilder.UseMySQL("DefaultConnection;", opts => opts.MigrationsAssembly(migrationsAssembly));

//            return new ConfigurationDbContext(optionsBuilder.Options, new ConfigurationStoreOptions());
//        }
//    }
//    public class LocalizationDbContextFactory : IDbContextFactory<LocalizationModelContext>
//    {
//        public LocalizationModelContext Create(DbContextFactoryOptions options)
//        {
//            var optionsBuilder = new DbContextOptionsBuilder<LocalizationModelContext>();
//            var migrationsAssembly = typeof(ApplicationUser).GetTypeInfo().Assembly.GetName().Name;
//            optionsBuilder.UseMySQL("DefaultConnection;", opts => opts.MigrationsAssembly(migrationsAssembly));

//            SqlContextOptions ctx = new SqlContextOptions();
//            ctx.SqlSchemaName = "dbo";

//            return new LocalizationModelContext(optionsBuilder.Options);
//        }
//    }
//}
