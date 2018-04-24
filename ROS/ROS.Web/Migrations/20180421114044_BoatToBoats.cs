using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ROS.Web.Migrations
{
    public partial class BoatToBoats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boat_AspNetUsers_OwnerId",
                table: "Boat");

            migrationBuilder.DropForeignKey(
                name: "FK_Crews_Boat_BoatId",
                table: "Crews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Boat",
                table: "Boat");

            migrationBuilder.RenameTable(
                name: "Boat",
                newName: "Boats");

            migrationBuilder.RenameIndex(
                name: "IX_Boat_OwnerId",
                table: "Boats",
                newName: "IX_Boats_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Boats",
                table: "Boats",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Boats_AspNetUsers_OwnerId",
                table: "Boats",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Crews_Boats_BoatId",
                table: "Crews",
                column: "BoatId",
                principalTable: "Boats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boats_AspNetUsers_OwnerId",
                table: "Boats");

            migrationBuilder.DropForeignKey(
                name: "FK_Crews_Boats_BoatId",
                table: "Crews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Boats",
                table: "Boats");

            migrationBuilder.RenameTable(
                name: "Boats",
                newName: "Boat");

            migrationBuilder.RenameIndex(
                name: "IX_Boats_OwnerId",
                table: "Boat",
                newName: "IX_Boat_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Boat",
                table: "Boat",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Boat_AspNetUsers_OwnerId",
                table: "Boat",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Crews_Boat_BoatId",
                table: "Crews",
                column: "BoatId",
                principalTable: "Boat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
