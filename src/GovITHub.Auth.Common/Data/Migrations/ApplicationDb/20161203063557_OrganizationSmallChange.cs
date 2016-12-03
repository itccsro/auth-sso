using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GovITHub.Auth.Common.Data.Migrations.ApplicationDb
{
    public partial class OrganizationSmallChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrgSetting_EmailSetting",
                table: "EmailSetting");

            migrationBuilder.DropIndex(
                name: "IX_EmailSetting_OrganizationSettingId",
                table: "EmailSetting");

            migrationBuilder.DropColumn(
                name: "OrganizationSettingId",
                table: "EmailSetting");

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "Organization",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "EmailSettingId",
                table: "OrganizationSetting",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationSetting_EmailSettingId",
                table: "OrganizationSetting",
                column: "EmailSettingId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrgSetting_EmailSetting",
                table: "OrganizationSetting",
                column: "EmailSettingId",
                principalTable: "EmailSetting",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrgSetting_EmailSetting",
                table: "OrganizationSetting");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationSetting_EmailSettingId",
                table: "OrganizationSetting");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "Organization");

            migrationBuilder.AddColumn<long>(
                name: "OrganizationSettingId",
                table: "EmailSetting",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmailSettingId",
                table: "OrganizationSetting",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmailSetting_OrganizationSettingId",
                table: "EmailSetting",
                column: "OrganizationSettingId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrgSetting_EmailSetting",
                table: "EmailSetting",
                column: "OrganizationSettingId",
                principalTable: "OrganizationSetting",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
