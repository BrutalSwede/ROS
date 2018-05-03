using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ROS.Web.Migrations
{
    public partial class RegattaRegistration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegattaRegistration",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ApplicationUserGuid = table.Column<string>(nullable: true),
                    BoatId = table.Column<Guid>(nullable: false),
                    RegattaId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegattaRegistration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegattaRegistration_Boats_BoatId",
                        column: x => x.BoatId,
                        principalTable: "Boats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegattaRegistration_Regattas_RegattaId",
                        column: x => x.RegattaId,
                        principalTable: "Regattas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegattaRegistration_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegattaRegistration_BoatId",
                table: "RegattaRegistration",
                column: "BoatId");

            migrationBuilder.CreateIndex(
                name: "IX_RegattaRegistration_RegattaId",
                table: "RegattaRegistration",
                column: "RegattaId");

            migrationBuilder.CreateIndex(
                name: "IX_RegattaRegistration_UserId",
                table: "RegattaRegistration",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegattaRegistration");
        }
    }
}
