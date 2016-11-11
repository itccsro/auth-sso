using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using IdentityServer4.EntityFramework.DbContexts;

namespace GovITHub.Auth.Identity.Data.Migrations.PersistedGrantDb
{
    [DbContext(typeof(PersistedGrantDbContext))]
    [Migration("20161110100904_InitialIdentityServerPersistedGrantDbMigration")]
    partial class InitialIdentityServerPersistedGrantDbMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1");

            modelBuilder.Entity("IdentityServer4.EntityFramework.Entities.PersistedGrant", b =>
                {
                    b.Property<string>("Key");

                    b.Property<string>("Type");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 200);

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 8196);

                    b.Property<DateTime>("Expiration");

                    b.Property<string>("SubjectId");

                    b.HasKey("Key", "Type");

                    b.HasIndex("SubjectId");

                    b.HasIndex("SubjectId", "ClientId");

                    b.HasIndex("SubjectId", "ClientId", "Type");

                    b.ToTable("PersistedGrants");
                });
        }
    }
}
