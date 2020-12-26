using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reservea.Persistance.Migrations
{
    public partial class changeIntervalDatabaseType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Interval",
                table: "ResourceAvailabilities");

            migrationBuilder.AddColumn<long>(
                name: "Interval",
                table: "ResourceAvailabilities",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Interval",
                table: "ResourceAvailabilities"
              );

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Interval",
                table: "ResourceAvailabilities",
                type: "time",
                nullable: true);


        }
    }
}
