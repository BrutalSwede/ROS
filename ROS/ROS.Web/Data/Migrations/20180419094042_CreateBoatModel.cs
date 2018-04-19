using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ROS.Web.Data.Migrations
{
    public partial class CreateBoatModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boat",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Certificate = table.Column<string>(nullable: true),
                    HandicapShorthandedWithForesail = table.Column<double>(nullable: false),
                    HandicapShorthandedWithoutForesail = table.Column<double>(nullable: false),
                    HandicapStandardWithForesail = table.Column<double>(nullable: false),
                    HandicapStandardWithoutForesail = table.Column<double>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boat", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Boat");
        }
    }
}
