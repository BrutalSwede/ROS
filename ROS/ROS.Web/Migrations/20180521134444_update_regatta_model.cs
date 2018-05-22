using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ROS.Web.Migrations
{
    public partial class update_regatta_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Regattas_Clubs_HostingClubId",
                table: "Regattas");

            migrationBuilder.AddForeignKey(
                name: "FK_Regattas_Clubs_HostingClubId",
                table: "Regattas",
                column: "HostingClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Regattas_Clubs_HostingClubId",
                table: "Regattas");

            migrationBuilder.AddForeignKey(
                name: "FK_Regattas_Clubs_HostingClubId",
                table: "Regattas",
                column: "HostingClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
