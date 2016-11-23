using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GovITHub.Auth.Identity.Data.Migrations.ApplicationDb
{
    public partial class AddOrganization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailProvider",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailProvider", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    ParentId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Org_OrgChild",
                        column: x => x.ParentId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationClient",
                columns: table => new
                {
                    OrganizationId = table.Column<long>(nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationClient", x => new { x.OrganizationId, x.ClientId });
                    table.ForeignKey(
                        name: "FK_OrgClient_Organization",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationSetting",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    AllowSelfRegister = table.Column<bool>(nullable: false),
                    DomainRestriction = table.Column<string>(maxLength: 50, nullable: true),
                    EmailSettingId = table.Column<int>(nullable: true),
                    OrganizationId = table.Column<long>(nullable: true),
                    UseDomainRestriction = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Org_OrgSettings",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationUser",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Level = table.Column<int>(nullable: false),
                    OrganizationId = table.Column<long>(nullable: true),
                    Status = table.Column<short>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Org_OrgUsers",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppUser_OrgUser",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailSetting",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    EmailProviderId = table.Column<long>(nullable: false),
                    OrganizationSettingId = table.Column<long>(nullable: true),
                    Settings = table.Column<string>(maxLength: 8196, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailSetting_EmailProvider",
                        column: x => x.EmailProviderId,
                        principalTable: "EmailProvider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrgSetting_EmailSetting",
                        column: x => x.OrganizationSettingId,
                        principalTable: "OrganizationSetting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplate",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Key = table.Column<string>(maxLength: 20, nullable: false),
                    OrganizationSettingId = table.Column<long>(nullable: false),
                    Value = table.Column<string>(maxLength: 8196, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrgSetting_EmailTemplate",
                        column: x => x.OrganizationSettingId,
                        principalTable: "OrganizationSetting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailSetting_EmailProviderId",
                table: "EmailSetting",
                column: "EmailProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailSetting_OrganizationSettingId",
                table: "EmailSetting",
                column: "OrganizationSettingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplate_OrganizationSettingId",
                table: "EmailTemplate",
                column: "OrganizationSettingId");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_ParentId",
                table: "Organization",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationClient_OrganizationId",
                table: "OrganizationClient",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationSetting_OrganizationId",
                table: "OrganizationSetting",
                column: "OrganizationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationUser_OrganizationId",
                table: "OrganizationUser",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationUser_UserId",
                table: "OrganizationUser",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailSetting");

            migrationBuilder.DropTable(
                name: "EmailTemplate");

            migrationBuilder.DropTable(
                name: "OrganizationClient");

            migrationBuilder.DropTable(
                name: "OrganizationUser");

            migrationBuilder.DropTable(
                name: "EmailProvider");

            migrationBuilder.DropTable(
                name: "OrganizationSetting");

            migrationBuilder.DropTable(
                name: "Organization");
        }
    }
}
