using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ROS.Web.Migrations
{
    public partial class CreateCrewModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clubs_AspNetUsers_OwnerId",
                table: "Clubs");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Clubs",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "Crews",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BoatId = table.Column<Guid>(nullable: false),
                    CaptainId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Crews_Boat_BoatId",
                        column: x => x.BoatId,
                        principalTable: "Boat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Crews_AspNetUsers_CaptainId",
                        column: x => x.CaptainId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CrewUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CrewId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrewUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrewUser_Crews_CrewId",
                        column: x => x.CrewId,
                        principalTable: "Crews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrewUser_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Crews_BoatId",
                table: "Crews",
                column: "BoatId");

            migrationBuilder.CreateIndex(
                name: "IX_Crews_CaptainId",
                table: "Crews",
                column: "CaptainId");

            migrationBuilder.CreateIndex(
                name: "IX_CrewUser_CrewId",
                table: "CrewUser",
                column: "CrewId");

            migrationBuilder.CreateIndex(
                name: "IX_CrewUser_UserId",
                table: "CrewUser",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clubs_AspNetUsers_OwnerId",
                table: "Clubs",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clubs_AspNetUsers_OwnerId",
                table: "Clubs");

            migrationBuilder.DropTable(
                name: "CrewUser");

            migrationBuilder.DropTable(
                name: "Crews");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Clubs",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Clubs_AspNetUsers_OwnerId",
                table: "Clubs",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
