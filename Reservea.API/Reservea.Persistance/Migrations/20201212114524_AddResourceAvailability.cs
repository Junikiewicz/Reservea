using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reservea.Persistance.Migrations
{
    public partial class AddResourceAvailability : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResourceAvailabilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsReccuring = table.Column<bool>(type: "bit", nullable: false),
                    Interval = table.Column<TimeSpan>(type: "time", nullable: true),
                    ResourceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceAvailabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceAvailabilities_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResourceAvailabilities_ResourceId",
                table: "ResourceAvailabilities",
                column: "ResourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResourceAvailabilities");
        }
    }
}
