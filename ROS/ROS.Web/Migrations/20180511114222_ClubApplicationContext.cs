using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ROS.Web.Migrations
{
    public partial class ClubApplicationContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClubApplication_Clubs_ClubId",
                table: "ClubApplication");

            migrationBuilder.DropForeignKey(
                name: "FK_ClubApplication_AspNetUsers_UserId",
                table: "ClubApplication");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClubApplication",
                table: "ClubApplication");

            migrationBuilder.RenameTable(
                name: "ClubApplication",
                newName: "ClubApplications");

            migrationBuilder.RenameIndex(
                name: "IX_ClubApplication_UserId",
                table: "ClubApplications",
                newName: "IX_ClubApplications_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ClubApplication_ClubId",
                table: "ClubApplications",
                newName: "IX_ClubApplications_ClubId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClubApplications",
                table: "ClubApplications",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClubApplications_Clubs_ClubId",
                table: "ClubApplications",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClubApplications_AspNetUsers_UserId",
                table: "ClubApplications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClubApplications_Clubs_ClubId",
                table: "ClubApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_ClubApplications_AspNetUsers_UserId",
                table: "ClubApplications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClubApplications",
                table: "ClubApplications");

            migrationBuilder.RenameTable(
                name: "ClubApplications",
                newName: "ClubApplication");

            migrationBuilder.RenameIndex(
                name: "IX_ClubApplications_UserId",
                table: "ClubApplication",
                newName: "IX_ClubApplication_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ClubApplications_ClubId",
                table: "ClubApplication",
                newName: "IX_ClubApplication_ClubId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClubApplication",
                table: "ClubApplication",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClubApplication_Clubs_ClubId",
                table: "ClubApplication",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClubApplication_AspNetUsers_UserId",
                table: "ClubApplication",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
