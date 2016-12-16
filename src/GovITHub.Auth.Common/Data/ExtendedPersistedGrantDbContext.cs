using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MySQL.Data.Entity.Extensions;

public class ExtendedPersistedGrantDbContext : PersistedGrantDbContext
    {
        OperationalStoreOptions _storeOptions;
        public ExtendedPersistedGrantDbContext(DbContextOptions<PersistedGrantDbContext> options, OperationalStoreOptions storeOptions) : 
            base(options , storeOptions)
        {
            _storeOptions = storeOptions;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        // perform original build
            System.Console.WriteLine("ExtendedPersistentGrantDbContext OnModelCreating");
            modelBuilder.Entity<IdentityServer4.EntityFramework.Entities.PersistedGrant>(grant => 
            { 
                grant.Property(x => x.Data).HasMaxLength(8196).IsRequired(); 
            }); 
            base.OnModelCreating(modelBuilder);
        }

        public class ExtendedPersistedGrantDbContextFactory : IDbContextFactory<ExtendedPersistedGrantDbContext>
        {
            public ExtendedPersistedGrantDbContext Create(DbContextFactoryOptions options)
            {
                var builder = new DbContextOptionsBuilder<PersistedGrantDbContext>();
                builder.UseMySQL("DefaultConnection");
                return new ExtendedPersistedGrantDbContext(builder.Options,
                    new OperationalStoreOptions() );
            }
        }        
    }