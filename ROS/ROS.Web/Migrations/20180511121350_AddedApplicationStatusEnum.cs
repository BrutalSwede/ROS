using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ROS.Web.Migrations
{
    public partial class AddedApplicationStatusEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHandled",
                table: "ClubApplications");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ClubApplications",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "ClubApplications");

            migrationBuilder.AddColumn<bool>(
                name: "IsHandled",
                table: "ClubApplications",
                nullable: false,
                defaultValue: false);
        }
    }
}
