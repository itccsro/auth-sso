using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GovITHub.Auth.Identity.Data.Migrations.ApplicationDb
{
    public partial class AddDeviceDetection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoginDevices",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    OperatingSystem = table.Column<string>(nullable: true),
                    Browser = table.Column<string>(nullable: true),
                    MobileDevice = table.Column<string>(nullable: true),
                    UserAgent = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginDevices", columns => columns.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLoginDevices",
                columns: table => new
                {
                    UserId = table.Column<string>(maxLength: 36, nullable: false),
                    DeviceId = table.Column<string>(maxLength: 36, nullable: false),
                    RegistrationTimeUtc = table.Column<DateTime>(nullable: false),
                    LastLoginTimeUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLoginDevices", columns => new { columns.UserId, columns.DeviceId });
                    table.ForeignKey("FK_UserLoginDevices_LoginDevices",
                        columns => columns.DeviceId,
                        principalTable: "LoginDevices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoginDevices_UserAgent",
                table: "LoginDevices",
                column: "UserAgent",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("UserLoginDevices");
            migrationBuilder.DropTable("LoginDevices");
        }
    }
}
