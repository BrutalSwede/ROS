using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ROS.Web.Migrations
{
    public partial class CreateRegattaModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Regattas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    CreatedById = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regattas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Regattas_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Regattas_CreatedById",
                table: "Regattas",
                column: "CreatedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Regattas");
        }
    }
}
