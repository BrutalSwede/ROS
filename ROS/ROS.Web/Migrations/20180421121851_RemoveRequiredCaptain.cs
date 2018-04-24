using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ROS.Web.Migrations
{
    public partial class RemoveRequiredCaptain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Crews_AspNetUsers_CaptainId",
                table: "Crews");

            migrationBuilder.AlterColumn<string>(
                name: "CaptainId",
                table: "Crews",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Crews_AspNetUsers_CaptainId",
                table: "Crews",
                column: "CaptainId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Crews_AspNetUsers_CaptainId",
                table: "Crews");

            migrationBuilder.AlterColumn<string>(
                name: "CaptainId",
                table: "Crews",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Crews_AspNetUsers_CaptainId",
                table: "Crews",
                column: "CaptainId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
