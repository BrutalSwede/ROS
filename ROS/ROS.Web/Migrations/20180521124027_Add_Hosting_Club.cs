using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ROS.Web.Migrations
{
    public partial class Add_Hosting_Club : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "HostingClubId",
                table: "Regattas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Regattas_HostingClubId",
                table: "Regattas",
                column: "HostingClubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Regattas_Clubs_HostingClubId",
                table: "Regattas",
                column: "HostingClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Regattas_Clubs_HostingClubId",
                table: "Regattas");

            migrationBuilder.DropIndex(
                name: "IX_Regattas_HostingClubId",
                table: "Regattas");

            migrationBuilder.DropColumn(
                name: "HostingClubId",
                table: "Regattas");
        }
    }
}
