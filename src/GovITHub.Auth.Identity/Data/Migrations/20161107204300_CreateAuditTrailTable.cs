using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GovITHub.Auth.Identity.Data.Migrations
{
    public partial class CreateAuditTrailTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditActions",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false, type: "CHAR(36)"),
                    UserName = table.Column<string>(nullable: true),
                    ActionUrl = table.Column<string>(nullable: false),
                    IpV4 = table.Column<string>(nullable: true),
                    Ipv6 = table.Column<string>(nullable: true),
                    Timestamp = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditActions", t => t.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
